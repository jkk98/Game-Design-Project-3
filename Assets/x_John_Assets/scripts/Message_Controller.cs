using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Message_Controller : MonoBehaviour {

	public static Message_Controller message_ctrl;

	public string[] test_message;
	public int message_index = 0;

	public bool active_message = false;

	public Text message_content;
	public string message_type;


	// Makes sure there is a single MessageController that is created
	// at the beginning of the program
	void Awake () {
		if (message_ctrl == null) {
			Debug.Log ("Initiallizing New Message Controller");
			DontDestroyOnLoad (gameObject);
			message_ctrl = this;
		} 
		else if (message_ctrl != this) {
			Destroy (gameObject);
			Debug.Log ("Preventing duplicate Message Controller from being created");
		}
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		if (active_message == true) {
			if (Input.GetKeyDown ("return")) {
				if (message_index < test_message.Length) {
					message_content.text = test_message [message_index];
					message_index++;
				} else {
					message_content.text = "";
					active_message = false;
				}
			}
		}
	}

	public void new_message_list(string[] message_list, string type, int starting_index = 0){
		if (message_list.Length == 0) {
			Debug.LogWarning ("Message list appears to be empty");
		}
		else{
			test_message = message_list;
			message_index = starting_index;
			message_type = type;
			active_message = true;
		}
	}

}

