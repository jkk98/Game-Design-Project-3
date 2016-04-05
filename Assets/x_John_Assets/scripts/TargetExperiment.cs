using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TargetExperiment : MonoBehaviour {

	public float max_target_distance = 10;
	public float interactable_distance = 2;
	private bool interactable = false;

	public Collider targeted_object_collider;
	public Text target_name;


	// Use this for initialization
	void Start () {
		Target_GUI_Effet("");
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		// Sets up the ray and casts it
		RaycastHit hit;
		Ray targetingRay = new Ray (transform.position, transform.forward);
		Debug.DrawRay (transform.position, transform.forward * max_target_distance);
		Physics.Raycast(targetingRay, out hit, max_target_distance);

		// Detected an object
		if (hit.collider != null) {
			targeted_object_collider = hit.collider;
			Target_GUI_Effet(targeted_object_collider.name);

			// Check if close enough to interact with object
			if (hit.distance < interactable_distance) {
				target_name.color = Color.green;
				interactable = true;
			}
			else{
				target_name.color = Color.red;
				interactable = false;
			}
		}
		// else no object detected
		else{
			targeted_object_collider = null;
			interactable = false; // Redundantly changes interactable to false in cases where ray cast immediately loses object while up close
			Target_GUI_Effet("");
		}

		if (Input.GetKeyDown ("return")) {
			interact ();
		}
	}

	void Target_GUI_Effet(string name){
		target_name.text = name;
	}

	void interact()
	{
		if (interactable == true) {
			Debug.Log ("Interacting with object");
		} else if (targeted_object_collider != null) {
			Debug.Log ("Object is too far away to interact with");
		} else {
			Debug.Log ("No Object to interact with");
		}
	}
}
