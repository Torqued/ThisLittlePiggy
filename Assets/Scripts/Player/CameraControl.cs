using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour {

    private Vector3 position;
    private Rigidbody rigidBody;

    void Start() {
        rigidBody = GetComponent<Rigidbody>();
    }

	void LateUpdate () {
        rigidBody.MovePosition(position);
	}

    public void setPosition(Vector3 pos) {
        position = pos;
    }
}
