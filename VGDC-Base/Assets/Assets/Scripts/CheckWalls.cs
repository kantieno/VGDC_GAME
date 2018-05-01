using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckWalls : MonoBehaviour {

	public GameObject cam;
	public GameObject player;
	public Material wallOpaque;
	public Material wallTransparent;
	public GameObject wall;

	// Use this for initialization
	void Start () {
		//player = gameObject;
	}
	
	// Update is called once per frame
	void Update () {

		if (wall != null) {
			wall.GetComponent<MeshRenderer>().material = wallOpaque;
			wall = null;
		}

		RaycastHit hit;
		if (Physics.Raycast (player.transform.position, cam.transform.position - player.transform.position, out hit, 100.0f)) {

			//Check if the collided object has the possessable component
			wall = hit.collider.gameObject;
			if (wall != null) {
				if (wall.tag == "Wall") {
					//Possess the object
					wall.GetComponent<MeshRenderer> ().material = wallTransparent;
				}

			}
		}
	}
}
