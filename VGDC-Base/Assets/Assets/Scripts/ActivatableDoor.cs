using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivatableDoor : Activatable {

	public Light light;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	// Activates 
	public override void activate()
	{
		transform.Rotate (new Vector3 (0.0f, 90.0f, 0.0f));
		transform.position += new Vector3(-1.0f,0.0f,-0.5f);
	}
}
