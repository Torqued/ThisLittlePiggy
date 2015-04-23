using UnityEngine;
using System.Collections;

public class Pause : MonoBehaviour {

	bool isPaused;

	// Use this for initialization
	void Start () {
		isPaused = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.P)) {
			isPaused = !isPaused;
		}
	}

	public bool gameIsPaused(){
		return isPaused;
	}
}
