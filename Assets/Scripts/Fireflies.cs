using UnityEngine;
using System.Collections;

public class Fireflies : MonoBehaviour {

	private DayNightCycle dayCycle; 
	private ParticleSystem fireflies; 
	private bool spawned = false;
	// Use this for initialization
	void Start () {
		dayCycle = GameObject.FindGameObjectWithTag("GameController").GetComponent<DayNightCycle>();
		fireflies = this.gameObject.GetComponent<ParticleSystem>();
	}
	
	// Update is called once per frame
	void Update () {
		if (dayCycle.timeOfDay() > 0.5f) {
			if (!spawned) {
				// start emission of particles
				fireflies.Play();
				spawned = true;
			}
		}
		else {
			if (spawned) {
				// stop emission of particles 
				fireflies.Stop();
				spawned = false;
			}
		}
	}

}
