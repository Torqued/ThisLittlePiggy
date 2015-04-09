using UnityEngine;
using System.Collections;

public enum ItemType {
	Grass,
    Straw,
    Sticks,
    Bricks,
	Rope
}

public class Item : MonoBehaviour {
    public ItemType itemType;

    public Renderer modelRenderer;

    void Start() {
        Transform child;

        if ((child = transform.Find("Model")) == null) {
            Debug.LogError("This GameObject must have a child named \"Model\".");
            Debug.Break();
        }

        if ((modelRenderer = child.gameObject.GetComponent<Renderer>()) == null) {
            Debug.LogError("The Model needs a Renderer component.");
            Debug.Break();
        }
    }

    void Update() {

    }

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