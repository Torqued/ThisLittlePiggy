using UnityEngine;
using System.Collections;

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

	void Start(){
		constructing = true;
		if (houseType == HouseType.Straw) {
			maxHealth = 100;
		} else if (houseType == HouseType.Sticks) {
			maxHealth = 150;
		} else if (houseType == HouseType.Bricks) {
			maxHealth = 1000;
		}
		healthRate = 1.0f;
		nextHp = 0.0f;
		Object.Instantiate((Resources.Load("Effects/ConstructionEffect", typeof(GameObject)) as GameObject), (transform.position - new Vector3(0,5,5)), Quaternion.identity);
		houseGUI = GameObject.FindGameObjectWithTag("HouseGUI").GetComponent<HouseGUI>();
	}

	void Update(){
		if (constructing) {
			if(currentHealth < maxHealth)
			{
				if(Time.time > nextHp){
					currentHealth += 1;
					nextHp = Time.time + healthRate;
				}

			}
			else
				constructing = false;
		}

		if (inHouse) {

			houseGUI.updateHealth(currentHealth, maxHealth);


			if (!GUI && !houseGUI.attacked) {
				houseGUI.EnableGUI();
				Debug.Log("gets here to enable GUI");
				GUI = true;
			}

			if (Input.GetKeyDown(KeyCode.Z)) {
				player.GetComponent<CharacterControls>().setResting(false);
				player.transform.position = front.position;
				houseGUI.DisableGUI();
				Debug.Log("gets here to disable GUI");
				GUI = false;
				inHouse = false;
			}

		}


	}

	public void DamageHouse(){
		currentHealth -= 5;
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

}