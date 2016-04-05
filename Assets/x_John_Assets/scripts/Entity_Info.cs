using UnityEngine;
using System.Collections;

public class Entity_Info : MonoBehaviour {

	public string entity_name;


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
}
