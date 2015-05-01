﻿using UnityEngine;
using System.Collections;

[RequireComponent (typeof(AudioSource))]

public class Video : MonoBehaviour {

	public string nextScene;
	public MovieTexture movie;
	// Use this for initialization

	void Start () {
		GetComponent<Renderer>().material.mainTexture = movie as MovieTexture;
		GetComponent<AudioSource>().clip = movie.audioClip;
		movie.Play ();
		GetComponent<AudioSource> ().Play ();
	}

	void Update(){
		if (!movie.isPlaying) {
			Application.LoadLevel(nextScene);
		}
	}
}
