using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TargetExperiment : MonoBehaviour {

	public float max_target_distance = 10;
	public float interactable_distance = 2;
	private bool interactable = false;

	public Collider targeted_object_collider;
	public Text target_name;
	public GameObject ray_target_ui;
	public KeyCode interact_button = KeyCode.Tab;

	private Image ray_target_image;


	// Use this for initialization
	void Start () {
		Target_GUI_Effet("");
		ray_target_image = ray_target_ui.GetComponent<Image> ();
		ray_target_ui.SetActive (false);
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
			if (hit.collider.gameObject.CompareTag ("Agent") || hit.collider.gameObject.CompareTag ("Note") || hit.collider.gameObject.CompareTag ("Device")) { 
				targeted_object_collider = hit.collider;
				//Target_GUI_Effet (targeted_object_collider.name);
				ray_target_ui.SetActive (true);


				// Check if close enough to interact with object
				if (hit.distance < interactable_distance) {
					
					//target_name.color = Color.green;
					ray_target_image.color = new Color(0, 1, 0, 0.75f); // Sets the color to green and somewhat transparent
					interactable = true;
				} else {
					//target_name.color = Color.red;
					interactable = false;
					ray_target_image.color = new Color(1, 0, 0, .75f); // Sets the color to red and somewhat transparent
				}
			} else {
				targeted_object_collider = null;
				interactable = false; // Redundantly changes interactable to false in cases where ray cast immediately loses object while up close
				//Target_GUI_Effet("");
				ray_target_ui.SetActive (false);
			}
		}
		// else no object detected
		else{
			targeted_object_collider = null;
			interactable = false; // Redundantly changes interactable to false in cases where ray cast immediately loses object while up close
			//Target_GUI_Effet("");
			ray_target_ui.SetActive (false);
		}

		if (Input.GetKeyDown (interact_button)) {
			if (Message_Controller.message_ctrl.currently_interacting == false) {
				interact ();
			} else {
				Debug.LogWarning ("Currently already interacting with an object");
			}
		}
	}

	void Target_GUI_Effet(string name){
		target_name.text = name;
	}

	void interact()
	{
		if (interactable == true) {
			Debug.Log ("Interacting with object");
			if (targeted_object_collider.CompareTag ("Agent") || targeted_object_collider.CompareTag ("Note")) {
				Message_Controller.message_ctrl.interacting_entity (targeted_object_collider.gameObject);
			}
			else if(targeted_object_collider.CompareTag ("Device")){
				// Different thing for interacting with device
				targeted_object_collider.gameObject.GetComponent<DeviceInfo>().Interact(); // Would prefer a generic EntityInfo.Interact() for everything but I am having some inheritance issues
			}
		} else if (targeted_object_collider != null) {
			Debug.Log ("Object is too far away to interact with");
		} else {
			Debug.Log ("No Object to interact with");
		}
	}
}
