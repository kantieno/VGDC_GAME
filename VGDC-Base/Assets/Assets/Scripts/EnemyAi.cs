using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAi : MonoBehaviour {

	private UnityEngine.AI.NavMeshAgent navAgent;
	private Vector3 move;
	private float waitTime = 0.0f;
	private SeePlayer viewer;
	private Vector3 dest;
	public string[] movements; 
	public int step = 0;
	public float speed = 100.0f;
	public float panickedSpeed = 150.0f;
	public bool panicked = false;
	public GameObject exit;



	// Use this for initialization
	void Start () {
		//Create reference to nav agent component
		navAgent= transform.GetComponent<UnityEngine.AI.NavMeshAgent> ();
		//Position will be set using grid move
		navAgent.updatePosition = false;

		string[] currentMove = movements [step].Split (',');
		if (currentMove [0] == "w") {
			waitTime = Time.fixedTime + float.Parse (currentMove [1]);
			step++;
			if (step >= movements.Length)
				step = 0;
		} 
		//Do not put two waits in a row
		currentMove = movements [step].Split (',');
		dest = new Vector3 (float.Parse (currentMove [0]), 0.0f, float.Parse (currentMove [1]));
		step++;

		viewer = gameObject.GetComponent<SeePlayer> ();

	}
	
	// Update is called once per frame
	void Update () {
		if (viewer.CanSeePlayer ()) {
			panicked = true;
			dest = exit.transform.position;
			gameObject.GetComponent<GridMove> ().speed = panickedSpeed;
			//navAgent.speed = panickedSpeed;
		}
		if (!gameObject.GetComponent<Possessable> ().isPossessed ()) {

			if (waitTime <= Time.fixedTime || panicked) {

				if (!panicked){
					if (dest.x == GetComponent<GridMove> ().getPosition ().x && dest.z == GetComponent<GridMove> ().getPosition ().z) {
						string[] currentMove = movements [step].Split (',');
						if (currentMove [0] == "w") {
							waitTime = Time.fixedTime + float.Parse (currentMove [1]);
							step++;
							if (step >= movements.Length)
								step = 0;
						} 
						currentMove = movements [step].Split (',');
						dest = new Vector3 (float.Parse (currentMove [0]), 0.0f, float.Parse (currentMove [1]));
						step++;
						if (step >= movements.Length)
							step = 0;
					}
				}


				//Setting destination for testing
				navAgent.destination = dest;

				//Setting next grid move required to reach destination
				determineMove ();

				//Attempt to call a new move, it will return 0 when the previous move is complete
				if (gameObject.GetComponent<GridMove> ().Move (move.x, move.z) != 1)
			//When the move completes, set the simulated position back to the real position
			navAgent.nextPosition = transform.position;
			}

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
