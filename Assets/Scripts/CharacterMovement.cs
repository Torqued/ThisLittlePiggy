using UnityEngine;
using System.Collections;

#region Serializable Classes
[System.Serializable]
public class KeySettings {
    public KeyCode forward = KeyCode.W;
    public KeyCode back = KeyCode.S;
    public KeyCode left = KeyCode.A;
    public KeyCode right = KeyCode.D;
    public KeyCode interact = KeyCode.F;
    public KeyCode guiMode = KeyCode.Escape;
}

[System.Serializable]
public class CameraSettings {
    public Vector2 defaultCameraOffset;
    public float cameraLookHeightOffset;
    public float minPitch, maxPitch;
    public float minZoom, maxZoom;
    public float sensitivity;
}
#endregion

public class CharacterMovement : MonoBehaviour {

    #region Serializable Fields
    public float moveSpeed;
    public KeySettings keySettings;
    public CameraSettings cameraSettings;
    #endregion

    #region Private Instance Variables
    private GameObject mainCamera;
    private GameObject characterModel;
    private Rigidbody rigidBody;
    private bool cameraLocked;
    private bool forward, wasForward;
    private bool backward, wasBackward;
    private bool strafeLeft, wasLeft;
    private bool strafeRight, wasRight;

    private float yaw, pitch, cameraZoom, nextCameraZoom;
    #endregion

    #region Animation and Global Data Variables
    private HashIDs hash;
    private GameObject gameController;
    private Animator characterAnimator;
    #endregion

	private Vector3 startPos = new Vector3(41,18,216); //JORDAN'S EDIT DONT CHANGE

    void Start() {
        GameObject[] mainCameras = GameObject.FindGameObjectsWithTag("MainCamera");

        if (mainCameras.Length != 1) {
            Debug.LogError("Make sure this scene has only 1 object tagged \"MainCamera\".");
            Debug.Break();
        }
        
        mainCamera = mainCameras [0];

        Transform modelTransform = transform.Find("Model");
        if (modelTransform == null) {
            Debug.LogError("This object is missing a child object called \"Model\".");
            Debug.Break();
        }

        characterModel = modelTransform.gameObject;

        if ((rigidBody = GetComponent<Rigidbody>()) == null) {
            Debug.LogError("There is no rigidbody attached to " + gameObject + ".");
            Debug.Break();
        }

        yaw = transform.localEulerAngles.y;
        pitch = transform.localEulerAngles.x;

        cameraZoom = nextCameraZoom = 1;
        cameraLocked = false;

		//Animation Vars
		gameController = GameObject.FindGameObjectWithTag("GameController");
		hash = gameController.GetComponent<HashIDs>();
		characterAnimator = characterModel.GetComponent<Animator>();
    }

    #region Player Movement
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
    #endregion

    void Update() {
        checkKeyInputs();
        checkMouseInputs();
    }


	void OnApplicationFocus(bool focus) {
		forward = wasForward = backward = wasBackward = strafeLeft = wasLeft = strafeRight = wasRight = false;
	}

    #region Keyboard Inputs
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
		else if (Input.GetKey (KeyCode.LeftShift)){
			Vector3 switchPos = startPos;
			if(startPos.x == 41){
				startPos=new Vector3(111,152,-1828);
			}else{
				startPos=new Vector3(41,18,216);
			}
			transform.position=switchPos;
		}

        if (Input.GetKeyDown(keySettings.guiMode)) {
            cameraLocked = !cameraLocked;

            if (cameraLocked) {
                Cursor.lockState = CursorLockMode.None;
            }
            else {
                Cursor.lockState = CursorLockMode.Locked;
            }
            Cursor.visible = cameraLocked;
        }
    }
    #endregion

    #region Mouse Inputs
    private void checkMouseInputs() {
        if (!cameraLocked) {
            float h = Input.GetAxis("Mouse X") * cameraSettings.sensitivity;
            float v = Input.GetAxis("Mouse Y") * cameraSettings.sensitivity;
            float scroll = Input.GetAxis("Mouse ScrollWheel");

            yaw += h;
            pitch = Mathf.Clamp(pitch - v, cameraSettings.minPitch, cameraSettings.maxPitch);

            nextCameraZoom = Mathf.Clamp(
                nextCameraZoom - scroll * 0.5f,
                cameraSettings.minZoom,
                cameraSettings.maxZoom);
        }
        else {

        }
        
        transform.localEulerAngles = new Vector3(pitch, yaw, 0);
    }
    #endregion
        
    void LateUpdate() {
        Vector3 cameraOffset = Vector3.up * cameraSettings.defaultCameraOffset.y +
            transform.forward * cameraSettings.defaultCameraOffset.x;

        // Zeno's Paradox (creates smooth zooming)
        cameraZoom = Mathf.Lerp(cameraZoom, nextCameraZoom, 0.2f);

        Vector3 cameraPosition;

        #region Camera Collision
        RaycastHit collisionInfo;
        if (Physics.Raycast(transform.position, cameraOffset, out collisionInfo)) {
            Vector3 collisionOffset = Vector3.zero;
            if (Vector3.Dot(collisionInfo.normal, Vector3.up) > 0) {
                collisionOffset = Vector3.up;
            }
            if ((cameraOffset * cameraZoom).sqrMagnitude > collisionInfo.distance * collisionInfo.distance) {
                cameraPosition = transform.position + Vector3.Normalize(cameraOffset) * (collisionInfo.distance) + collisionOffset;
            }
            else {
                cameraPosition = transform.position + cameraOffset * cameraZoom + collisionOffset;
            }
        }
        else if (Physics.Raycast(transform.position, cameraOffset + Vector3.up, out collisionInfo)) {
            Vector3 collisionOffset = Vector3.zero;
            if (Vector3.Dot(collisionInfo.normal, Vector3.up) > 0) {
                collisionOffset = Vector3.up;
            }
            if ((cameraOffset * cameraZoom).sqrMagnitude > collisionInfo.distance * collisionInfo.distance) {
                cameraPosition = transform.position + Vector3.Normalize(cameraOffset) * (collisionInfo.distance) + collisionOffset;
            }
            else {
                cameraPosition = transform.position + cameraOffset * cameraZoom + collisionOffset;
            }
        }
        else {
            cameraPosition = transform.position + cameraOffset * cameraZoom + Vector3.up;
        }
        #endregion

        mainCamera.transform.position = cameraPosition;

        Debug.DrawRay(transform.position, transform.forward * 10, Color.yellow);
        Debug.DrawRay(transform.position, mainCamera.transform.position - transform.position, Color.red);

        // Force pitch to 0 so that the player doesn't bend up or down
        transform.localEulerAngles = new Vector3(0, yaw, 0);

        mainCamera.transform.LookAt(transform.position + cameraSettings.cameraLookHeightOffset*Vector3.up);

        updateAnimations();
    }

    private void updateAnimations() {
        if (forward) {
            characterModel.transform.localEulerAngles = new Vector3(0, 0, 0);
			characterAnimator.SetBool(hash.runningBool, true);
        }
        else if (backward) {
            characterModel.transform.localEulerAngles = new Vector3(0, 180, 0);
			characterAnimator.SetBool(hash.runningBool, true);
        }
        else if (strafeLeft) {
            characterModel.transform.localEulerAngles = new Vector3(0, -90, 0);
			characterAnimator.SetBool(hash.runningBool, true);
        }
        else if (strafeRight) {
            characterModel.transform.localEulerAngles = new Vector3(0, 90, 0);
			characterAnimator.SetBool(hash.runningBool, true);
        }
        else {
			characterAnimator.SetBool(hash.runningBool, false);
        }
    }
}