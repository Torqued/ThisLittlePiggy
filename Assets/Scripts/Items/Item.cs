using UnityEngine;
using System.Collections;

public enum ItemType {
	Grass,
    Straw,
    Sticks,
    Brick,
	Fairy,
	Rope
}

public class Item : MonoBehaviour {
    public ItemType itemType;

	void OnTriggerEnter(Collider collision) {
		if (collision.gameObject.tag == "Player") {
			CharacterControls playerControls = collision.gameObject.GetComponent<CharacterControls>();
			playerControls.setCurrentItem(this);
		}
	}

	void OnTriggerExit(Collider collision) {
		if (collision.gameObject.tag == "Player") {
			CharacterControls playerControls = collision.gameObject.GetComponent<CharacterControls>();
			playerControls.setCurrentItem(null);
		}
	}
}