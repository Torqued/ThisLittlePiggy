using UnityEngine;
using System.Collections;
using System.Collections.Generic; 

public class AIMovement : MonoBehaviour
{

		public float speed = 15;
		public Vector3 targetPosition;
		private Vector3 moveDirection = Vector3.zero;
		float rotateSpeed = 1000.0f;
		Animation animation;
		public AnimationClip idleAnimation;
		public AnimationClip runAnimation;

		private AIPath path; 
		Dictionary<string, AnimationState> states = new Dictionary<string, AnimationState>();
		void Awake ()
		{	
				// give it an initial target position 
				targetPosition = transform.position;
				path = this.GetComponent<AIPath> ();
				animation = this.GetComponent<Animation> ();
				if (animation) {
					foreach(AnimationState state in animation)
					{
					    states.Add(state.name, state);
					}
				}
		}
	
		// Update is called once per frame
		void Update ()
		{	
				if (path.player == null)
					return;

				if (Vector3.Distance (path.player.position, this.transform.position) <= 5.0f) {
						path.stop = true;
						return;
				}

				path.stop = false;

				if (targetPosition != Vector3.zero) {
						
						moveDirection = Vector3.RotateTowards (moveDirection, targetPosition, rotateSpeed * Mathf.Deg2Rad * Time.deltaTime, 1000);
				
						moveDirection = moveDirection.normalized;
				}
				transform.rotation = Quaternion.LookRotation (moveDirection);
				transform.position = Vector3.MoveTowards (transform.position, targetPosition, speed * Time.deltaTime);

				// ANIMATION sector
				if (animation) {
						if (Vector3.Distance (path.player.position, this.transform.position) > 5.0f) {
							states[runAnimation.name].speed = 2.0f;
								//animation [runAnimation.name].speed = 1.0f;
							animation.CrossFade (runAnimation.name);	
			
						} else {
							states[idleAnimation.name].speed = 1.0f;	
							//animation [idleAnimation.name].speed = 1.0f;
							animation.CrossFade (idleAnimation.name);
						}
				}
		}
}
