﻿using UnityEngine;
using System.Collections;

public class Minimap : MonoBehaviour {
    GameObject player;

    void Start() {
        player = GameObject.FindGameObjectWithTag("Player");
    }

	void Update() {
        transform.position = player.transform.position + Vector3.up * 100;
	}
}
