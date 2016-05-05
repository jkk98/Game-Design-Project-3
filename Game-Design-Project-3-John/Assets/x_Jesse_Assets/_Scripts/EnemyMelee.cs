using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EnemyMelee : MonoBehaviour {

	// Use this for initialization
	private bool beginStrafe = true;
	public Transform player;
	public Transform enemy;
	public int SwordDamage = 50;
	public float SwordDistance = 2.0f;
	public float RayDistance = 10.0f;
	public float Distance = 0f;
	public bool attack = false;
	private bool horizontalRight = true;
	public Animator anim;
	public Animator shield;
	private int block_cnt;
	private int action;
	private bool can_attack = false;
	public int attackDist = 10;
	public int moveSpeed = 4;
	private bool aggro = false;
	public int aggroDist = 10;
	public GameObject weapon;
    private int strafe = 0;
	private float tempTime;
	private float randomStrafe;
	void Start(){
		enemy.GetComponent<NavMeshAgent> ().updateRotation = false;
	}
	// Update is called once per frame
	void Update () {
		if (aggro) {
			enemy.LookAt (player);
		}

		if (Vector3.Distance (transform.position, player.position) >= attackDist && aggro) {
			enemy.GetComponent<NavMeshAgent> ().speed = 3.5f;
			enemy.GetComponent<NavMeshAgent> ().destination = player.transform.position;
			can_attack = false;
			enemy.LookAt (player);
		} else if (aggro) {
			enemy.GetComponent<NavMeshAgent> ().speed = 1f;
			setTempTime ();
			if (strafe == 0) {
				enemy.GetComponent<NavMeshAgent> ().destination = enemy.position + enemy.right;
				beginStrafe = false;
			} else if (strafe == 1) {
				enemy.GetComponent<NavMeshAgent> ().destination = enemy.position - enemy.right;
				beginStrafe = false;
			} else {
				enemy.GetComponent<NavMeshAgent> ().destination = enemy.position;
				beginStrafe = false;
			}
			if (Time.time - tempTime >= randomStrafe) {
				beginStrafe = true;
				strafe++;
				if (strafe > 2) {
					strafe = 0;
				}
			}
			can_attack = true;
			enemy.LookAt (player);

		}
		if (Vector3.Distance (transform.position, player.position) <= aggroDist) {
			enemy.LookAt (player);
			aggro = true;
		}


		action = Random.Range (1, 6);
		if (block_cnt > 100) {
			shield.Play ("idleshield", -1, 8f);
			action = Random.Range (4, 6);
			block_cnt = 0;
		}
		if (anim.GetCurrentAnimatorStateInfo (0).IsName ("idle") && shield.GetCurrentAnimatorStateInfo(0).IsName("idleshield")) {
			attack = false;
			//RaycastHit hitInfo;
			//Ray hitRay = new Ray (transform.position, transform.TransformDirection (Vector3.forward));

			if (action == 1) {
				weapon.GetComponent<MaceAttributes> ().recentAttack = "Hblock";
				shield.Play ("Hblock", -1, 0f);

			}else if (action == 2) {
				weapon.GetComponent<MaceAttributes> ().recentAttack = "Sblock";
				shield.Play ("Sblock", -1, 0f);

			}else if (action == 3) {
				weapon.GetComponent<MaceAttributes> ().recentAttack = "Vblock";
				shield.Play ("Vblock", -1, 0f);

			}else if(action == 4 && can_attack){
				weapon.GetComponent<MaceAttributes> ().recentAttack = "Hstrike";
				attack = true;
				if (horizontalRight) {
					anim.Play ("horizontalright", -1, 0f);
					horizontalRight = false;
				} else if (!horizontalRight) {
					anim.Play ("horizontalleft", -1, 0f);
					horizontalRight = true;
				}

			}else if (action == 5 && can_attack){
				weapon.GetComponent<MaceAttributes> ().recentAttack = "Stab";
				attack = true;
				anim.Play ("stab", -1, 0f);
			}else if (action == 6 && can_attack) {
				weapon.GetComponent<MaceAttributes> ().recentAttack = "Vstrike";
				anim.Play ("verticle", -1,0f);
				attack = true;
				/*weapon.GetComponent<Animator> ().enabled = true;
				if (Physics.Raycast (hitRay, out hitInfo, RayDistance)) {
					Distance = hitInfo.distance;
					Debug.Log("Hit");
					if (Distance <= SwordDistance) {
						Debug.Log ("SwordHit");
						hitInfo.transform.SendMessage ("ApplyDamage", SwordDamage, SendMessageOptions.DontRequireReceiver);
					}
				} else{
					Debug.Log("None");
				}*/
			}
		}else if(!shield.GetCurrentAnimatorStateInfo(0).IsName("idleshield")){
			block_cnt += 1;

		}
	}

	/*IEnumerator WaitForAnimation(){
		yield return new WaitForSeconds (1);
	}*/
	void setTempTime(){
		if (beginStrafe) {
			tempTime = Time.time;
			randomStrafe = Random.Range (3, 11);
		}
	}
}
