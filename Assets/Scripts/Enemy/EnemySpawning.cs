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


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		if (dayCycle.isNightTime () && !spawned) {
			SpawnWolves ();
			spawned = true; 
			despawned = false;
		} else if (!dayCycle.isNightTime () && !despawned) {
			DespawnWolves();
			despawned = true;
			spawned = false;
		}
	}


	void SpawnWolves(){
		// spawn 1 wolf at each spawnpoint; 
		foreach (Transform spawnPoint in spawnPoints) {
			wolves.Add((GameObject) GameObject.Instantiate(wolfPrefab,spawnPoint.position, Quaternion.identity));
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
