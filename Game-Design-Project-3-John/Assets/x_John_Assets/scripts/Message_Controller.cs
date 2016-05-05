using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class Message_Controller : MonoBehaviour {

	public static Message_Controller message_ctrl;

	public string[] current_message_list;
	public string message_type;
	public int message_index = 0;
	public GameObject current_entity; // Current entity one is interacting with
	public bool currently_interacting = false; // flag if currently interacting with a gameobject

	public bool active_message = false;

	public Text message_content;
	public GameObject message_panel;
	public GameObject letterbox; // A somewhat redundant setup to manipulate UI elements for testing purposes
	public GameObject player;
	private RigidbodyFirstPersonController player_FP_ctrl;

	private bool is_typing = false;
	private bool cancel_typing;
	public float type_speed = 1.0f;
	public KeyCode activate_key = KeyCode.Tab;


	// Makes sure there is a single MessageController that is created
	// at the beginning of the program
	void Awake () {
		if (message_ctrl == null) {
			Debug.Log ("Initiallizing New Message Controller");
			//DontDestroyOnLoad (gameObject);
			message_ctrl = this;
		} 
		else if (message_ctrl != this) {
			Destroy (gameObject);
			Debug.Log ("Preventing duplicate Message Controller from being created");
		}
	}

	// Use this for initialization
	void Start () {
		message_panel = GameObject.Find ("Message Panel");
		letterbox = GameObject.Find ("Letter Box");

		message_panel.SetActive (false);
		letterbox.SetActive (false);
		player_FP_ctrl = player.GetComponent<RigidbodyFirstPersonController> ();
	}
	
	// Update is called once per frame
	void Update () {

		if (!is_typing) {
			if (message_index < current_message_list.Length) {
				//message_content.text = current_message_list[message_index];
				StartCoroutine (TextScroll (current_message_list [message_index]));
				message_index++;
			}
		}

		if (active_message == true) {
			if (Input.GetKeyDown (activate_key)) {
				// Check if text is not currently being written across the panel
				if (!is_typing) {
					if (message_index < current_message_list.Length) {
						//message_content.text = current_message_list[message_index];
						StartCoroutine(TextScroll(current_message_list[message_index]));
						message_index++;

					} else {
						message_content.text = "";
						active_message = false;
						currently_interacting = false; // Ending interaction for sake of testing at end of message list
						message_panel.SetActive (false);
						//letterbox.SetActive (false);

						// Disables the mouse and enables the first person controller
						player_FP_ctrl.mouseLook.SetCursorLock(true);
						player_FP_ctrl.enabled = true;
					}
				} else {
					cancel_typing = true; // Stops the scrolling text and displays the entire message
				}
			}
		}
	}

	public void new_message_list(string[] message_list, string type, int starting_index = 0){
		if (message_list.Length == 0) {
			Debug.LogWarning ("Message list appears to be empty");
		}

		current_message_list = message_list;
		message_index = starting_index;
		message_type = type;
		active_message = true;

	}

	// Begin a new interaction with an entity
	public void interacting_entity(GameObject entity){
		if (currently_interacting == false) {
			current_entity = entity;
			string[] test_message = current_entity.GetComponent<Entity_Info> ().message_test;
			new_message_list (test_message, entity.tag);
			currently_interacting = true;
			message_panel.SetActive (true);
			//letterbox.SetActive (true);

			// Enables the mouse and disables the first person controller
			player_FP_ctrl.mouseLook.SetCursorLock(false);
			player_FP_ctrl.enabled = false;
		} else {
			Debug.LogWarning ("Currently already interacting with an object");
		}
	}

	// Coroutine to handle scrolling text (based on tutorial in youtube link)
	private IEnumerator TextScroll (string message){
		int letter_index = 0;
		message_content.text = "";
		is_typing = true;
		cancel_typing = false;
		// Go through each letter in the message and displaying them based on the speed
		while (is_typing && !cancel_typing && (letter_index < message.Length)) {
			message_content.text += message [letter_index];
			letter_index++;
			yield return new WaitForSeconds (type_speed); // Manages the speed for the scrolling text
		}
		message_content.text = message; // Displays the whole line if canceled typing or the while loop finished
		is_typing = false;
	}

}

