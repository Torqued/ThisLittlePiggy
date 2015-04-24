using UnityEngine;
using System.Collections;

public class AlwaysFaceCamera : MonoBehaviour {
	float initx = 0;
	float initz = 0;

	void Awake() {
		initx = transform.rotation.x;
		initz = transform.rotation.z;
	}
	// Update is called once per frame
	void Update () {
		Vector3 relativePos = Camera.main.transform.position - transform.position;
		Quaternion rotation = Quaternion.LookRotation(relativePos);
		rotation.x = initx;
		rotation.z = initz;
		transform.rotation = rotation;
	}
}

