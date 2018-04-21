using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour {

	public int GRID_WIDTH = 24;
	public int GRID_HEIGHT = 24;
	public Material mat;

	// Use this for initialization
	void Start () {
		for (int i = 0; i < GRID_WIDTH; i++) {
			for (int j = 0; j < GRID_HEIGHT; j++) {
				GameObject tile = (GameObject)Instantiate (Resources.Load ("Prefabs/Tile"));
				if ((i + j) % 2 == 0)
					tile.GetComponent<MeshRenderer> ().material = mat;
				tile.transform.position = new Vector3 ((float)i, 0.05f, (float)j);
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
