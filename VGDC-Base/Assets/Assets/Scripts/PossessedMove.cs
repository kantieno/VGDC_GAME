using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PossessedMove : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if (gameObject.GetComponent<Possessable>().isPossessed())
			gameObject.GetComponent<GridMove> ().Move (Input.GetAxis ("Horizontal"), Input.GetAxis ("Vertical"));
	}
}
