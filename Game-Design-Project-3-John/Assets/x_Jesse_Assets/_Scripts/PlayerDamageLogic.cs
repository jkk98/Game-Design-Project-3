using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerDamageLogic : MonoBehaviour {

	// Use this for initialization
	public Transform playerMelee;
	private Transform enemyAttack;
	public GameObject myMace;
	public int health;
	public GameObject cam_Anim;
	private bool immune = false;
	private static float immunity_time;
	public GameObject playerSounds;
	private int painCount = 0;
	private float tempTime;
	private bool setTime = true;

	public Text health_UI;

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (cam_Anim.GetComponent<Animator> ().GetCurrentAnimatorStateInfo (0).IsName ("New State")) {
			cam_Anim.GetComponent<Animator> ().enabled = false;
		}
		if (Time.time - immunity_time > .8f) {
			immune = false;
		}
		if (health <= 0) {
			Dead ();
		}
		health_UI.text = health.ToString();
	}

	void OnTriggerEnter(Collider other){
		cam_Anim.GetComponent<Animator> ().enabled = true;
		if (other.transform.gameObject.name == "SSword") {
			Debug.Log ("Trigger");
			enemyAttack = other.transform.parent.FindChild ("MyMelee");
			if (enemyAttack.GetComponent<EnemyMelee> ().attack) {
				int dmg = other.GetComponent<MaceAttributes> ().damage;
				string mytype = myMace.GetComponent<MaceAttributes> ().recentAttack;
				string ptype = other.GetComponent<MaceAttributes> ().recentAttack;
				if ((mytype == "Hstrike" && ptype == "Hblock") || (mytype == "Vstrike" && ptype == "Vblock") || (mytype == "Stab" && ptype == "Sblock")) {
					print ("Parry Enemy ");
					playerSounds.transform.GetChild (4).GetComponent<AudioSource> ().Play ();
					playerSounds.transform.GetChild (5).GetComponent<AudioSource> ().Play ();
				} else if ((mytype == "Hstrike" && ptype == "Hstrike") || (mytype == "Vstrike" && ptype == "Vstrike") || (mytype == "Stab" && ptype == "Stab")) {
					print ("Both Parry");
					playerSounds.transform.GetChild (6).GetComponent<AudioSource> ().Play ();
				} else if ((mytype == "Stab" && ptype == "Hblock") || (mytype == "Vstrike" && ptype == "Sblock") || (mytype == "Hstrike" && ptype == "Vblock")) {
					print ("Blocked Enemy");
					playerSounds.transform.GetChild (4).GetComponent<AudioSource> ().Play ();
				} else if ((mytype == "Stab" && ptype == "Hblock") || (mytype == "Vstrike" && ptype == "Sblock") || (mytype == "Hstrike" && ptype == "Vblock")) {
					if (other.GetComponent<Animator> ().GetCurrentAnimatorStateInfo (0).normalizedTime < .70f) {
						print ("Enemy take Damage");
					}
				} else if (mytype == "idle" || mytype == "none") {
					print ("Player take Damage");
					ApplyDamage (dmg);
				} else{
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
					if (setTime) {
						tempTime = Time.time;
						setTime = false;
						playerSounds.transform.GetChild (4).GetComponent<AudioSource> ().Play ();
					}
					if (Time.time - tempTime >= 1f) {
						setTime = true;
					}
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
		if (painCount < 3) {
			painCount++;
			playerSounds.transform.GetChild (0).GetComponent<AudioSource> ().Play ();
		} else {
			playerSounds.transform.GetChild (1).GetComponent<AudioSource> ().Play ();
			painCount = 0;
		}
		cam_Anim.GetComponent<Animator> ().Play ("dmgShake");
	}

	void Dead(){
		SceneManager.LoadScene ("Menu Screen");
	}
}

