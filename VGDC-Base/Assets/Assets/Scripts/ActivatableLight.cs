using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivatableLight : Activatable {

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
		light.enabled = !light.enabled;
	}
}
