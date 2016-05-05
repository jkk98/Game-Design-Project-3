using UnityEngine;
using System.Collections;

public class ignoreCollision : MonoBehaviour {

	public Transform other;
	//public Transform other1;


	// Use this for initialization
	void Start () {
		Physics.IgnoreCollision (other.GetComponent<Collider> (), GetComponent<Collider> ());
		//Physics.IgnoreCollision (other1.GetComponent<Collider> (), GetComponent<Collider> ());

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
