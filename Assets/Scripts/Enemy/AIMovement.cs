using UnityEngine;
using System.Collections;
using System.Collections.Generic; 

public class AIMovement : MonoBehaviour
{

		public float speed = 15;
		public Vector3 targetPosition;
		private Vector3 moveDirection = Vector3.zero;
		float rotateSpeed = 1000.0f;

		private AIPath path; 

		public Animator animator;
		public HashIds hash;

		public float attackInterval = 2.0f;
		public bool attackingHouse = false;
		private House house; 

		private float stopRange = 5.0f;
		void Awake ()
		{	
				// give it an initial target position 
				targetPosition = transform.position;
				path = this.GetComponent<AIPath> ();
				
				hash = GameObject.FindGameObjectWithTag("GameController").GetComponent<HashIds>();
				animator = transform.Find("Model").gameObject.GetComponent<Animator>();
		}
	
		// Update is called once per frame
		void Update ()
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
						
						moveDirection = Vector3.RotateTowards (moveDirection, targetPosition, rotateSpeed * Mathf.Deg2Rad * Time.deltaTime, 1000);
				
						moveDirection = moveDirection.normalized;
				}
				transform.rotation = Quaternion.LookRotation (moveDirection);
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
