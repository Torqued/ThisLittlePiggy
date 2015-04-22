using UnityEngine;
using System.Collections;

public class AIMovement : MonoBehaviour
{

		public float speed = 15;
		public Vector3 targetPosition;
		private Vector3 moveDirection = Vector3.zero;
		float rotateSpeed = 1000.0f;
		Animation _animation;
		public AnimationClip idleAnimation;
		public AnimationClip runAnimation;

		void Awake ()
		{	
				// give it an initial target position 
				targetPosition = transform.position;
				
				_animation = this.GetComponent<Animation> ();
		}
	
		// Update is called once per frame
		void Update ()
		{	
				if (Vector3.Distance (targetPosition, this.transform.position) <= 2.0f)
						return;


				if (targetPosition != Vector3.zero) {
						
						moveDirection = Vector3.RotateTowards (moveDirection, targetPosition, rotateSpeed * Mathf.Deg2Rad * Time.deltaTime, 1000);
				
						moveDirection = moveDirection.normalized;
				}
				transform.rotation = Quaternion.LookRotation (moveDirection);
				transform.position = Vector3.MoveTowards (transform.position, targetPosition, speed * Time.deltaTime);

				// ANIMATION sector
				if (_animation) {
						if (Vector3.Distance (targetPosition, this.transform.position) > 1.0f) {
								_animation [runAnimation.name].speed = 1.0f;
								_animation.CrossFade (runAnimation.name);	
				
						} else {
								_animation [idleAnimation.name].speed = 1.0f;
								_animation.CrossFade (idleAnimation.name);
						}
				}
		}
}
