using UnityEngine;
using System.Collections;

public class AnimationAudio : MonoBehaviour {
	private AudioSource audio;

	void Awake() {
		audio = GetComponent<AudioSource>();
	}

	void MakeSound(AudioClip clip) {
		//Animation event for making sound.
		audio.clip = clip;
		audio.Play();
	}
}
