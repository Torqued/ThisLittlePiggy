using UnityEngine;
using System.Collections;

[System.Serializable]
public class KeySettings {
    public KeyCode forward;
    public KeyCode back;
    public KeyCode left;
    public KeyCode right;
    public KeyCode interact;
}

[System.Serializable]
public class CameraSettings {
    public Vector2 defaultCameraOffset;
    public float minPitch, maxPitch;
    public float minZoom, maxZoom;
    public float sensitivity;
}

public class CharacterMovement : MonoBehaviour {
    public float moveSpeed;
    public KeySettings keySettings;
    public CameraSettings cameraSettings;
    private GameObject mainCamera;
    private Rigidbody rigidBody;
    private bool forward, wasForward;
    private bool backward, wasBackward;
    private bool strafeLeft, wasLeft;
    private bool strafeRight, wasRight;
    private float yaw, pitch, cameraZoom, nextCameraZoom;

    void Start() {
        GameObject[] mainCameras = GameObject.FindGameObjectsWithTag("MainCamera");
        if (mainCameras.Length != 1) {
            Debug.LogError("Make sure this scene has only 1 object tagged \"MainCamera\"");
            Debug.Break();
        }
        mainCamera = mainCameras [0];

        if ((rigidBody = GetComponent<Rigidbody>()) == null) {
            Debug.LogError("There is no rigidbody attached to " + gameObject + ".");
            Debug.Break();
        }

        yaw = transform.localEulerAngles.y;
        pitch = transform.localEulerAngles.x;

        cameraZoom = nextCameraZoom = 1;
    }

    void FixedUpdate() {
        Vector3 nextVelocity = new Vector3();

        if (forward) {
            nextVelocity = transform.forward;
        }
        else if (backward) {
            nextVelocity = -transform.forward;
        }

        if (strafeLeft) {
            nextVelocity += Vector3.Cross(transform.forward, Vector3.up);
        }
        else if (strafeRight) {
            nextVelocity += Vector3.Cross(Vector3.up, transform.forward);
        }

        nextVelocity = Vector3.Normalize(nextVelocity) * moveSpeed;

        rigidBody.velocity = new Vector3(nextVelocity.x, rigidBody.velocity.y, nextVelocity.z);
    }

    void Update() {
        checkKeyInputs();
        checkMouseInputs();
        updateAnimations();
    }

    private void checkKeyInputs() {
        if (Input.GetKeyDown(keySettings.forward)) {
            forward = wasForward = !(backward = false);
        }
        else if (Input.GetKeyUp(keySettings.forward)) {
            forward = wasForward = false;
            backward = wasBackward;
        }
        if (Input.GetKeyDown(keySettings.back)) {
            backward = wasBackward = !(forward = false);
        }
        else if (Input.GetKeyUp(keySettings.back)) {
            backward = wasBackward = false;
            forward = wasForward;
        }
        if (Input.GetKeyDown(keySettings.left)) {
            strafeLeft = wasLeft = !(strafeRight = false);
        }
        else if (Input.GetKeyUp(keySettings.left)) {
            strafeLeft = wasLeft = false;
            strafeRight = wasRight;
        }
        if (Input.GetKeyDown(keySettings.right)) {
            strafeRight = wasRight = !(strafeLeft = false);
        }
        else if (Input.GetKeyUp(keySettings.right)) {
            strafeRight = wasRight = false;
            strafeLeft = wasLeft;
        }
    }

    private void checkMouseInputs() {
        float h = Input.GetAxis("Mouse X") * cameraSettings.sensitivity;
        float v = Input.GetAxis("Mouse Y") * cameraSettings.sensitivity;
        float scroll = Input.GetAxis("Mouse ScrollWheel");

        yaw += h;
        pitch = Mathf.Clamp(pitch - v, cameraSettings.minPitch, cameraSettings.maxPitch);

        transform.localEulerAngles = new Vector3(pitch, yaw, 0);

        nextCameraZoom = Mathf.Clamp(
                nextCameraZoom - scroll * 0.5f,
                cameraSettings.minZoom,
                cameraSettings.maxZoom);
    }

    private void updateAnimations() {
        Renderer temp = GetComponent<MeshRenderer>();
        if (strafeLeft) {
            temp.material.color = Color.blue;
        }
        else if (strafeRight) {
            temp.material.color = Color.yellow;
        }
        else if (forward) {
            temp.material.color = Color.green;
        }
        else if (backward) {
            temp.material.color = Color.red;
        }
        else {
            temp.material.color = Color.white;
        }
    }
        
    void LateUpdate() {
        Vector3 cameraOffset = transform.forward * cameraSettings.defaultCameraOffset.x +
            Vector3.up * cameraSettings.defaultCameraOffset.y;

        // Zeno's Paradox (creates smooth zooming)
        cameraZoom = Mathf.Lerp(cameraZoom, nextCameraZoom, 0.2f);

        Vector3 cameraPosition;// = transform.position + cameraOffset * cameraZoom;

        RaycastHit collisionInfo;
        if (Physics.Raycast(transform.position, cameraOffset, out collisionInfo)) {
            if ((cameraOffset * cameraZoom).sqrMagnitude > collisionInfo.distance * collisionInfo.distance) {
                cameraPosition = transform.position + Vector3.Normalize(cameraOffset) * (collisionInfo.distance);
            }
            else {
                cameraPosition = transform.position + cameraOffset * cameraZoom;
            }
        }
        else {
            cameraPosition = transform.position + cameraOffset * cameraZoom;
        }

        mainCamera.transform.position = cameraPosition + Vector3.up;

        Debug.DrawRay(transform.position, transform.forward * 10, Color.yellow);
        Debug.DrawRay(transform.position, mainCamera.transform.position - transform.position, Color.red);

        // Force pitch to 0 so that the player doesn't bend up or down
        transform.localEulerAngles = new Vector3(0, yaw, 0);

        mainCamera.transform.LookAt(transform.position + Vector3.up);
    }
}