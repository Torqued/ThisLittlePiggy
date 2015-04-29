using UnityEngine;
using System.Collections;

public class RotateWithPlayer : MonoBehaviour {
    
    private GameObject player;

	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
	}
	
	void LateUpdate () {
        Vector3 rot = player.transform.localEulerAngles;
        transform.localEulerAngles = new Vector3(90, 0, -rot.y);
	}
}
