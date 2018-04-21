using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activatable : MonoBehaviour {

	public Activatable activation;

	// Use this for initialization
	void Start () {
		
	}

	public virtual void activate()
	{
		activation.activate ();
	}
}
