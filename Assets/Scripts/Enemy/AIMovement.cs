using UnityEngine;
using System.Collections;
using System.Collections.Generic; 

public class AIMovement : MonoBehaviour
{

		public float speed = 15;
		public Vector3 targetPosition;
		private Vector3 moveDirection = Vector3.zero;

		private AIPath path; 

		public Animator animator;
		public HashIds hash;

		public float attackInterval = 2.0f;
		public bool attackingHouse = false;
		private House house; 

		private float stopRange = 5.0f;

		//value for rotation
	    private Quaternion _lookRotation;
	    private Vector3 _direction;
		public float RotationSpeed = 10.0f;

		void Awake ()
		{	
				// give it an initial target position 
				targetPosition = transform.position;
				path = this.GetComponent<AIPath> ();
				
				hash = GameObject.FindGameObjectWithTag("GameController").GetComponent<HashIds>();
				animator = transform.Find("Model").gameObject.GetComponent<Animator>();
		}
	
		// Update is called once per frame
		void FixedUpdate ()
		{	
				if (path.player == null)
					return;

				if (attackingHouse) {

					if (Time.time % attackInterval < 0.5) {
						if (house == null) {
							// destroyed house
							attackingHouse = false;
							path.player.gameObject.GetComponent<CharacterControls>().setResting(false);
						}
						else 
							house.DamageHouse();
					}
					return;
				}

				if (path.player.gameObject.GetComponent<CharacterControls>().getResting()) {
								stopRange = 10.0f;
				}
				else {
					stopRange = 5.0f;
				}

				if (Vector3.Distance (path.player.position, this.transform.position) <= stopRange) {
						path.stop = true;
						idleState();
						if (Time.time % attackInterval < 0.5) {
						// if player is outside house, then attack house
							if (!path.player.gameObject.GetComponent<CharacterControls>().getResting())
								path.player.gameObject.GetComponent<CharacterControls>().damageStamina(35.0f);
						}
						return;
				}
				else { chaseState();}

				path.stop = false;


				if (targetPosition != Vector3.zero) {
						
					// rotate towards next path point
					//find the vector pointing from our position to the target
			        _direction = (targetPosition - transform.position).normalized;
			 
			        //create the rotation we need to be in to look at the target
			        _lookRotation = Quaternion.LookRotation(_direction);
			 
			        //rotate us over time according to speed until we are in the required rotation
			        transform.rotation = Quaternion.Slerp(transform.rotation, _lookRotation, Time.deltaTime * RotationSpeed);
						
				}
				transform.position = Vector3.MoveTowards (transform.position, targetPosition, speed * Time.deltaTime);

		}

		void OnTriggerEnter(Collider other ) {
			
			if (other.tag == "House") {
				attackingHouse = true;
				Debug.Log("gets here3");
				house = other.gameObject.GetComponent<House>();
			}
		}


		void OnTriggerExit(Collider other ) {
			if (other.tag == "House") 
				attackingHouse = false;
		}

		void chaseState() {
		animator.SetBool(hash.runningBool, true);
		animator.SetBool(hash.idleBool, false);
		//agent.SetDestination(playerLastSighting);
		//agent.speed = enemySpeed;
		}

		void alertState() {
			animator.SetBool(hash.idleBool, false);
			animator.SetBool(hash.runningBool, false);
			animator.SetBool(hash.alertBool, true);
			//agent.speed = 0;
		}

		void idleState() {
			animator.SetBool(hash.idleBool, true);
			animator.SetBool(hash.runningBool, false);
			//agent.speed = 0;
		}

		void attackState() {
			animator.SetBool(hash.attackBool, true);
			//What else should be done here?
		}

		void fleeState() {
			animator.SetBool(hash.runningBool, true);
			//agent.speed = enemySpeed;
		}

}
