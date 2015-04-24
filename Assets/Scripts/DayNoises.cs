using UnityEngine;
using System.Collections;

public class DayNoises : MonoBehaviour {
	
	// Use this for initialization
	
	GameObject player;
	GameObject gc;
	DayNightCycle dayNight;
	
	void Start () {
		player = GameObject.FindGameObjectWithTag("Player");
		gc = GameObject.FindGameObjectWithTag ("GameController");
		dayNight = gc.GetComponent<DayNightCycle> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (!dayNight.isNightTime() && !GetComponent<AudioSource> ().isPlaying) {
			GetComponent<AudioSource>().Play();
		}
		else if (dayNight.isNightTime() && GetComponent<AudioSource> ().isPlaying) {
			GetComponent<AudioSource>().Stop();
		}
		
		
	}
}
