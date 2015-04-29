using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Threading;
using System.Linq;

public class BuildGrid : MonoBehaviour
{
	
		
	// the size of a cell, smaller units find obstacles better
	public float cellSize = 20.0f;
	
	// width and height of the grid in number of cells
	public int gridWidth, gridHeight;
	
	// the grid that we compute for A* search 
	public GridCell[,] grid;
		
	
	// the bounds of the grids, we'll use this for raycasting
	Bounds gridBounds;

	// Top left corner of the bounds
	Vector3 topLeftCorner;

	// two layer masks
	// obstacle layers are the layers that contain components that should not be walkable on
	public LayerMask obstacleLayer;
	// raycast layers are the layers that contain components that the raycasts should check collision with
	public LayerMask raycastLayer;

	// a singleton instance for other scripts to refer to
	public static BuildGrid instance;

	// a class representing a cell in the grid
	// use a class instead of a struct so that it lives on the heap, as grid cells are used throughout
	public class GridCell
	{
		// returns true if characters can walk over this cell
		public bool isWalkable;

	}

	
	void Awake ()
	{
		instance = this;
		GetComponent<Renderer>().enabled = false;
	}
		
	public void Update ()
	{
		// draws the raycasts 
		for (var x = 0; x < gridWidth; x ++) {
			for (var y = 0; y < gridHeight; y++) {
				gridBounds = GetComponent<Renderer>().bounds;
				// Work out the top left corner
				topLeftCorner = gridBounds.center - gridBounds.extents + new Vector3 (0, gridBounds.size.y, 0);
				// Get the position for a ray
				var center = topLeftCorner + new Vector3 (x * cellSize, 0, y * cellSize) + new Vector3 (0.5f * cellSize, 0, 0.5f * cellSize);
								
				//Debug.DrawRay (center, Vector3.up, Color.green);
				/*
								// generate 4 random rays out of topleft square of cell 
								for (var i = 0; i < 4; i++) {
										var factor1 = UnityEngine.Random.Range (0f, 0.5f);
										var factor2 = UnityEngine.Random.Range (0f, 0.5f);
										var pos = topLeftCorner + new Vector3 (x * cellSize, 0, y * cellSize) + new Vector3 (factor1 * cellSize, 0, factor2 * cellSize);
										Debug.DrawRay (pos, Vector3.up, Color.green);
								}
				
								// generate 4 random rays out of bottomleft square of cell 
								for (var i = 0; i < 4; i++) {
										var factor1 = UnityEngine.Random.Range (0f, 0.5f);
										var factor2 = UnityEngine.Random.Range (0.5f, 1.0f);
										var pos = topLeftCorner + new Vector3 (x * cellSize, 0, y * cellSize) + new Vector3 (factor1 * cellSize, 0, factor2 * cellSize);
										Debug.DrawRay (pos, Vector3.up, Color.green);
								}
				
								// generate 4 random rays out of topright square of cell 
								for (var i = 0; i < 4; i++) {
										var factor1 = UnityEngine.Random.Range (0.5f, 1.0f);
										var factor2 = UnityEngine.Random.Range (0f, 0.5f);
										var pos = topLeftCorner + new Vector3 (x * cellSize, 0, y * cellSize) + new Vector3 (factor1 * cellSize, 0, factor2 * cellSize);
										Debug.DrawRay (pos, Vector3.up, Color.green);
								}
				
								// generate 4 random rays out of bottomright square of cell 
								for (var i = 0; i < 4; i++) {
										var factor1 = UnityEngine.Random.Range (0.5f, 1.0f);
										var factor2 = UnityEngine.Random.Range (0.5f, 1.0f);
										var pos = topLeftCorner + new Vector3 (x * cellSize, 0, y * cellSize) + new Vector3 (factor1 * cellSize, 0, factor2 * cellSize);
										Debug.DrawRay (pos, Vector3.up, Color.green);
								}
								*/
			}
		}
		
	}
	
	
	// Build a grid to use A* search on 
	public void makeGrid ()
	{
		
		gridBounds = GetComponent<Renderer>().bounds;

		// we use the top left corner to start iterating through the grid
		topLeftCorner = gridBounds.center - gridBounds.extents + new Vector3 (0, gridBounds.size.y, 0);
				
		// get dimensions of grid
		gridWidth = Mathf.RoundToInt (gridBounds.size.x / cellSize);
		gridHeight = Mathf.RoundToInt (gridBounds.size.z / cellSize);
			
		// initialize grid
		grid = new GridCell[gridWidth, gridHeight];
				
		/*
				 * For each cell, we raycast rays upwards out of the cell. It only tests for collisions against gameobjects 
				 * which are in the raycastLayer. If the rays hit something, if that gameobject is also in the obstacles layer, 
				 * mark the cell as unwalkable. All cells are marked walkable initially
				 */
		for (var x = 0; x < gridWidth; x ++) {
			for (var y = 0; y < gridHeight; y++) {
				/* how many rays are we going to cast? how about casting 17 rays in total?
							 * 1 ray out of the center. Then split up the cell into 4 squares, and have 4 random rays out of each square
							 */
				
				//Create a cell for the grid
				var cell = new GridCell ();
				grid [x, y] = cell;
				cell.isWalkable = true;	

				var center = topLeftCorner + new Vector3 (x * cellSize, 0, y * cellSize) + new Vector3 (0.5f * cellSize, 0, 0.5f * cellSize);
				RaycastHit hit; 
								
				// generate a ray out of center of cell
				if (Physics.Raycast (center, Vector3.up, out hit, 30.0f, raycastLayer)) {
					//Test if the thing we hit was an obstacle
					if (((1 << hit.collider.gameObject.layer) & obstacleLayer) != 0) {
						//Flag the cell as unwalkable
						cell.isWalkable = false;
					}
				}
				/*
				// generate 4 random rays out of topleft square of cell 
				for (var i = 0; i < 4; i++) {
					var factor1 = UnityEngine.Random.Range (0f, 0.5f);
					var factor2 = UnityEngine.Random.Range (0f, 0.5f);
					var pos = topLeftCorner + new Vector3 (x * cellSize, 0, y * cellSize) + new Vector3 (factor1 * cellSize, 0, factor2 * cellSize);
					if (Physics.Raycast (pos, Vector3.up, out hit, 2.0f, raycastLayer)) {
						//Test if the thing we hit was an obstacle
						if (((1 << hit.collider.gameObject.layer) & obstacleLayer) != 0) {
							//Flag the cell as unwalkable
							cell.isWalkable = false;
						}
					}
				}
				
				// generate 4 random rays out of bottomleft square of cell 
				for (var i = 0; i < 4; i++) {
					var factor1 = UnityEngine.Random.Range (0f, 0.5f);
					var factor2 = UnityEngine.Random.Range (0.5f, 1.0f);
					var pos = topLeftCorner + new Vector3 (x * cellSize, 0, y * cellSize) + new Vector3 (factor1 * cellSize, 0, factor2 * cellSize);
					if (Physics.Raycast (pos, Vector3.up, out hit, 2.0f, raycastLayer)) {
						//Test if the thing we hit was an obstacle
						if (((1 << hit.collider.gameObject.layer) & obstacleLayer) != 0) {
							//Flag the cell as unwalkable
							cell.isWalkable = false;
						}
					}
				}
				
				// generate 4 random rays out of topright square of cell 
				for (var i = 0; i < 4; i++) {
					var factor1 = UnityEngine.Random.Range (0.5f, 1.0f);
					var factor2 = UnityEngine.Random.Range (0f, 0.5f);
					var pos = topLeftCorner + new Vector3 (x * cellSize, 0, y * cellSize) + new Vector3 (factor1 * cellSize, 0, factor2 * cellSize);
					if (Physics.Raycast (pos, Vector3.up, out hit, 2.0f, raycastLayer)) {
						//Test if the thing we hit was an obstacle
						if (((1 << hit.collider.gameObject.layer) & obstacleLayer) != 0) {
							//Flag the cell as unwalkable
							cell.isWalkable = false;
						}
					}
				}
				
				// generate 4 random rays out of bottomright square of cell 
				for (var i = 0; i < 4; i++) {
					var factor1 = UnityEngine.Random.Range (0.5f, 1.0f);
					var factor2 = UnityEngine.Random.Range (0.5f, 1.0f);
					var pos = topLeftCorner + new Vector3 (x * cellSize, 0, y * cellSize) + new Vector3 (factor1 * cellSize, 0, factor2 * cellSize);
					if (Physics.Raycast (pos, Vector3.up, out hit, 2.0f, raycastLayer)) {
						//Test if the thing we hit was an obstacle
						if (((1 << hit.collider.gameObject.layer) & obstacleLayer) != 0) {
							//Flag the cell as unwalkable
							cell.isWalkable = false;
						}
					}
				}
				*/

			}
		}
		
	}
		
		
	// use a struct to represent the position on a grid, since there is much better locality of reference
	// cheaper allocation and deallocation since allocated inline. Will be faster than a class when storing them in an array
	// We will use this to store lists of neighbours so we use struct here
	public struct GridCoordinates
	{
		public int x;
		public int y;

