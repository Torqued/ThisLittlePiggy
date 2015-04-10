using UnityEngine;
using System.Collections;

public class HashIds : MonoBehaviour {
	public int idleBool;
	public int runningBool;
	public int alertBool;
	public int attackBool;
	public int playerDetectedBool;
	public int fleeBool;
	public int panicBool;
	public bool playerSpotted = false; //Player Panic Mode Hack.

	void Awake () {
		idleBool = Animator.StringToHash("Idle");
		runningBool = Animator.StringToHash("Running");
		alertBool = Animator.StringToHash("Alert");
		attackBool = Animator.StringToHash("Attack");
		playerDetectedBool = Animator.StringToHash("PlayerDetected");
		fleeBool = Animator.StringToHash("Flee");
		panicBool = Animator.StringToHash("Panic");
	}
}