using UnityEngine;
using System.Collections;

public class HashIds : MonoBehaviour {
	public int runningBool;

	void Awake () {
		runningBool = Animator.StringToHash("Running");
	}
}