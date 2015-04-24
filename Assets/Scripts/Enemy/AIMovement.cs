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

				if (Vector3.Distance (path.player.position, this.transform.position) <= 5.0f) {
						path.stop = true;
						idleState();
						path.player.gameObject.GetComponent<CharacterControls>().damageStamina(35.0f);
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
