using UnityEngine;
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
	public GameObject rightHand;
	private GameObject rightHandChild;
	public GameObject leftHand;
	public GameObject leftHandChild;
	public Animator anim;
	public Animator shield;
	private bool toggle = true;
	public GameObject playerSounds;

	public GameObject weapon;

	void Start(){
		rightHandChild = rightHand.transform.GetChild(0).gameObject;
		leftHandChild = leftHand.transform.GetChild (1).gameObject;
	}
	// Update is called once per frame
	void Update () {
		if(Input.GetButtonDown("ToggleHide")){
			toggle = !toggle;
			rightHand.GetComponent<BoxCollider> ().enabled = toggle;
			rightHandChild.GetComponent<MeshRenderer> ().enabled = toggle;
			leftHandChild.GetComponent<MeshRenderer> ().enabled = toggle;
			weapon.GetComponent<MaceAttributes>().recentAttack = "idle";
		}
		if(toggle){
			if (shield.GetCurrentAnimatorStateInfo(0).IsName("idleshield") && anim.GetCurrentAnimatorStateInfo(0).IsName("idle")){
			weapon.GetComponent<MaceAttributes>().recentAttack = "idle";
		}
		if (Input.GetButtonUp ("Mouse2")) {
			shield.Play ("idleshield", -1, .9f);
			weapon.GetComponent<MaceAttributes> ().recentAttack = "idle";
		}

			if (anim.GetCurrentAnimatorStateInfo (0).IsName ("idle") && shield.GetCurrentAnimatorStateInfo (0).IsName ("idleshield")) {
				attack = false;
				weapon.GetComponent<MaceAttributes> ().recentAttack = "idle";
				//RaycastHit hitInfo;
				//Ray hitRay = new Ray (transform.position, transform.TransformDirection (Vector3.forward));

				if (Input.GetButtonDown ("Mouse2") && Input.GetButton ("Hstrike")) {
					weapon.GetComponent<MaceAttributes> ().recentAttack = "Hblock";
					shield.Play ("Hblock", -1, 0f);

				} else if (Input.GetButtonDown ("Mouse2") && Input.GetButton ("Stab")) {
					weapon.GetComponent<MaceAttributes> ().recentAttack = "Sblock";
					shield.Play ("Sblock", -1, 0f);

				} else if (Input.GetButtonDown ("Mouse2")) {
					weapon.GetComponent<MaceAttributes> ().recentAttack = "Vblock";
					shield.Play ("Vblock", -1, 0f);

				} else if (Input.GetButtonDown ("Mouse1") && Input.GetButton ("Hstrike")) {
					weapon.GetComponent<MaceAttributes> ().recentAttack = "Hstrike";
					attack = true;

					if (horizontalRight) {
						anim.Play ("horizontalright", -1, 0f);
						horizontalRight = false;
					} else if (!horizontalRight) {
						anim.Play ("horizontalleft", -1, 0f);
						horizontalRight = true;
					}
					playerSounds.transform.GetChild (2).GetComponent<AudioSource> ().Play ();
			
				} else if (Input.GetButtonDown ("Mouse1") && Input.GetButton ("Stab")) {
					weapon.GetComponent<MaceAttributes> ().recentAttack = "Stab";
					attack = true;
					anim.Play ("stab", -1, 0f);
					playerSounds.transform.GetChild (2).GetComponent<AudioSource> ().Play ();
				} else if (Input.GetButtonDown ("Mouse1")) {
					weapon.GetComponent<MaceAttributes> ().recentAttack = "Vstrike";
					anim.Play ("verticle", -1, 0f);
					attack = true;
					playerSounds.transform.GetChild (3).GetComponent<AudioSource> ().Play ();
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
	}

	/*IEnumerator WaitForAnimation(){
		yield return new WaitForSeconds (1);
	}*/
		
}
