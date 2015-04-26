using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class EnemySpawning : MonoBehaviour {

	private bool spawned = false;
	private bool despawned = false;

	public DayNightCycle dayCycle; 

	public GameObject wolfPrefab;
	private ArrayList wolves = new ArrayList();
	 
	public Transform[] spawnPoints; 

	public int daysPassed = 0;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {

		if (dayCycle.isNightTime () && !spawned) {
			daysPassed++;
			SpawnWolves (daysPassed);
			spawned = true; 
			despawned = false;
		} else if (!dayCycle.isNightTime () && !despawned) {
			DespawnWolves();
			despawned = true;
			spawned = false;
		}
	}


	void SpawnWolves(int daysPassed){
		// spawn 1 wolf at each spawnpoint; 
		// on the first day, spawn 2 wolves, on the second and third spawn 4.
		// if more than 3 days have passed, don't spawn wolves
		int count = 0;
		if (daysPassed > 3) {
			return;
		}
		foreach (Transform spawnPoint in spawnPoints) {
			wolves.Add((GameObject) GameObject.Instantiate(wolfPrefab,spawnPoint.position, Quaternion.identity));
			count ++;
			if (daysPassed == 1 && count == 2) {
				return; 
			} 
			if (daysPassed > 1 && count == 4) {
				return;
			}
		}

	}

	void DespawnWolves() {
		// have each wolf head for its spawn point, 
		// increase its speed, and once it reached, destroy it

		int current_index = 0; 
		foreach (GameObject wolf in wolves) {
			wolf.GetComponent<AIPath> ().Flee(spawnPoints[current_index]);
			current_index ++;
		}
	}
}
