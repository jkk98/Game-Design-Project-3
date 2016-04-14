using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EnemyMelee : MonoBehaviour {

	// Use this for initialization
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
	public GameObject weapon;

	// Update is called once per frame
	void Update () {
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

			}else if(action == 4){
				weapon.GetComponent<MaceAttributes> ().recentAttack = "Hstrike";
				attack = true;
				if (horizontalRight) {
					anim.Play ("horizontalright", -1, 0f);
					horizontalRight = false;
				} else if (!horizontalRight) {
					anim.Play ("horizontalleft", -1, 0f);
					horizontalRight = true;
				}

			}else if (action == 5){
				weapon.GetComponent<MaceAttributes> ().recentAttack = "Stab";
				attack = true;
				anim.Play ("stab", -1, 0f);
			}else if (action == 6) {
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

}
