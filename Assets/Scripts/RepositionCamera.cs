using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepositionCamera : MonoBehaviour {

	public Transform cameraPivot;

	// Use this for initialization
	void Start () {
		cameraPivot.position = new Vector3 (0.2f, -1.5f, -0.1f);
		cameraPivot.Rotate (0, 0, 0);
		
	}
	
	// Update is called once per frame
	void Update () {

		var offset = new Vector3 ();
		float angle = 0;

		if (Input.anyKey) {

			if (Input.GetKeyDown ("w")) {
				offset.z = 1;
			} else if (Input.GetKeyDown ("s")) {
				offset.z = -1;
			} else if (Input.GetKeyDown ("a")) {
				offset.x = -1;
			} else if (Input.GetKeyDown ("d")) {
				offset.x = 1;
			} else if (Input.GetKeyDown ("q")) {
				offset.y = -1;
			} else if (Input.GetKeyDown ("e")) {
				offset.y = 1;
			} else if (Input.GetKeyDown ("z")) {
				angle = -15;
			} else if (Input.GetKeyDown ("x")) {
				angle = 15;
			}
			cameraPivot.position += offset * 0.05f;
			cameraPivot.Rotate (0, angle, 0);
			Debug.Log (cameraPivot.position);
			Debug.Log (cameraPivot.eulerAngles);
		}


	}
}
