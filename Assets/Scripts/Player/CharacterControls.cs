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
    public KeyCode guiMode = KeyCode.Tab;
    public KeyCode pause = KeyCode.Escape;
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

public class CharacterControls : MonoBehaviour {

    #region Serializable Fields
    public float moveSpeed;
    public KeySettings keySettings;
    public CameraSettings cameraSettings;
    #endregion

    #region Private Instance Variables
    private Animator characterAnimator;
    private CharacterInventory inventory;
    private GameObject mainCamera;
    private GameObject characterModel;
    private GameObject gameController;
    private Item currentItem;
    private HashIds hash;
    private Rigidbody rigidBody;
    private bool cameraLocked;
    private bool gamePaused;
    private bool forward, wasForward;
    private bool backward, wasBackward;
    private bool strafeLeft, wasLeft;
    private bool strafeRight, wasRight;
    private float yaw, pitch, cameraZoom, nextCameraZoom;
    private float nextPlayerYaw;
    #endregion

    void Start() {
        GameObject[] mainCameras = GameObject.FindGameObjectsWithTag("MainCamera");

        if (mainCameras.Length != 1) {
            Debug.LogError("Make sure this scene has exactly 1 object tagged \"MainCamera\".");
            Debug.Break();
        }
        
        mainCamera = mainCameras[0];
        
        
        GameObject[] gameControllers = GameObject.FindGameObjectsWithTag("GameController");
        if (gameControllers.Length != 1) {
            Debug.LogError("Make sure this scene has exactly 1 object tagged \"GameController\".");
            Debug.Break();
        }

        gameController = gameControllers[0];

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
        
        if ((inventory = GetComponent<CharacterInventory>()) == null) {
            Debug.LogError("There is no CharacterInventory script attached to " + gameObject + ".");
            Debug.Break();
        }

        yaw = transform.localEulerAngles.y;
        pitch = transform.localEulerAngles.x;

        cameraZoom = nextCameraZoom = 1;
        cameraLocked = false;

        //Animation
        hash = gameController.GetComponent<HashIds>();
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

        if (Input.GetKeyDown(keySettings.pause)) {
            gamePaused = !gamePaused;
        }

        if (Input.GetKeyDown(keySettings.interact)) {
            if (currentItem != null) {
                inventory.addItem(currentItem.itemType);
                GameObject.Destroy(currentItem.gameObject);
            }
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

        mainCamera.transform.LookAt(transform.position + cameraSettings.cameraLookHeightOffset * Vector3.up);

        updateAnimations();
    }

    private void updateAnimations() {
        characterAnimator.SetBool(hash.runningBool, forward || backward || strafeLeft || strafeRight);

        #region Player Rotation
        if (forward && !strafeLeft && !strafeRight) {
            nextPlayerYaw = 0;
        }
        else if (forward && strafeLeft) {
            nextPlayerYaw = -45;
        }
        else if (forward && strafeRight) {
            nextPlayerYaw = 45;
        }
        else if (backward && !strafeLeft && !strafeRight) {
            nextPlayerYaw = 180;
        }
        else if (backward && strafeLeft) {
            nextPlayerYaw = -135;
        }
        else if (backward && strafeRight) {
            nextPlayerYaw = 135;
        }
        else if (strafeLeft) {
            nextPlayerYaw = -90;
        }
        else if (strafeRight) {
            nextPlayerYaw = 90;
        }

        float playerYaw = Mathf.LerpAngle(characterModel.transform.localEulerAngles.y, nextPlayerYaw, 0.2f);
        characterModel.transform.localEulerAngles = new Vector3(0, playerYaw, 0);
        #endregion
    }

    public void setCurrentItem(Item item) {
        currentItem = item;
    }
}