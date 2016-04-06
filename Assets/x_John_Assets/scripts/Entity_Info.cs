using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Entity_Info : MonoBehaviour {

	public string entity_name;
	public string[] message_test;


	void Awake(){
		if (entity_name == "") {
			entity_name = gameObject.name;
		}
	}

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Interact(){
		// Does nothing for the moment for the superclass
	}
}
