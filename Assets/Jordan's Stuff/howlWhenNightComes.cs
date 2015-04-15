using UnityEngine;
using System.Collections;

public class howlWhenNightComes : MonoBehaviour {
	private bool switched;
	public AudioClip howl;
	public DayNightCycle dayCycle;
	private AudioSource source;
	// Use this for initialization
	void Start () {
		switched=false;
		source = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		if (dayCycle.isNightTime () && switched == false) {
			switched = true;
			source.PlayOneShot (howl);
		}else if (!dayCycle.isNightTime () && switched == true) {
			switched = false;
		}
	}
}
