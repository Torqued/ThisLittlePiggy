using UnityEngine;
using System.Collections;

public class Construction : MonoBehaviour {

	int[] rope;
	int[] strawHouse;
	int[] stickHouse;
	int[] brickHouse;
	GameObject controller;
	Inventory inventory;
	GameObject player;
	public GameObject StrawHousePrefab;

	// Use this for initialization
	void Start () {

		controller = GameObject.FindGameObjectWithTag ("GameController");
		inventory = controller.GetComponent<Inventory> ();
		player = GameObject.FindGameObjectWithTag ("Player");
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.F)) {
			makeStrawHouse();
		}
	}

	private void makeRope()
	{
		if (inventory.grass >= 2) {
			inventory.grass -=2;
			inventory.rope++;
		}
	}

	private void makeStrawHouse()
	{
		if (inventory.straw >= 2){//(inventory.rope >= 2 && inventory.straw >= 5) {
			inventory.straw -=2;
			//inventory.rope -=2;
			//inventory.straw -=5;
			Instantiate(StrawHousePrefab, (player.transform.position + new Vector3(0,4.2f,5)), Quaternion.identity);
		}
	}

	private void makeStickHouse()
	{
		if (inventory.grass >= 2) {
			
		}
	}

	private void makeBrickHouse()
	{
		if (inventory.grass >= 2) {
			
		}
	}
	
}
