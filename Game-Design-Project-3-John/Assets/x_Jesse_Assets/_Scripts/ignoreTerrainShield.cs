using UnityEngine;
using System.Collections;

public class ignoreTerrainShield : MonoBehaviour {

	public GameObject cur_terrain;
	// Use this for initialization
	void Start () {
		Physics.IgnoreCollision (cur_terrain.transform.GetComponent<Collider> (), GetComponent<Collider> ());
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
