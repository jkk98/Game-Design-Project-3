﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MeleeSystem : MonoBehaviour {

	// Use this for initialization
	public int SwordDamage = 50;
	public float SwordDistance = 2.0f;
	public float RayDistance = 10.0f;
	public float Distance = 0f;
	public bool attack = false;
	private bool horizontalRight = true;
	public Animator anim;

	public GameObject weapon;

	// Update is called once per frame
	void Update () {
		if (anim.GetCurrentAnimatorStateInfo (0).IsName ("idle")) {
			attack = false;
			//RaycastHit hitInfo;
			//Ray hitRay = new Ray (transform.position, transform.TransformDirection (Vector3.forward));

			if(Input.GetButtonDown("Mouse1") && Input.GetButton("Hstrike")){
				weapon.GetComponent<MaceAttributes> ().recentAttack = "Horizontal";
				attack = true;
				if (horizontalRight) {
					anim.Play ("horizontalright", -1, 0f);
					horizontalRight = false;
				} else if (!horizontalRight) {
					anim.Play ("horizontalleft", -1, 0f);
					horizontalRight = true;
				}
			
			}else if (Input.GetButtonDown("Mouse1") && Input.GetButton("Stab")){
				weapon.GetComponent<MaceAttributes> ().recentAttack = "Stab";
				attack = true;
				anim.Play ("stab", -1, 0f);
			}else if (Input.GetButtonDown ("Mouse1")) {
				weapon.GetComponent<MaceAttributes> ().recentAttack = "Vertical";
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
		}
	}

	/*IEnumerator WaitForAnimation(){
		yield return new WaitForSeconds (1);
	}*/
		
}
