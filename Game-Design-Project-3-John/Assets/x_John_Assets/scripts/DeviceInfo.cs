using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class DeviceInfo : Entity_Info {

	public enum DeviceType {generic, door, other}; // will need to come up with device ideas later if ever

	public DeviceType device = DeviceType.generic;
	public bool locked = false; // boolean for if this device can be used

	public int integer_value; // currently have generic variables to be used differently by different Device types
	public string string_value; // This is way is bad coding practice but it is easier for the moment than chaning the inspector
	//public string link_id; // Name of game object in next where player will spawn

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public new void Interact(){
		Debug.Log("Interacting with Device");
		// Checks the device type. Could also just use a switch statement here
		if (device == DeviceType.generic) {
			// do nothing for the moment
		} else if (device == DeviceType.door) {
			UseDoor ();
		} else if (device == DeviceType.other) {
			// do nothing for the moment
		}
	}

	void UseDoor(){
		Debug.Log ("Loading Scene: " + string_value);
		//GameController.controller.link_id = link_id;
		SceneManager.LoadScene (string_value);
	}
}
