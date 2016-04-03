using UnityEngine;
using System.Collections;

public class EnemyDamageLogic : MonoBehaviour {

	// Use this for initialization
	public int health = 100;
	public Transform meleeSys;
	private bool block = false;
	private int hit;
	// Update is called once per frame
	void Update () {
		block = !block;
		if (health <= 0) {
			Dead ();
		}
	}

	void OnTriggerEnter(Collider other){
		if (meleeSys.GetComponent<MeleeSystem> ().attack) {
			if (other.transform.gameObject.name == "Mace") {
				int dmg = other.GetComponent<MaceAttributes> ().damage;
				string type = other.GetComponent<MaceAttributes> ().recentAttack;
				if (block && type == "Vertical") {
					print ("Blocked Vertical");
				} else if (!block && type == "Horizontal") {
					print ("Blocked Horizontal");
				} else {
					if (other.GetComponent<Animator> ().GetCurrentAnimatorStateInfo (0).normalizedTime < .50) {
						hit += 1;
						print (hit);
						ApplyDamage (dmg);
					}
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
