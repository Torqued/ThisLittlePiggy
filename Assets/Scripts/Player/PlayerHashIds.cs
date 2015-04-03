using UnityEngine;
using System.Collections;

public class PlayerHashIds : MonoBehaviour {
	public int runningBool;

	void Awake () {
		runningBool = Animator.StringToHash("Running");
	}
}
