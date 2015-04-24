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
	private float nextHp;

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
		Object.Instantiate((Resources.Load("Effects/ConstructionEffect", typeof(GameObject)) as GameObject), transform.position, Quaternion.identity);
	}

	void Update(){
		if (constructing) {
			if(currentHealth < maxHealth && (Time.time > nextHp))
			{
				currentHealth += 1;
				nextHp = Time.time + healthRate;
			}
			else
				constructing = false;
		}

	}

	public void DamageHouse(){
		currentHealth -= 10;
		if (currentHealth <= 0) {
			Destroy(gameObject);
		}
	}

}