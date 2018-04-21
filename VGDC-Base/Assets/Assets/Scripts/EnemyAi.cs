using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAi : MonoBehaviour {

	private UnityEngine.AI.NavMeshAgent navAgent;
	private Vector3 move;
	public string[] movements; 
	public int step = 0;

	// Use this for initialization
	void Start () {
		//Create reference to nav agent component
		navAgent= transform.GetComponent<UnityEngine.AI.NavMeshAgent> ();
		//Position will be set using grid move
		navAgent.updatePosition = false;
	}
	
	// Update is called once per frame
	void Update () {

		if (!gameObject.GetComponent<Possessable> ().isPossessed ()) {
			
			//Setting destination for testing
			navAgent.destination = new Vector3 (20.0f, 0.05f, 0.0f);

			//Setting next grid move required to reach destination
			determineMove ();

			//Attempt to call a new move, it will return 0 when the previous move is complete
			if (gameObject.GetComponent<GridMove> ().Move (move.x, move.z) != 1)
			//When the move completes, set the simulated position back to the real position
			navAgent.nextPosition = transform.position;
		}
	}

	void FixedUpdate(){

	}

	void determineMove(){
		
		//Use the simulated position of nav agent to determine movement direction
		move = navAgent.nextPosition - transform.position;

		//Normalize direction for determining dominant directions
		Vector3.Normalize (move);

		float moveX = 0.0f;
		float moveZ = 0.0f;

		if (move.x > 0.4f)
			moveX = 1.0f;
		if (move.x < -0.4f)
			moveX = -1.0f;
		
		if (move.z > 0.4f)
			moveZ = 1.0f;
		if (move.z < -0.4f)
			moveZ = -1.0f;
		
		move = new Vector3 (moveX,0.0f,moveZ);


	}
}
