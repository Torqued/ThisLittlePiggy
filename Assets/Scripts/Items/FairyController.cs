using UnityEngine;
using System.Collections;

public class FairyController : MonoBehaviour {

	public int numFairies;
	public bool hasSpawned;
	private GameObject player;
	private GameObject gc;
	private DayNightCycle dayNight;
	public GameObject fairyObj;
	
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
				int spawned = 0;
				Debug.Log("About to spawn");
				while(spawned < numFairies)
				{

					Debug.Log("Spawning");
					Object.Instantiate(fairyObj, player.transform.position, Quaternion.identity);
					spawned++;
				}
			}
			hasSpawned = true;
		}
		else{
			hasSpawned = false;
		}
	}
}
