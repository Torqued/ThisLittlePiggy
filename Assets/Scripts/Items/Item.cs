using UnityEngine;
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

	void OnTriggerEnter(Collider collision) {
		if (collision.gameObject.tag == "Player") {
			CharacterControls playerControls = collision.gameObject.GetComponent<CharacterControls>();
			playerControls.addItem(this);
            GameObject.Destroy(gameObject);
		}
	}
}