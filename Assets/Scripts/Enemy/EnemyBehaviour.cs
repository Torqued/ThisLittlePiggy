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
	
	
	/* Variables for raycasting to avoid obstacles
	RaycastHit hit;
	public int rayRange = 10;
	public float rayRotationSpeed = 100.0f;
	private bool rayObstacle = false;
	*/

	
	// variables to switch between the 3 behaviours
	public bool wander;
	public bool flee;
	public bool chase;

	// variables to limit wolf wander range 
	public int wander_radiusX = 20;
	public int wander_radiusZ = 20;

	NavMeshAgent agent2;

	// Use this for initialization
	void Start ()
	{
		
		transformer = GetComponent<Transform> ();
		range = 5.0f;
		if (wander) {
			Wander();
		}
		agent2 = GetComponent<NavMeshAgent>();

	}

	public void Wander () {
		wander = true;
		flee = false;
		chase = false;
		target = GetTarget ();
		
		int wanderInterval = Random.Range (3, 6);
		float interval = (float)wanderInterval;
		InvokeRepeating ("NewTarget", 0.01f, interval);
		
		moveDirection = (target - transformer.position).normalized;
		//agent2.SetDestination(target);
	}

	public void Flee(Transform goal) {
		wander = false; 
		flee = true;
		chase = false;
		target = goal.position;
		moveDirection = (target - transformer.position).normalized;
		//agent2.SetDestination(target);
	}

	public void Chase(Vector3 player) {
		wander = false;
		flee = false; 
		chase = true;
		target = player;
		moveDirection = (target - transformer.position).normalized;
		//agent2.SetDestination (player);
	}

	
	void Move ()
	{
		// change the rotation of the character towards the target if we are not avoiding 
		// an obstacle.
		moveDirection = (target - transformer.position).normalized;

		// using quarternions instead of euler angles in the end to avoid gimbal lock effect
		var newRotation = Quaternion.LookRotation (moveDirection);
		transform.rotation = Quaternion.Slerp (transform.rotation, newRotation, Time.deltaTime);

		
		transformer.position += transformer.forward * speed * Time.deltaTime;
		
	}
	
	void  FixedUpdate ()
	{   	


		if (Vector3.Distance (transformer.position, target) > range) {
			Move ();
		} else if (wander) {
			target = GetTarget ();
		} else if (flee) {
			GameObject.Destroy(this.gameObject);
		}
		
	}
	
	void Update ()
	{
	
	}
	
	Vector3 GetTarget ()
	{
		// set random initial target, and change this target
		int x = Random.Range (0, wander_radiusX);
		int z = Random.Range (0, wander_radiusZ);
		
		int check_sign_x = Random.Range (0, 2);
		int check_sign_z = Random.Range (0, 2);
		float x_float = x * 1.0f;
		float z_float = z * 1.0f;
		if (check_sign_x > 0)
			x_float *= -1.0f;
		if (check_sign_z > 0)
			z_float *= -1.0f;
		
		Vector3 final_vector = new Vector3 (transformer.position.x - x_float, transform.position.y,transformer.position.z -  z_float);
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