		// have 2 distance functions to another coordinates on grid
		// can add more to try out different types of A* searches
		public int ManhattanDistance (GridCoordinates point)
		{
			return Mathf.Abs (point.x - x) + Mathf.Abs (point.y - y);
		}

		public float EuclideanDistance (GridCoordinates point)
		{
			var part1 = Mathf.Pow (1.0f * Mathf.Abs (point.x - x), 2.0f);
			var part2 = Mathf.Pow (1.0f * Mathf.Abs (point.y - y), 2.0f);
			return Mathf.Sqrt (part1 + part2);
		}

	}

	// get all surrouding neighbours of some coordinates on the grid 
	public List<GridCoordinates> GetNeighbours (GridCoordinates coordinates)
	{
		var neighbours = new List<GridCoordinates> ();
		// gets all neighbours surrounding the coordinates.
		// checks each of the 8 directions to see if valid coordinates and if so, adds them as a neighbour
		var curNeighbour = coordinates;
		for (var x = -1; x <= 1; x ++) {
			for (var y = -1; y <= 1; y ++) {
				curNeighbour = new GridCoordinates { x = x + coordinates.x, y = y + coordinates.y };		
				// verify that the coordinates are not outside the grid
				if (curNeighbour.x >= 0 && curNeighbour.y >= 0 && curNeighbour.x < gridWidth && curNeighbour.y < gridHeight)
					neighbours.Add (curNeighbour);
			}
		}
		return neighbours;
	}


