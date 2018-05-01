using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Possess : MonoBehaviour {

	public GameObject player;
	public GameObject bow;
	public Possessable possessedObject = null;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown ("space")) {

			//If an object if not currently possed, perform a raycast to check for possessable object
			if (possessedObject == null) {
				//Send a Raycast originating from the player to see if an object is within range
				RaycastHit hit;
				if (Physics.Raycast (player.transform.position, player.transform.TransformDirection (Vector3.forward), out hit, 1.0f)) {

					//Check if the collided object has the possessable component
					possessedObject = hit.collider.gameObject.GetComponent<Possessable> ();
					if (possessedObject != null) {

						//Possess the object
						possessedObject.GetComponent<Possessable> ().possess ();

						//Set the active player representation
						player.SetActive(false);
						//bow.SetActive (true);
						bow.GetComponent<BowBehave>().possessed = possessedObject.gameObject;

					}
				}

			//If an object is possessed, depossess it
			} else {

				//Reactivate the player and update its position
				player.SetActive (true);
				player.GetComponent<GridMove> ().setTarget (possessedObject.gameObject.GetComponent<GridMove>().getGridPosition());
				player.GetComponent<GridMove> ().setGridPosition (possessedObject.gameObject.GetComponent<GridMove>().getGridPosition());
				player.transform.position = possessedObject.gameObject.GetComponent<GridMove>().getGridPosition();
				//bow.SetActive (false);
				bow.GetComponent<BowBehave>().possessed = null;

				//Depossess the object and clear its value
				possessedObject.GetComponent<Possessable> ().depossess ();
				possessedObject = null;

			}
		}
	}
}
