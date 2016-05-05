using UnityEngine;
using System.Collections;

public class ratai : MonoBehaviour {

	public Transform player;
	public int MoveSpeed = 4;
	public int MaxDist = 10;
	public int MinDist = 5;
	public Animator ratAnim;
	private int bored = 0;
	public int attack = 0;
	public int damage = 20;
	public bool aggro = false;
	public int aggroDistance = 10;
	public int hp;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		hp = transform.GetComponent<RatDamageLogic> ().health;
		if (hp > 0) {
			if (Vector3.Distance (transform.position, player.position) <= aggroDistance || aggro) {
				transform.LookAt (player);
				aggro = true;

				if (ratAnim.GetCurrentAnimatorStateInfo (0).IsName ("rat_backoff") || (transform.position.x == player.transform.position.x && transform.position.z == player.transform.position.z)) {
					transform.position -= transform.forward * .15f;
					attack = 0;

				} else if (Vector3.Distance (transform.position, player.position) >= MinDist) {
					Vector3 temp = new Vector3 (1, 2, 1);
					GetComponent<NavMeshAgent> ().destination = player.transform.position - temp;
					if (!ratAnim.GetCurrentAnimatorStateInfo (0).IsName ("rat_move") && !ratAnim.GetCurrentAnimatorStateInfo (0).IsName ("rat_attack")) {
						ratAnim.Play ("rat_move", -1, 0f);
						attack = 0;
					}
					bored = 0;
				} else {
					bored += 1;
					if (bored >= 100) {
						if (!ratAnim.GetCurrentAnimatorStateInfo (0).IsName ("rat_bored")) {
							ratAnim.Play ("rat_bored", -1, 0f);
							attack = 0;
						}
						bored = 0;
					}

				}
				if (Vector3.Distance (transform.position, player.position) <= MaxDist) {
					if (!ratAnim.GetCurrentAnimatorStateInfo (0).IsName ("rat_attack") && !ratAnim.GetCurrentAnimatorStateInfo (0).IsName ("rat_backoff")) {
						ratAnim.Play ("rat_attack", -1, 0f);
						attack = 1;
					}
				}
			}
		}
   }
}
