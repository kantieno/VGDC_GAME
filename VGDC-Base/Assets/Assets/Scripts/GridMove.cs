using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridMove : MonoBehaviour {

	public float speed = 10.0f;

	private const int DIR_BUFFER_MAX = 5;
	private const int MOVE_ATTEMPT_TIME_MAX = 100;
	private bool moving = false;
	private int dirBuffer = DIR_BUFFER_MAX;
	private int moveAttemptTime = MOVE_ATTEMPT_TIME_MAX;
	private Vector3 gridPosition;
	private Vector3 target;
	private Vector3 dir;
	private Rigidbody rb;
	private float rotationSpeed = 10.0f;

	// Use this for initialization
	void Start () {
		rb = this.gameObject.GetComponent<Rigidbody> ();
		gridPosition = new Vector3(Mathf.Round (transform.position.x), Mathf.Round (transform.position.y),  Mathf.Round (transform.position.z));
	} 

	// FixedUpdate is called for physics
	void FixedUpdate () {
		//Objects using GridMove should always be upright and at the level of the plane
		//transform.rotation = Quaternion.Euler(Vector3.zero);
		transform.position = new Vector3 (transform.position.x, gridPosition.y, transform.position.z);

		//Moving is true as long as the target position is still trying to be reached
		if (moving) {

			//Stop trying to move if the target cannot be reached
			//Temporary Function until proper movement checking is added
			if (moveAttemptTime > 0)
				moveAttemptTime--;
			else {
				if(Mathf.Abs(transform.position.x - gridPosition.x) + Mathf.Abs(transform.position.z - gridPosition.z) > 0.9f)
					gridPosition = new Vector3(Mathf.Round (transform.position.x), Mathf.Round (transform.position.y),  Mathf.Round (transform.position.z));
				target = gridPosition;
			}
			
			//If not within a certain range of the target, move towards target
			if (Mathf.Abs (transform.position.x - target.x) < 0.05f &&
			    Mathf.Abs (transform.position.z - target.z) < 0.05f) {

				//When close enough to the target update gridposition and stop
				gridPosition = new Vector3 (target.x, transform.position.y, target.z);
				transform.position = gridPosition;
				moving = false;
				rb.velocity = Vector3.zero;

			} else {
				
				//Move towards target
				rb.velocity = Vector3.Normalize(new Vector3 ((target.x - transform.position.x), 0.0f, 
					(target.z - transform.position.z))) * Time.deltaTime * speed;
			}

		}

		/**if (dir != Vector3.zero){
			transform.rotation = Quaternion.Slerp(
				transform.rotation,
				Quaternion.LookRotation(dir),
				Time.deltaTime * rotationSpeed
			);
		}**/
		if (dir != Vector3.zero)
			transform.rotation = Quaternion.LookRotation (dir);
	}

	public int Move(float x, float z){

		//If the input is 0,0 do nothing
		if (x == 0.0f && z == 0.0f) {
			rb.velocity = Vector3.zero;
			return 1;
		}

		//If move is called and the object is not already moving
		if (!moving) {
			
			if(Mathf.Abs(transform.position.x - gridPosition.x) + Mathf.Abs(transform.position.z - gridPosition.z) > 0.9f)
				gridPosition = new Vector3(Mathf.Round (transform.position.x), Mathf.Round (transform.position.y),  Mathf.Round (transform.position.z));

			//Update target location to current position plus direction
			target = new Vector3 (gridPosition.x + x, transform.position.y, gridPosition.z + z);
			dir = new Vector3 (x, 0.0f, z);
			moving = true;
			//Set the buffer time to check for multiple button presses
			dirBuffer = DIR_BUFFER_MAX;
			moveAttemptTime = MOVE_ATTEMPT_TIME_MAX;
			return 0;

		} else {

			//Buffer allows for diagonal movement to happen without 
			//frame perfect press of both directions
			if (dirBuffer > 0) {
				if (x != 0 || z != 0) {
					if (!(dir.x == 1.0f && dir.z == 1.0f)) {
						if (dir.x == x && dir.z != z) {
							dir.z = z;
							target.z = target.z + dir.z;
						}
						if (dir.z == z && dir.x != x) {
							dir.x = x;
							target.x = target.x + dir.x;
						}
					}
				}
				dirBuffer--;
			}
				
			return 1;	
		}
	}

	public Vector3 getDirection(){
		return dir;
	}

	public Vector3 getGridPosition(){
		return gridPosition;
	}

	public void setGridPosition(Vector3 pos){
		gridPosition = pos;
	}

	public Vector3 getTarget(){
		return target;
	}

	public void setTarget(Vector3 tar){
		target = tar;
	}
}
