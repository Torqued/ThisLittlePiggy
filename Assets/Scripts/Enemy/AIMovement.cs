﻿using UnityEngine;
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
		public bool boss; 

		public float attackInterval = 2.0f;
		public bool attackingHouse = false;
		private House house; 

		private float stopRange = 5.0f;

		//value for rotation
	    private Quaternion _lookRotation;
	    private Vector3 _direction;
		public float RotationSpeed = 10.0f;


		private HouseGUI houseGUI;
		private Transform model; 

		private AudioSource attack; 
		private AudioSource growl; 

		void Awake ()
		{	
				// give it an initial target position 
				targetPosition = transform.position;
				path = this.GetComponent<AIPath> ();
				
				model = transform.Find("Model");
				hash = GameObject.FindGameObjectWithTag("GameController").GetComponent<HashIds>();
				animator = model.gameObject.GetComponent<Animator>();
				houseGUI = GameObject.FindGameObjectWithTag("HouseGUI").GetComponent<HouseGUI>();
				AudioSource[] sounds = this.gameObject.GetComponents<AudioSource>();
				if (sounds.Length > 1) {
					attack = sounds[0];
					growl = sounds[1];
				}
		}
	
		// Update is called once per frame
		void FixedUpdate ()
		{	
				// check if there is a player 
				if (path.player == null)
					return;
				// play the growl sound once every 5 seconds
				if (Time.time % 5.0 == 0) {
					//growl.Play();
				}
				Debug.Log(path.player.gameObject.GetComponent<CharacterControls>().getResting());
				// check if wolf is attacking house, if so then don't run pathfinding code
				if (attackingHouse) {
					if (!boss) attackState();
					if (Time.time % attackInterval == 0) {
						if (house == null || !path.player.gameObject.GetComponent<CharacterControls>().getResting()) {
							// destroyed house

							if (!boss) chaseState();
							attackingHouse = false;
							path.player.gameObject.GetComponent<CharacterControls>().setResting(false);
						}
						else {
							// only damage house if not a brick house
							if (house.houseType != HouseType.Bricks) {
								house.DamageHouse(5);
							}
							else {
								house.DamageHouse(0);
							}

							attack.Play();
							if (!houseGUI.attacked)
								houseGUI.FadeGUI();
						}
					}
					return;
				}

				// if pig inside house, then increase the distance from the pig at which the wolf stops
				if (path.player.gameObject.GetComponent<CharacterControls>().getResting()) {
								if (!boss) stopRange = 10.0f;
								else stopRange = 20.0f;
				}
				else {
					if (!boss) stopRange = 5.0f;
					else stopRange = 10.0f;
				}

				// if wolf within stoprange distance of its target, then stop moving and start attacking
				if (Vector3.Distance (path.player.position, this.transform.position) <= stopRange) {
						
						path.stop = true;
						if (!boss) attackState();
						if (Time.time % attackInterval == 0) {
						// if player is outside house, then attack house
							if (!path.player.gameObject.GetComponent<CharacterControls>().getResting()) {
								path.player.gameObject.GetComponent<CharacterControls>().damageStamina(10.0f);
								attack.Play();
							}
							else {
								attackingHouse = true;
							}
						
						}
						return;
				}
				else { 
					if (!boss) chaseState();

				}


				path.stop = false;


				// actual rotation and translation code 
				if (targetPosition != Vector3.zero) {
						
					// rotate towards next path point
					//find the vector pointing from our position to the target
			        _direction = (targetPosition - transform.position).normalized;
			 
			       
			        if (_direction == Vector3.zero)
			        	;
			        else {
			        	 //create the rotation we need to be in to look at the target
			        	_lookRotation = Quaternion.LookRotation(_direction);
			 
				        //rotation over time according to speed until we are in the required rotation
				        model.rotation = Quaternion.Slerp(model.rotation, _lookRotation, Time.deltaTime * RotationSpeed);
				    }
						
				}
				transform.position = Vector3.MoveTowards (transform.position, targetPosition, speed * Time.deltaTime);

		}

		void OnTriggerEnter(Collider other ) {
			
			if (other.tag == "House") {
				attackingHouse = true;
				house = other.gameObject.GetComponent<House>();
				Debug.Log("gets here");
			}
		}


		void OnTriggerExit(Collider other ) {
			if (other.tag == "House") {
				if (!path.player.gameObject.GetComponent<CharacterControls>().getResting())
					attackingHouse = false;
				Debug.Log("gets here2");
			}
		}

		public void chaseState() {
			animator.SetBool(hash.runningBool, true);
			animator.SetBool(hash.idleBool, false);
			animator.SetBool(hash.attackBool, false);
			//agent.SetDestination(playerLastSighting);
			//agent.speed = enemySpeed;
		}

		public void alertState() {
			animator.SetBool(hash.idleBool, false);
			animator.SetBool(hash.runningBool, false);
			animator.SetBool(hash.alertBool, true);
			//agent.speed = 0;
		}

		public void idleState() {
			animator.SetBool(hash.idleBool, true);
			animator.SetBool(hash.runningBool, false);
			animator.SetBool(hash.attackBool, false);
			//agent.speed = 0;
		}

		public void attackState() {
			animator.SetBool(hash.attackBool, true);
			animator.SetBool(hash.idleBool, false);
			animator.SetBool(hash.runningBool, false);
			//What else should be done here?
		}

		public void fleeState() {
			animator.SetBool(hash.runningBool, true);
			//agent.speed = enemySpeed;
		}

}
