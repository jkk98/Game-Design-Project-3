using UnityEngine;
using System.Collections;

public class PlayerDamageLogic : MonoBehaviour {

	// Use this for initialization
	public Transform playerMelee;
	private Transform enemyAttack;
	public GameObject myMace;
	public int health;
	private bool immune = false;
	private static float immunity_time;
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time - immunity_time > 1.0f) {
			immune = false;
		}
		if (health <= 0) {
			Dead ();
		}
	}

	void OnTriggerEnter(Collider other){
		if (other.transform.gameObject.name == "EnemyMace") {
			enemyAttack = other.transform.parent.FindChild ("MyMelee");
			if (enemyAttack.GetComponent<EnemyMelee> ().attack) {
				int dmg = other.GetComponent<MaceAttributes> ().damage;
				string mytype = myMace.GetComponent<MaceAttributes> ().recentAttack;
				string ptype = other.GetComponent<MaceAttributes> ().recentAttack;
				if ((mytype == "Hstrike" && ptype == "Hblock") || (mytype == "Vstrike" && ptype == "Vblock") || (mytype == "Stab" && ptype == "Sblock")) {
					print ("Parry Enemy ");
				} else if ((mytype == "Hstrike" && ptype == "Hstrike") || (mytype == "Vstrike" && ptype == "Vstrike") || (mytype == "Stab" && ptype == "Stab")) {
					print ("Both Parry");
				} else if ((mytype == "Stab" && ptype == "Hblock") || (mytype == "Vstrike" && ptype == "Sblock") || (mytype == "Hstrike" && ptype == "Vblock")) {
					print ("Blocked Enemy");
				} else if ((mytype == "Stab" && ptype == "Hblock") || (mytype == "Vstrike" && ptype == "Sblock") || (mytype == "Hstrike" && ptype == "Vblock")) {
					if (other.GetComponent<Animator> ().GetCurrentAnimatorStateInfo (0).normalizedTime < .70f) {
						print ("Enemy take Damage");
						ApplyDamage (dmg);
					}
				} else if (mytype == "none") {
					print ("Player take Damage");
					ApplyDamage (dmg);
				} else {
					print ("Error");
					print (mytype);
					print (ptype);
					print ("end");
				}
			}
		} else if(other.transform.gameObject.tag == "Rat"){
			if (other.GetComponent<ratai> ().attack == 1 && !immune) {
				int dmg = other.GetComponent<ratai> ().damage;
				string ptype = myMace.GetComponent<MaceAttributes> ().recentAttack;
				if (ptype == "Hblock" || ptype == "Vblock" || ptype == "Sblock") {
					//block
					print("Blocked");
				} else {
					ApplyDamage (dmg);
					immune = true;
					immunity_time = Time.time;
				}
			}

		}
	}
	void ApplyDamage(int damage){
		health -= damage;
	}

	void Dead(){
		Destroy (gameObject);
	}
}

