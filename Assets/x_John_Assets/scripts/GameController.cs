using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

	public static GameController controller;

	/* Add vairables as necessary for existing through out the game */

	public float max_health = 10; // Leaving health here as an option for the game.
	public float current_health = 10;

	public int current_scene_index = 1; 


	void load_scene(string scene_name){

		// Change current_scene_index to the index corresponding to the scene name
		// load the new scene
	}

	// Makes sure there is a single GameController that is created
	// at the beginning of the program
	void Awake () {
		if (controller == null) {
			Debug.Log ("Initiallizing New Game Controller");
			DontDestroyOnLoad (gameObject);
			controller = this;
		} 
		else if (controller != this) {
			Destroy (gameObject);
			Debug.Log ("Preventing duplicate Game Controller from being created");
		}
	}

	// Using GUI only to test variables
	void OnGUI()
	{

	}

	void FixedUpdate()
	{
		
	}
}
