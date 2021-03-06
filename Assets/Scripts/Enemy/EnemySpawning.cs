﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class EnemySpawning : MonoBehaviour {

	private bool spawned = false;
	private bool despawned = false;
	private bool bossSpawned = false;

	public DayNightCycle dayCycle; 

	public GameObject wolfPrefab;
	public GameObject bigBadWolfPrefab;

	private ArrayList wolves = new ArrayList();
	 
	public Transform[] spawnPoints; 
	public Transform bigBadWolfSpawnPoint;
	public int daysPassed = 0;
	// Use this for initialization
	void Awake () {
	}
	
	// Update is called once per frame
	void Update () {

		if (dayCycle.isNightTime () && !spawned) {
			daysPassed++;
			SpawnWolves (daysPassed);
			spawned = true; 
			despawned = false;
		} else if (!dayCycle.isNightTime () && !despawned) {
			DespawnWolves(bossSpawned);
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

		if (daysPassed == 3) {
			wolves.Add((GameObject) GameObject.Instantiate(bigBadWolfPrefab,bigBadWolfSpawnPoint.position, Quaternion.identity));
			bossSpawned = true;
			return;
		}
		foreach (Transform spawnPoint in spawnPoints) {
			wolves.Add((GameObject) GameObject.Instantiate(wolfPrefab,spawnPoint.position, Quaternion.identity));
			count ++;
			if (daysPassed == 1 && count == 2) {
				bossSpawned = false;
				return; 
			} 
			if (daysPassed == 2 && count == 4) {
				bossSpawned = false;
				return;
			}
		}

	}

	void DespawnWolves(bool boss) {
		// have each wolf head for its spawn point, 
		// increase its speed, and once it reached, destroy it
		if (boss) {
			GameObject wolf = (GameObject) wolves[0];
			wolf.GetComponent<AIPath> ().Flee(bigBadWolfSpawnPoint);
			return;
		}
		else {
			int current_index = 0; 
			foreach (GameObject wolf in wolves) {
				if(wolf){
					wolf.GetComponent<AIPath> ().Flee(spawnPoints[current_index]);
					current_index ++;
				}
			}
		}
	}
}
