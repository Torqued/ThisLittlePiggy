using UnityEngine;
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
		private AIMovement move;
		public bool stop = false;
		private bool flee = false; 
		private Vector3 spawnPoint; 

		public bool reachedTarget = false; 
		void Awake ()
		{
				move = this.GetComponent<AIMovement> ();
				player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
				lastPosition = transform.position;

		}

		public void Flee(Transform target) {
			flee = true;
			spawnPoint = target.position;
			move.attackingHouse = false;
		}

		void FixedUpdate ()
		{
				// first check if AI has a target, if not then just return without doing anything
				// the AI just stays there or moves towards cached target
				if (player == null || stop || move.attackingHouse)
						return; 

				if (flee) {
					setPath(spawnPoint);
					if (Vector3.Distance(transform.position, spawnPoint) < 3.0f)
						GameObject.Destroy(transform.gameObject);
				} else {

					Vector3 playerPosition = player.position;
					setPath(playerPosition);
				}
		}

	
		void setPath(Vector3 target) {
			// check if player position is on a walkable cell. If it is not, then ai characters just move 
				// towards last cached valid destination
				var gridPosition = BuildGrid.instance.Convert3DTo2DCoordinates (target);
				var valid = true; 
				if (BuildGrid.instance.grid != null) {
						if (!BuildGrid.instance.grid [gridPosition.x, gridPosition.y].isWalkable) 
								valid = false;
				}
				// now, we only make changes to the AI's movement if the player has moved and is on a walkable cell
				// that means if the new player position is different from the last position
				float dist = Vector3.Distance(target, lastPosition);
				

				if (dist > 5.0f && valid) {
						lastPosition = target;

						List<Vector3> temp_path = BuildGrid.instance.AStarSearch (this.transform.position, target);

						if (temp_path == null) 
								return;
						// else update the path
						path.Clear ();
						path.AddRange (temp_path);
						pathPos = 1;

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

						// increment the path pos once we are close enough to current path pos
						var distance = Vector3.Distance (move.targetPosition, this.transform.position);
						if (distance < move.speed / 4)
								pathPos++;
				}

		}
}