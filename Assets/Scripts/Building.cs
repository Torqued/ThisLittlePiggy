using UnityEngine;
using System.Collections;


public class Building : MonoBehaviour {

	private bool isUnderConstruction;
	private float timeConstructionStarted;
	private int currHealth;
	private int maxHealth;

	// Use this for initialization
	void Start () {
		timeConstructionStarted = Time.time;
		isUnderConstruction = false;
		maxHealth = 100000;
	}
	
	// Update is called once per frame
	void Update () {
		if(isUnderConstruction)
			underConstruction();
	}

	public void setMaxHealth(int max){
		maxHealth = max;
	}

	public void removeHealth(int remove)
	{
		if (isUnderConstruction) {
			isUnderConstruction = false;
		}
		currHealth -= remove;
	}

	public void underConstruction(){
		if (currHealth >= maxHealth) {
			currHealth = maxHealth;
			isUnderConstruction = false;
		}
	}
}
