using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Possessable : MonoBehaviour {

	private bool possessed = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public bool isPossessed(){
		return possessed;
	}

	public void possess(){
		possessed = true;
	}

	public void depossess(){
		possessed = false;
	}
}
