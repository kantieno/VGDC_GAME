using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activate : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown ("Activate")) {
			RaycastHit hit;
			if (Physics.Raycast (transform.position, transform.TransformDirection (Vector3.forward), out hit, 1.0f)) {

				//Check if the collided object has the possessable component
				Activatable act = hit.collider.gameObject.GetComponent<Activatable> ();
				if (act != null) {
					
					//Possess the object
					act.activate ();

				}
			}
		}
	}
}
