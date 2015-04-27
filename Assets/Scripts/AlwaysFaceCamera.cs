using UnityEngine;
using System.Collections;

public class AlwaysFaceCamera : MonoBehaviour {
	
	// Update is called once per frame
	void Update () {
		transform.LookAt(transform.position + Camera.main.transform.rotation * Vector3.back, Camera.main.transform.rotation * Vector3.up);
	}
}

