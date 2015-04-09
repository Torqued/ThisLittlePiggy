using UnityEngine;
using System.Collections;

public class FairyController : MonoBehaviour {

	public int numFairies;
	private bool hasSpawned;
	private GameObject player;
	private GameObject gc;
	private DayNightCycle dayNight;
	
	void Start(){
		player = GameObject.FindGameObjectWithTag("Player");
		gc = GameObject.FindGameObjectWithTag ("GameController");
		dayNight = gc.GetComponent<DayNightCycle> ();
		hasSpawned = false;
	}
	
	void Update(){
		if (dayNight.isNightTime()) {
			if(!hasSpawned)
			{
				int spanwed = 0;
				while(spanwed < numFairies)
				{
					Object.Instantiate(Resources.Load("SpawnPrefabs/StrawHouse"), player.transform.position, Quaternion.identity);
				}
			}
			hasSpawned = true;
		}
		else{
			hasSpawned = false;
		}
	}
}
