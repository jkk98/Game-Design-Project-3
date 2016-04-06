﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

	public static GameController controller;

	/* Add vairables as necessary for existing through out the game */

	public GameObject player;

	public float max_health = 10; // Leaving health here as an option for the game.
	public float current_health = 10;

	//public int current_scene_index = 1;
	public string link_id; // id of object containing Position/Rotation for player to start out in loaded scene.

	public int reset_count = 0; // Keeping it public just to check in the inspector

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

	void OnLevelWasLoaded(int level){
		Transform starting_position = GameObject.Find (link_id).transform;
		player.transform.position = starting_position.position;
		player.transform.rotation = starting_position.rotation;
	}
}
