using UnityEngine;
using System.Collections;

public class EnemyDamageLogic : MonoBehaviour {

	// Use this for initialization
	public int health = 100;
	public Transform playerMelee;
	public Transform myMace;
	public GameObject player;
	public Transform myMelee;
	private bool block = false;
	public GameObject bossSounds;
	private bool DeadSound = true;
	private bool immune = false;
	private static float immunity_time;
	private float tempTime;
	private bool setTime = true;
	public Transform myShield;
	public Transform myBody;
	// Update is called once per frame
	void Update () {
		block = !block;
		if (health <= 0) {
			Dead ();
		}
		if (Time.time - immunity_time > .8f) {
			immune = false;
		}
	}

	void OnTriggerEnter(Collider other){
		if (playerMelee.GetComponent<MeleeSystem> ().attack && !immune && DeadSound) {
			if (other.transform.gameObject.name == "SSword") {
				int dmg = other.GetComponent<MaceAttributes> ().damage;
				string ptype = other.GetComponent<MaceAttributes> ().recentAttack;
				string mytype = myMace.GetComponent<MaceAttributes> ().recentAttack;
				if ((mytype == "Hblock" && ptype == "Hstrike") || (mytype == "Vblock" && ptype == "Vstrike") || (mytype == "Sblock" && ptype == "Stab")) {
					bossSounds.transform.GetChild (2).GetComponent<AudioSource> ().Play ();
					bossSounds.transform.GetChild (1).GetComponent<AudioSource> ().Play ();
				} else if ((mytype == "Hstrike" && ptype == "Hblock") || (mytype == "Vstrike" && ptype == "Vblock") || (mytype == "Stab" && ptype == "Sblock")) {
					bossSounds.transform.GetChild (2).GetComponent<AudioSource> ().Play ();
					bossSounds.transform.GetChild (1).GetComponent<AudioSource> ().Play ();
				} else if ((mytype == "Hstrike" && ptype == "Hstrike") || (mytype == "Vstrike" && ptype == "Vstrike") || (mytype == "Stab" && ptype == "Stab")) {
					bossSounds.transform.GetChild (2).GetComponent<AudioSource> ().Play ();
				} else if ((mytype == "Hblock" && ptype == "Stab") || (mytype == "Sblock" && ptype == "Vstrike") || (mytype == "Vblock" && ptype == "Hstrike")) {
					bossSounds.transform.GetChild (1).GetComponent<AudioSource> ().Play ();
				} else if ((mytype == "Stab" && ptype == "Hblock") || (mytype == "Vstrike" && ptype == "Sblock") || (mytype == "Hstrike" && ptype == "Vblock")) {
					bossSounds.transform.GetChild (1).GetComponent<AudioSource> ().Play ();
				} else if ((mytype == "Hblock" && ptype == "Vstrike") || (mytype == "Vblock" && ptype == "Stab") || (mytype == "Sblock" && ptype == "Hstrike")) {
					if (other.GetComponent<Animator> ().GetCurrentAnimatorStateInfo (0).normalizedTime < .70) {
						ApplyDamage (dmg);
					}
				} else if ((mytype == "Stab" && ptype == "Hblock") || (mytype == "Vstrike" && ptype == "Sblock") || (mytype == "Hstrike" && ptype == "Vblock")) {
					
				} else if ((mytype == "Hstrike" && (ptype == "stab" || ptype == "Vstrike")) || (mytype == "Vstrike" && (ptype == "Hstrike" || ptype == "Stab")) || (mytype == "Hstrike" && (ptype == "Vstrike" || ptype == "Stab"))) {
					
					player.GetComponent<PlayerDamageLogic> ().health -= dmg;
					ApplyDamage (dmg);
				} else if ((ptype == "Hstrike" && (mytype == "stab" || mytype == "Vstrike")) || (ptype == "Vstrike" && (mytype == "Hstrike" || mytype == "Stab")) || (ptype == "Hstrike" && (mytype == "Vstrike" || mytype == "Stab"))) {
					
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
		immune = true;
		immunity_time = Time.time;
		bossSounds.transform.GetChild (0).GetComponent<AudioSource> ().Play ();
		Debug.Log ("Damage");
	}

	void Dead(){
		myMace.GetChild (0).GetComponent<MeshRenderer> ().enabled = false;
		myShield.GetChild (1).GetComponent<MeshRenderer> ().enabled = false;
		myBody.GetComponent<MeshRenderer> ().enabled = false;
		if (DeadSound) {
			bossSounds.transform.GetChild (3).GetComponent<AudioSource> ().Play ();
			DeadSound = false;
		}if (!bossSounds.transform.GetChild (3).GetComponent<AudioSource> ().isPlaying) {
			Destroy (gameObject);
		}
	}
}
