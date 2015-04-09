using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyBehaviour : MonoBehaviour {

	// variables for moving around
	Transform transformer;
	bool change;
	float range;
	public Vector3 moveDirection;
	public Vector3 target;
	float velocity;
	public float rotationSpeed = 200.0f;
	public float speed = 5;
	
	
	// Variables for raycasting to avoid obstacles
	RaycastHit hit;
	public int rayRange = 10;
	public float rayRotationSpeed = 100.0f;
	private bool rayObstacle = false;
	
	// Unity Game Components 
	GameObject[] allWolves;
	GameObject[] allObstacles;
	GameObject[] allGoals; 
	
	// variables to switch between the 3 behaviours
	public bool wander;
	public bool reachGoal;
	
	// Use this for initialization
	void Start ()
	{
		
		transformer = GetComponent<Transform> ();
		allWolves = GameObject.FindGameObjectsWithTag ("Wolf");
		//allObstacles = GameObject.FindGameObjectsWithTag ("Obstacles");
		range = 5.0f;
		if (wander) {
			target = GetTarget ();
			
			int wanderInterval = Random.Range (3, 6);
			float interval = (float)wanderInterval;
			InvokeRepeating ("NewTarget", 0.01f, interval);
			
			moveDirection = (target - transformer.position).normalized;
		}
		
		if (reachGoal) {
			allGoals = GameObject.FindGameObjectsWithTag ("Goal");
			var min = 200f;
			var current = 0f;
			// there are 4 goals, so each character finds nearest goal and heads for it
			foreach (var goal in allGoals) {
				current = Vector3.Distance (goal.transform.position, transformer.position);
				if (current < min) {
					min = current; 
					target = goal.transform.position;
				}
				moveDirection = (target - transformer.position).normalized;
			}			
		}
	}

	// uses separation behavior to avoid other characters. 
	void AvoidOtherCharacters ()
	{
		// for each character in the list that is not this character, we check if it is in this character's line of sight. 
		// if it is, this character turns right to avoid the other characters. 
		// only check for line of sight if character is near enough
		
		foreach (GameObject character in allWolves) {
			// first check that the character is not checking itself. 
			if (!GameObject.Equals (this, character)) {
				// now check that the other character is near enough for a possible collision
				float distance = Vector3.Distance (transformer.position, character.transform.position);
				
				if (distance > 0 && distance < 5f) {
					if (Vector3.Dot ((transformer.position - character.transform.position).normalized, transformer.forward) > 0) {
						// we found a character in our line of sight that we could hit. 
						// so we turn away from it 
						// calculate vector going away from this object
						// use separation behavior
						Vector3 direction = (transform.position - character.transform.position).normalized;	
						// give it a weight of the distance, so if is further away, it moves less
						direction = direction / distance;
						moveDirection += direction;
						
					}
				}
			}
		}
		
	}
	
	// helps to avoid round obstacles better using separation behavior.
	void AvoidRoundObstacles ()
	{
		// for each character in the list that is not this character, we check if it is in this character's line of sight. 
		// if it is, this character turns right to avoid the other characters. 
		// only check for line of sight if character is near enough

		foreach (GameObject obstacle in allObstacles) {
			// now check that the obstacle is near enough for a possible collision
			float distance = Vector3.Distance (transformer.position, obstacle.transform.position);
			if (distance > 0 && distance < 3f) {
				if (Vector3.Dot ((transformer.position - obstacle.transform.position).normalized, transformer.forward) > 0) {
					// we found a obstacle in our line of sight that we could hit. 
					// so we turn away from it 
					// calculate vector going away from this object
					// use separation behavior
					Vector3 direction = (transform.position - obstacle.transform.position).normalized;	
					// give it a weight of the distance, so if is further away, it moves less
					direction = direction / distance;
					moveDirection += direction;
					
				}
			}
		}
	}
	
	// function to cast rays so that character avoids static obstacles. 
	bool MakeRaycasts ()
	{
		// ray-casting code for static obstacle avoidance		
		// check for obstacles in front of a wolf
		// has 3 rays to detect obstacles 
		moveDirection = (target - transformer.position).normalized;
		bool hitRay = false;
		// right ray and left ray
		if (Physics.Raycast (transformer.position + transformer.right, transformer.right + transformer.forward, out hit, rayRange)) {
			if (hit.collider.gameObject.CompareTag ("Obstacle")) {
				if (wander && Vector3.Distance (hit.transform.position, target) <= range)
					target = GetTarget ();
				rayObstacle = true;
				hitRay = true;
				transformer.Rotate (-Vector3.up * Time.deltaTime * rayRotationSpeed);
				transformer.Rotate (-Vector3.right * Time.deltaTime * rayRotationSpeed);
			}
		} else if (Physics.Raycast (transformer.position - transformer.right, - transformer.right + transformer.forward, out hit, rayRange)) {
			if (hit.collider.gameObject.CompareTag ("Obstacle")) {
				if (wander && Vector3.Distance (hit.transform.position, target) <= range)
					target = GetTarget ();
				rayObstacle = true;
				hitRay = true;
				transformer.Rotate (Vector3.up * Time.deltaTime * rayRotationSpeed);
				transformer.Rotate (-Vector3.right * Time.deltaTime * rayRotationSpeed);
			}
		} 
		
		
		// We also add two more rays to the back of the object. This is needed when we are not turning around corners
		// but still trying to reach the corners of the obstacle.
		
		else if (Physics.Raycast (transformer.position - (transformer.forward), transformer.right, out hit, 5)) {
			if (hit.collider.gameObject.CompareTag ("Obstacle")) {
				
				if (wander && Vector3.Distance (hit.transform.position, target) <= range)
					target = GetTarget ();
				rayObstacle = true;
				hitRay = true;
				transformer.Rotate (-Vector3.right * Time.deltaTime * rayRotationSpeed);
			}
		} else if (Physics.Raycast (transformer.position - (transformer.forward), -transformer.right, out hit, 5)) {
			if (hit.collider.gameObject.CompareTag ("Obstacle")) {
				
				if (wander && Vector3.Distance (hit.transform.position, target) <= range)
					target = GetTarget ();
				rayObstacle = true;
				hitRay = true;
				transformer.Rotate (-Vector3.right * Time.deltaTime * rayRotationSpeed);
			}
		}
		/*
				// Use to debug the Physics.RayCast.
				Debug.DrawRay (transformer.position + (transformer.right), transformer.right + transformer.forward * rayRange, Color.red);
				Debug.DrawRay (transformer.position - (transformer.right), -transformer.right + transformer.forward * rayRange, Color.red);
		
				Debug.DrawRay (transformer.position - (transformer.forward), - transformer.right * 5, Color.blue);
		
				Debug.DrawRay (transformer.position - (transformer.forward), transformer.right * 5, Color.blue);

				*/
		return hitRay;
	}
	
	void Move ()
	{
		// change the rotation of the character towards the target if we are not avoiding 
		// an obstacle.
		moveDirection = (target - transformer.position).normalized;

		if (!rayObstacle) {
			// using quarternions instead of euler angles in the end to avoid gimbal lock effect
			var newRotation = Quaternion.LookRotation (moveDirection);
			transform.rotation = Quaternion.Slerp (transform.rotation, newRotation, Time.deltaTime);
			/*
						var newRotation = Quaternion.LookRotation (moveDirection).eulerAngles;
						var angles = transformer.rotation.eulerAngles;
						transformer.rotation = Quaternion.Euler (0.0f, 
			                                         Mathf.SmoothDampAngle (angles.y, newRotation.y, ref velocity, 0.1f, rotationSpeed),
			                                         0.0f);
			*/

		}
		
		
		transformer.position += transformer.forward * speed * Time.deltaTime;
		
	}
	
	void  FixedUpdate ()
	{   	
		
		if (Time.time > 5)
			AvoidOtherCharacters ();
		
		//AvoidRoundObstacles ();

		if (!MakeRaycasts ()) {
			rayObstacle = false;
		} else
			rayObstacle = true;
	
		if (Vector3.Distance (transformer.position, target) > range) {
			Move ();
		} else if (wander) {
			target = GetTarget ();
		} 
		
	}
	
	void Update ()
	{
	}
	
	Vector3 GetTarget ()
	{
		// set random initial target, and change this target
		int x = Random.Range (0, 40);
		int z = Random.Range (0, 40);
		
		int check_sign_x = Random.Range (0, 2);
		int check_sign_z = Random.Range (0, 2);
		
		if (check_sign_x > 0)
			x *= -1;
		if (check_sign_z > 0)
			z *= -1;
		
		Vector3 final_vector = new Vector3 (x, 0.0f, z);
		return final_vector;
	}
	
	void NewTarget ()
	{
		// gets a new target based on chance. Rolls a random number. 
		// if 0 - change target
		// if 1 - continue with current target
		int choice = Random.Range (0, 2);
		
		switch (choice) {
		case 0: 
			change = true;
			break;
		case 1: 
			change = false;
			break;
		} 
		
		// if true, we get a new target
		if (change) 
			target = GetTarget ();
	}
}
