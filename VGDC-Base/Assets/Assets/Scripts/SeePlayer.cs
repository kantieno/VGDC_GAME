using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeePlayer : MonoBehaviour {

	public GameObject player;
	public bool canSee = false;
	public bool inCone = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		canSee = false;
		inCone = false;
		Vector3 dist = player.transform.position - transform.position;
		float playerAngle = Mathf.Atan2 (dist.x, dist.z) * Mathf.Rad2Deg;
		if (playerAngle < 0.0f)
			playerAngle = playerAngle + 360.0f;
		float lowerAngle = transform.rotation.eulerAngles.y;
		if (lowerAngle < 0.0f)
			lowerAngle = lowerAngle + 360.0f;
		lowerAngle = lowerAngle - 46.0f;
		float higherAngle = transform.rotation.eulerAngles.y;
		if (higherAngle < 0.0f)
			higherAngle = higherAngle + 360.0f;
		higherAngle = higherAngle + 46.0f;
		if (playerAngle > lowerAngle && playerAngle < higherAngle)
			inCone = true;
		if (lowerAngle < 0.0f) {
			if (playerAngle > lowerAngle + 360.0f)
				inCone = true;
		}
		if (higherAngle > 360.0f) {
			if (playerAngle < higherAngle - 360.0f)
				inCone = true;
		}


		if (inCone) {
			RaycastHit hit;
			if (Physics.Raycast (transform.position, player.transform.position - transform.position, out hit, 10.0f)) {

				//Check if the collided object has the possessable component
				GameObject obj = hit.collider.gameObject;
				if (obj == player) {

					//Possess the object
					canSee = true;

				}
			}
		}

	}

	public bool CanSeePlayer(){
		return canSee;
	}
}
