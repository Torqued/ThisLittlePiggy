using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public enum HouseType {
	Straw,
	Sticks,
	Bricks
}

public class House : MonoBehaviour {

	public HouseType houseType;
	public int maxHealth;
	public int currentHealth;
	public bool constructing;
	public float healthRate;
	public Transform center;
	public Transform front;
	private float nextHp;

	private GameObject player;
	private bool inHouse = false;
	private bool GUI = false;

	private HouseGUI houseGUI;

	public LayerMask raycastLayer;
	Bounds gridBounds;

	void Start(){
		constructing = true;
		if (houseType == HouseType.Straw) {
			maxHealth = 100;
			currentHealth = 100;
		} else if (houseType == HouseType.Sticks) {
			maxHealth = 150;
			currentHealth = 150;
		} else if (houseType == HouseType.Bricks) {
			maxHealth = 1000;
			currentHealth = 1000;
		}


		//Object.Instantiate((Resources.Load("Effects/ConstructionEffect", typeof(GameObject)) as GameObject), (transform.position - new Vector3(0,5,5)), Quaternion.identity);
	
	//	UnityEngine.Object.Instantiate((Resources.Load("Effects/ConstructionEffect", typeof(GameObject)) as GameObject), (transform.position - new Vector3(0,5,5)), Quaternion.identity);
		houseGUI = GameObject.FindGameObjectWithTag("HouseGUI").GetComponent<HouseGUI>();

	}



	void Update(){
//		if (constructing) {
//			if(currentHealth < maxHealth)
//			{
//				if(Time.time > nextHp){
//					currentHealth += 1;
//					nextHp = Time.time + healthRate;
//				}
//
//			}
//			else
//				constructing = false;
//		}

		if (inHouse) {

			houseGUI.updateHealth(currentHealth, maxHealth);


			if (!GUI && !houseGUI.attacked) {
				houseGUI.EnableGUI();
				Debug.Log("gets here to enable GUI");
				GUI = true;
			}

			if (Input.GetKeyDown(KeyCode.F)) {
				player.GetComponent<CharacterControls>().setResting(false);
				player.transform.position = front.position;
				houseGUI.DisableGUI();
				Debug.Log("gets here to disable GUI");
				GUI = false;
				inHouse = false;
			}

		}

	}

	public void DamageHouse(int damage){
		if (houseType == HouseType.Bricks)
			return;
		currentHealth -= damage;
		Debug.Log(currentHealth);
		if (currentHealth <= 0) {
			houseGUI.DisableGUI();
			Debug.Log("gets here to disable GUI 2");
			Destroy(gameObject);
		}
	}

	void OnTriggerStay(Collider other) {
		if (other.gameObject.tag == "Player") {
			player = other.gameObject;
			player.GetComponent<CharacterControls>().setResting(true);
			player.transform.position = center.position;
			inHouse = true;
		}

	}

	void OnTriggerExit(Collider other) {
		if (other.gameObject.tag == "Player") {
			player = other.gameObject;
			//player.GetComponent<CharacterControls>().setResting(false);
			inHouse = false;
		}

	}


	void UpdateGrid() {
		// Raycast 5 rays from house, center, top-left, top-right, bottom-left, and bottom-right
		// mark all grid cells they touch as unwalkable on grid. 
		gridBounds = this.gameObject.GetComponent<Renderer>().bounds;

		// we use the top left corner 
		Vector3 topLeftCorner = gridBounds.center - gridBounds.extents + new Vector3 (0, gridBounds.size.y, 0);

		Vector3 center = topLeftCorner + new Vector3 (gridBounds.size.x /2.0f, transform.position.y, gridBounds.size.z /2.0f);
		RaycastHit hit; 
		BuildGrid.GridCoordinates cell;
		// generate a ray out of center of cell
		if (Physics.Raycast (center, -Vector3.up, out hit, 30.0f, raycastLayer)) {
			// when we hit grid, get coordinates of cell
			cell = BuildGrid.instance.Convert3DTo2DCoordinates (hit.collider.transform.position);		
			
			// make that cell unwalkable
			BuildGrid.instance.grid[cell.x, cell.y].isWalkable = false;
		}

		// now generate 4 more rays from each corner 

		// top left
		Vector3 pos = topLeftCorner;
		if (Physics.Raycast (pos, -Vector3.up, out hit, 30.0f, raycastLayer)) {
			// when we hit grid, get coordinates of cell
			cell = BuildGrid.instance.Convert3DTo2DCoordinates (hit.collider.transform.position);		
			
			// make that cell unwalkable
			BuildGrid.instance.grid[cell.x, cell.y].isWalkable = false;
		}

		// top right
		pos = topLeftCorner + new Vector3( gridBounds.size.x, 0f, 0f);
		if (Physics.Raycast (pos, -Vector3.up, out hit, 30.0f, raycastLayer)) {
			// when we hit grid, get coordinates of cell
			cell = BuildGrid.instance.Convert3DTo2DCoordinates (hit.collider.transform.position);		
			
			// make that cell unwalkable
			BuildGrid.instance.grid[cell.x, cell.y].isWalkable = false;
		}

		// bottom left
		pos = topLeftCorner + new Vector3( 0f, 0f, gridBounds.size.z);
		if (Physics.Raycast (pos, -Vector3.up, out hit, 30.0f, raycastLayer)) {
			// when we hit grid, get coordinates of cell
			cell = BuildGrid.instance.Convert3DTo2DCoordinates (hit.collider.transform.position);		
			
			// make that cell unwalkable
			BuildGrid.instance.grid[cell.x, cell.y].isWalkable = false;
		}

		// bottom right
		pos = topLeftCorner + new Vector3( gridBounds.size.x, 0f, gridBounds.size.z);
		if (Physics.Raycast (pos, -Vector3.up, out hit, 30.0f, raycastLayer)) {
			// when we hit grid, get coordinates of cell
			cell = BuildGrid.instance.Convert3DTo2DCoordinates (hit.collider.transform.position);		
			
			// make that cell unwalkable
			BuildGrid.instance.grid[cell.x, cell.y].isWalkable = false;
		}
				
	}

}