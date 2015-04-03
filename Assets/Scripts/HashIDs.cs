using UnityEngine;
using System.Collections;

public class HashIDs : MonoBehaviour {
	public int runningBool;

	// Use this for initialization
	void Awake () {
		runningBool = Animator.StringToHash("Running");
	
	}

}
