using UnityEngine;
using System.Collections;

public class EnemyDamageLogic : MonoBehaviour {

	// Use this for initialization
	public int health = 100;
	public Transform playerMelee;
	public Transform myMace;
	public GameObject player;
	private bool block = false;
	// Update is called once per frame
	void Update () {
		block = !block;
		if (health <= 0) {
			Dead ();
		}
	}

	void OnTriggerEnter(Collider other){
		if (playerMelee.GetComponent<MeleeSystem> ().attack) {
			if (other.transform.gameObject.name == "Mace") {
				int dmg = other.GetComponent<MaceAttributes> ().damage;
				string ptype = other.GetComponent<MaceAttributes> ().recentAttack;
				string mytype = myMace.GetComponent<MaceAttributes> ().recentAttack;
				if ((mytype == "Hblock" && ptype == "Hstrike") || (mytype == "Vblock" && ptype == "Vstrike") || (mytype == "Sblock" && ptype == "Stab")) {
					print ("Parry Player");
				} else if ((mytype == "Hstrike" && ptype == "Hblock") || (mytype == "Vstrike" && ptype == "Vblock") || (mytype == "Stab" && ptype == "Sblock")) {
					print ("Parry Enemy ");
				} else if ((mytype == "Hstrike" && ptype == "Hstrike") || (mytype == "Vstrike" && ptype == "Vstrike") || (mytype == "Stab" && ptype == "Stab")) {
					print ("Both Parry");
				} else if ((mytype == "Hblock" && ptype == "Stab") || (mytype == "Sblock" && ptype == "Vstrike") || (mytype == "Vblock" && ptype == "Hstrike")) {
					print ("Blocked Player");
				} else if ((mytype == "Stab" && ptype == "Hblock") || (mytype == "Vstrike" && ptype == "Sblock") || (mytype == "Hstrike" && ptype == "Vblock")) {
					print ("Blocked Enemy");
				} else if ((mytype == "Hblock" && ptype == "Vstrike") || (mytype == "Vblock" && ptype == "Stab") || (mytype == "Sblock" && ptype == "Hstrike")) {
					if (other.GetComponent<Animator> ().GetCurrentAnimatorStateInfo (0).normalizedTime < .70) {
						print ("Enemy take Damage");
						ApplyDamage (dmg);
					}
				} else if ((mytype == "Stab" && ptype == "Hblock") || (mytype == "Vstrike" && ptype == "Sblock") || (mytype == "Hstrike" && ptype == "Vblock")) {
					print ("Player take Damage");
				} else if ((mytype == "Hstrike" && (ptype == "stab" || ptype == "Vstrike")) || (mytype == "Vstrike" && (ptype == "Hstrike" || ptype == "Stab")) || (mytype == "Hstrike" && (ptype == "Vstrike" || ptype == "Stab"))) {
					print ("Both take Damage");
					player.GetComponent<PlayerDamageLogic> ().health -= dmg;
					ApplyDamage (dmg);
				} else if ((ptype == "Hstrike" && (mytype == "stab" || mytype == "Vstrike")) || (ptype == "Vstrike" && (mytype == "Hstrike" || mytype == "Stab")) || (ptype == "Hstrike" && (mytype == "Vstrike" || mytype == "Stab"))) {
					print ("Both take Damage");
					player.GetComponent<PlayerDamageLogic> ().health -= dmg;
					ApplyDamage (dmg);
				} else {
					print ("Error");
					print (mytype);
					print (ptype);
					print ("end");
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
