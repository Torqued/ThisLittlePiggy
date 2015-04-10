using UnityEngine;
using System.Collections;

public class Fairy : MonoBehaviour {


	public GameObject gc;
	public DayNightCycle dayNight;

	void Start(){
		gc = GameObject.FindGameObjectWithTag ("GameController");
		dayNight = gc.GetComponent<DayNightCycle> ();
	}

	void Update(){
		if (!dayNight.isNightTime()) {
			Destroy(gameObject);
		}
	}
}
