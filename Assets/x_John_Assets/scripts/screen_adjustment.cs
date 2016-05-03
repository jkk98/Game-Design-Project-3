using UnityEngine;
using System.Collections;

public class screen_adjustment : MonoBehaviour {

	Camera cam;

	// Use this for initialization
	void Start () {
	
		cam = gameObject.GetComponent<Camera> ();
		Debug.Log ("Resolution: " + Screen.currentResolution);
		Debug.Log ("Width/Height : " + Screen.width + " " + Screen.height);

		//Screen.SetResolution(3072, 768, true);
		Debug.Log ("Resolution: " + Screen.currentResolution);
		Debug.Log ("Width/Height : " + Screen.width + " " + Screen.height);
		//cam.pixelRect = new Rect(Screen.width

	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetButtonDown ("Jump")) {
			// choose the margin randomly
			//var margin = Random.Range (0.0f, 0.3f);
			// setup the rectangle
			cam.rect = new Rect (0f, 0.25f, 1f, .5f);
		}

		/*if (Input.GetKeyDown ("return")) {
			// choose the margin randomly
			//var margin = Random.Range (0.0f, 0.3f);
			// setup the rectangle
			Screen.SetResolution(3072, 768, true);
			cam.pixelRect = new Rect (0, 128, 3072, 768);
		}*/
	}
}
