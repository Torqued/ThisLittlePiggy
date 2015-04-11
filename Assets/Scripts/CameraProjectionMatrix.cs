using UnityEngine;
using System.Collections;

public class CameraProjectionMatrix : MonoBehaviour {

    Matrix4x4 projMat;
    Camera cam;

	void Start () {
        cam = GetComponent<Camera>();
        projMat = cam.projectionMatrix;

        cam.projectionMatrix = projMat;
	}
	
	void Update () {
	
	}
}
