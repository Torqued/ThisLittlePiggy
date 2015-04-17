using UnityEngine;
using System.Collections;

public class EnemyClass : MonoBehaviour {

	//Enemy State Variables
	public enum enemyState {
		IDLE, ALERT, CHASE, ATTACK, FLEE,
	};

	public enemyState currentState;

	//Animation Variables
	private GameObject enemyModel;
	private GameObject gameController;
	public Animator animator;
	public HashIds hash;

	//Movement Variables
	NavMeshAgent agent;
	public float enemySpeed = 10f;

	//Player Detection Variables
	private Vector3 playerCurrentPos;
	private Vector3 playerLastSighting;
	private SphereCollider col;
	public bool playerInSight = false;
	public float fieldOfViewAngle = 90f;
	public float attackRadius = 30.0f;

	private EnemyBehaviour behavior; 

	// Use this for initialization
	void Awake() {
		gameController = GameObject.FindGameObjectWithTag("GameController");

		behavior = this.gameObject.GetComponent<EnemyBehaviour>();

		Transform modelTransform = transform.Find("Model");
		if (modelTransform == null) {
			Debug.LogError("This object is missing a child object called \"Model\".");
			Debug.Break();
		}
		enemyModel = modelTransform.gameObject;
		hash = gameController.GetComponent<HashIds>();
		animator = enemyModel.GetComponent<Animator>();
		agent = GetComponent<NavMeshAgent>();
		if (agent == null) {
			Debug.LogError("This object is missing a NavMeshAgent Component!");
			Debug.Break();
		}
		col = GetComponent<SphereCollider>();
		if (col == null) {
			Debug.LogError("Please put a large sphere trigger on this enemy to act as its sensory range.");
			Debug.Break();
		}

		agent.speed = enemySpeed;
		
	}

	void OnTriggerStay(Collider other) {
		if(other.gameObject.tag == "Player") {
			playerCurrentPos = other.transform.position;
			detectPlayer();

		}
	}

	void OnTriggerExit(Collider other) {
		//If the player leaves the sensory collider
		if(other.gameObject.tag == "Player") {
			playerInSight = false; //They are no longer detected.
			//hash.playerSpotted = false;
			if (!behavior.flee) {
				behavior.Wander();
			}
		}
	}

	void FixedUpdate() {
		if (playerInSight && !behavior.flee) {
			// if wolf sees player and not daytime, chase player
			Debug.Log ("Chasing");
			behavior.Chase(playerLastSighting);
		}
	}


	void detectPlayer() {
		//Sets the playerInSight variable to true or false.
		Vector3 direction = playerCurrentPos - transform.position;
		float angle = Vector3.Angle(direction, transform.forward);
		if(angle < fieldOfViewAngle * 0.5f) {
			RaycastHit hit; //Send out a Raycast

			if(Physics.SphereCast(transform.position, 10.0f, 
			                   direction.normalized, out hit, attackRadius*2)) {
				if (hit.collider.gameObject.tag == "Player") {
					playerInSight = true; //Spotted! Run!
					//hash.playerSpotted = true;
					playerLastSighting = playerCurrentPos;
				}
			}
			Debug.DrawRay(transform.position,direction, Color.green);
		}
	}


	void chaseState() {
		animator.SetBool(hash.runningBool, true);
		animator.SetBool(hash.idleBool, false);
		agent.SetDestination(playerLastSighting);
		agent.speed = enemySpeed;
	}

	void alertState() {
		animator.SetBool(hash.idleBool, false);
		animator.SetBool(hash.runningBool, false);
		animator.SetBool(hash.alertBool, true);
		agent.speed = 0;
	}

	void idleState() {
		animator.SetBool(hash.idleBool, true);
		animator.SetBool(hash.runningBool, false);
		agent.speed = 0;
	}

	void attackState() {
		animator.SetBool(hash.attackBool, true);
		//What else should be done here?
	}

	void fleeState() {
		animator.SetBool(hash.runningBool, true);
		agent.speed = enemySpeed;
	}



}
