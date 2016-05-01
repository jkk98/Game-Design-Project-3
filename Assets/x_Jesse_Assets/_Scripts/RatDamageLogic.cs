using UnityEngine;
using System.Collections;

public class RatDamageLogic : MonoBehaviour {
	public int health = 100;
	public Transform playerMelee;
	public GameObject player;
	public Animator RatAnim;
	private bool setDead = false;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (health <= 0) {
			foreach (Collider c in GetComponents<Collider>()) {
				c.enabled = false;
			}
			transform.GetComponent<ratai> ().attack = 0;
			Dead ();
		}
	}

	void OnTriggerEnter(Collider other){
		if (other.transform.gameObject.name == "Mace") {
			if (playerMelee.GetComponent<MeleeSystem> ().attack) {
				int dmg = other.GetComponent<MaceAttributes> ().damage;
				ApplyDamage (dmg);
			}
		}else if(other.transform.gameObject.name == "Player"){
			other.transform.position -= other.transform.forward * .3f;
		}
	}

	void ApplyDamage(int damage){
		health -= damage;
	}

	void Dead(){
		if(RatAnim.GetCurrentAnimatorStateInfo(0).IsName("rat_deathstay 2") && setDead){
			Destroy(gameObject);
		}
		if (!setDead) {
			RatAnim.Play ("rat_death");
		}
		if (!RatAnim.GetCurrentAnimatorStateInfo (0).IsName ("rat_death") && !setDead) {
			setDead = true;
		}


	}
}
