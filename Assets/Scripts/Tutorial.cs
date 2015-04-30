using UnityEngine;
using System.Collections;

public class Tutorial : MonoBehaviour {
	public GameObject tutText;
	SpriteRenderer spRenderer;
	public Sprite[] tutlines;
	private int index;

	void Awake() {
		spRenderer = tutText.GetComponent<SpriteRenderer>();
	}

	void OnMouseDown() {
		index = (index+1)%tutlines.Length;
		spRenderer.sprite = tutlines[index];
	}

}
