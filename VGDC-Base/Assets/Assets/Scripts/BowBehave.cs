using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowBehave : MonoBehaviour {

	public GameObject possessed;
	public GameObject player;
	public float yOffset;
	public float playerOffset;

	// Use this for initialization
	void Start () {
		//yOffset = gameObject.GetComponentInChildren<MeshRenderer> ().bounds.size.y / 2.0f;
		yOffset = 0.05f;
		playerOffset = 0.4f;
	}

	// Update is called once per frame
	void Update () {
		if (possessed != null) {
			float ySize = possessed.GetComponentInChildren<MeshRenderer> ().bounds.size.y / 2.0f;
			Vector3 pos = new Vector3(possessed.transform.position.x, 
				possessed.transform.position.y + ySize - yOffset, possessed.transform.position.z);
			transform.position = pos;
			transform.eulerAngles = new Vector3(transform.rotation.eulerAngles.x, possessed.transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);

		} else {
			float ySize = player.GetComponentInChildren<MeshRenderer> ().bounds.size.y / 2.0f;
			Vector3 pos = new Vector3(player.transform.position.x, 
				player.transform.position.y + ySize - playerOffset, player.transform.position.z);
			transform.position = pos;
			transform.eulerAngles = new Vector3(transform.rotation.eulerAngles.x, player.transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
		}
	}
}
