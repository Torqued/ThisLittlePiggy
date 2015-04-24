﻿using UnityEngine;
using System.Collections;

public enum ItemType {
	Grass,
    Straw,
    Sticks,
    Bricks,
	Rope,
    Fairy
}

public class Item : MonoBehaviour {
    public ItemType itemType;
	public AudioClip Pickup;
	void OnTriggerEnter(Collider collision) {
		if (collision.gameObject.tag == "Player") {
			CharacterControls playerControls = collision.gameObject.GetComponent<CharacterControls>();
			playerControls.addItem(this);
			AudioSource.PlayClipAtPoint (Pickup, transform.position);
			Object.Instantiate((Resources.Load("Effects/Poof", typeof(GameObject)) as GameObject), transform.position, Quaternion.identity);
            GameObject.Destroy(gameObject);
		}
	}
}