	// Convert a 2D grid position into a 3D world position
	public Vector3 Convert2DTo3DCoordinates (GridCoordinates coordinates)
	{
		var worldPosition = new Vector3 (coordinates.x * cellSize, 0f, coordinates.y * cellSize);
		return worldPosition + new Vector3 (topLeftCorner.x, 0f, topLeftCorner.z);
	}
		
	// Convert a 3D world position into a 2D grid position
	public GridCoordinates Convert3DTo2DCoordinates (Vector3 coordinates)
	{
		coordinates -= topLeftCorner;
		return new GridCoordinates { x = Mathf.FloorToInt(coordinates.x / cellSize), y = Mathf.FloorToInt(coordinates.z / cellSize) };

	}
	// represents an A* node
	public class Node
	{		// cost
		public float g_score = float.MaxValue;
		// estimate
		public float f_score;
		// parent of current node (the node before this node on its current path)
		public GridCoordinates parent = new GridCoordinates ();
	}

	//Find a path between two positions using A* search
	public List<Vector3> AStarSearch (Vector3 startPosition, Vector3 endPosition)
	{		
		// firstly, we check if we have a grid, and if we do, we make the grid
		if (grid == null) 
			makeGrid ();
				
		//Start and end in grid coordinates
		var start = Convert3DTo2DCoordinates (startPosition);
		var end = Convert3DTo2DCoordinates (endPosition);

		// the set of nodes already evaluatated
		var closedSet = new Dictionary<GridCoordinates, Node> ();
		// the set of tentative nodes to be evaluated
		// contains the starting node initially
		var openSet = new Dictionary<GridCoordinates, Node> (); 
		// the set of navigated nodes ( we use it to rebuild path) 
		var parentSet = new Dictionary<GridCoordinates, Node> ();
		
		// create start node and add it to both open set and parent set
		// lets use the manhattan distance heuristic
		var startNode = new Node{f_score = start.ManhattanDistance(end)};
		// no cost for start node 
		startNode.g_score = 0;
		openSet [start] = startNode;
		parentSet [start] = startNode;

		// while the open set is not empty
		while (openSet.Count > 0) {
			// we get the node in the open set having the lowest f_score
			// aggregate simply compares every keyvalue pair sequentially, and returns the key-value pair
			// with the lowest f_score
			var current = openSet.Aggregate ((kv1,kv2) => kv1.Value.f_score < kv2.Value.f_score ? kv1 : kv2);
			// if the grid coordinates in curent is our goal, we return the reconstructed path
			if (current.Key.x == end.x && current.Key.y == end.y) {
				var path = new List<Vector3> ();
				var parentNode = current.Value;
				
				// keep using the parents of the node to find all the nodes and add their coordinates to path
				// we keep adding items to the start of the list, so add items in reverse
				path.Add (endPosition);
				
				// if parent's grid coordinates are the 0,0 vector, which is default values for struct,
				// then parent's were never assigned so this is the start of the path and we are done
				while (parentNode != null && !(parentNode.parent.x == 0 && parentNode.parent.y == 0)) {
					path.Insert (0, Convert2DTo3DCoordinates (parentNode.parent));
					parentNode = parentSet [parentNode.parent];
				}
				// send result back to caller
				return path;
			}
			
			// if current node is not the goal, we remove it from the open set and add it to closed set
			openSet.Remove (current.Key);
			closedSet [current.Key] = current.Value;
			
			// now, get all walkable neighbours of the current node
			var walkableNeighbours = GetNeighbours (current.Key).Where (n => grid [n.x, n.y].isWalkable);
			
			// process each of them
			foreach (var neighbour in walkableNeighbours) {
				// first check if neighbour in closed set, meaning already processed
				if (closedSet.ContainsKey (neighbour))
					continue;
				// add the cost of moving to this neighbour, which is simply a distance of 1 since no weights
				var tentativeGScore = current.Value.g_score + 1;
				
				// now get the neighbour node, or create a new one if it doesn't exist
				
				Node neighbourNode;
				
				if (openSet.ContainsKey (neighbour)) {
					neighbourNode = openSet [neighbour];
				} else {
					neighbourNode = new Node (); // default 0 value for g and f score
					// add node to open and parent set
					openSet [neighbour] = neighbourNode;
					parentSet [neighbour] = neighbourNode;
				} 
				
				// now check if tentative g score < g score of neighbour node
				if (tentativeGScore < neighbourNode.g_score) {
					// if it is, update g score and f score with same heuristic function as before
					neighbourNode.g_score = tentativeGScore;
					neighbourNode.f_score = tentativeGScore + neighbour.ManhattanDistance (end);
					
					// also update the parent
					neighbourNode.parent = current.Key;
				}
				
			}

		}
		return null;

	} 
	
		
	// draw the grid on scene view
	void OnDrawGizmosSelected ()
	{
		for (var x = 0; x < gridWidth; x++) {
			for (var y = 0; y < gridHeight; y++) {
				var cell = grid [x, y];
				Gizmos.color = cell.isWalkable ? Color.green : Color.red;
				var center = topLeftCorner + new Vector3 (((float)x + 0.5f) * cellSize, 0, ((float)y + 0.5f) * cellSize);
				Gizmos.DrawCube (center, Vector3.one * cellSize * 0.75f);
			}
		}
	}
	
}
