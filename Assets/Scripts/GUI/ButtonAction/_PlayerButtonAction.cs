using UnityEngine;
using System.Collections;

public abstract class _PlayerButtonAction : _ButtonAction {

    protected CharacterController player;
    protected CharacterInventory playerInventory;

	void Start () {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterController>();
        playerInventory = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterInventory>();
	}
}
