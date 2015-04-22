﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AIPath : MonoBehaviour
{

		// first get the target for this character to move towards
		public Transform player;
	
		// cache last target, till we find new path
		Vector3 lastPosition;

		// this sets the path to follow
		public List<Vector3> path = new List<Vector3> ();
		// this variable represents which position on the path we are on (5th out of 7 grids). 
		public int pathPos;
		public AIMovement move;

		void Awake ()
		{
				move = this.GetComponent<AIMovement> ();
				player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
		}

		void Update ()
		{
				// first check if AI has a target, if not then just return without doing anything
				// the AI just stays there or moves towards cached target
				if (player == null)
						return; 

				var playerPosition = player.position;

				// check if player position is on a walkable cell. If it is not, then ai characters just move 
				// towards last cached valid destination
				var gridPosition = BuildGrid.instance.Convert3DTo2DCoordinates (playerPosition);
				var valid = true; 
				if (BuildGrid.instance.grid != null) {
						if (!BuildGrid.instance.grid [gridPosition.x, gridPosition.y].isWalkable) 
								valid = false;
				}
				// now, we only make changes to the AI's movement if the player has moved and is on a walkable cell
				// that means if the new player position is different from the last position
				if (playerPosition != lastPosition && valid) {
			
						lastPosition = playerPosition;

						List<Vector3> temp_path = BuildGrid.instance.AStarSearch (this.transform.position, playerPosition);

						if (temp_path == null) 
								return;
						// else update the path
						path.Clear ();
						path.AddRange (temp_path);
						pathPos = 0;

				}

				// draws the path on scene view

				if (path.Count > 0) {
						var start = path [0];
						foreach (var end in path) {
								Debug.DrawLine (start, end, Color.yellow);
								start = end;
						}

				}

				// now, we actually get the AI moving towards route
				if (pathPos < path.Count) {
						// change target of AI
						move.targetPosition = path [pathPos];
						move.targetPosition = new Vector3(move.targetPosition.x, transform.position.y, move.targetPosition.z);

						// rotate towards next path point
						Vector3 targetDir = move.targetPosition - transform.position;
						float step = 100.0f * Time.deltaTime;
						Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, step, 0.0f);
						Debug.DrawRay(transform.position, newDir, Color.red);
						transform.rotation = Quaternion.LookRotation(newDir);
						// increment the path pos once we are close enough to current path pos
						var distance = Vector3.Distance (move.targetPosition, this.transform.position);
						if (distance < move.speed / 4)
								pathPos++;
				}

		}
}