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
    public KeyCode toggleHUD = KeyCode.E;
    public KeyCode guiMode = KeyCode.Tab;
    public KeyCode pause = KeyCode.Escape;
    public KeyCode cycleViewForward = KeyCode.X;
    public KeyCode cycleViewBack = KeyCode.Z;
}

[System.Serializable]
public class CameraView {
    public string name;
    public Vector3 view;
    public float viewHeight;

    private GameObject ghostCamera;

    public void initCamera(Transform transform) {
        ghostCamera = new GameObject(name);
        ghostCamera.transform.localPosition = view;
        ghostCamera.transform.LookAt(transform.position + Vector3.up * viewHeight);
    }

    public GameObject getCamera() {
        return ghostCamera;
    }
}

[System.Serializable]
public class CameraSettings {
    public float sensitivity;
    public CameraView[] views;

    public GameObject getView(int i) {
        return views[((i % views.Length + views.Length) + views.Length) % views.Length].getCamera();
    }
}
#endregion

public class CharacterControls : MonoBehaviour {

    #region Serializable Fields
    public float walkSpeed = 10;
    public float runSpeed = 15;
    public float maxStamina = 100;
    public float staminaDecay = 0.005f;
    public float staminaRecovery = 0.05f;
    public float staminaPenaltyPercent = 0.25f;
    public float staminaSpeedPenalty = 0.7f;
    public KeySettings keySettings;
    public CameraSettings cameraSettings;
    #endregion

    #region Private Instance Variables
    private Animator characterAnimator;
    private CharacterInventory inventory;
    private GameObject mainCamera;
    private GameObject characterModel;
    private GameObject gameController;
    private GameObject ghostCamera_current;
    private GameObject ghostCamera_target;
    private GameObject guiCamera;
    private HUD_Movement inventoryMovement;
    private HUD_Movement craftingMovement;
    private HashIds hash;
    private Item currentItem;
    private Rigidbody rigidBody;
    private bool cameraLocked;
    private bool gamePaused;
    private bool forward, wasForward;
    private bool backward, wasBackward;
    private bool strafeLeft, wasLeft;
    private bool strafeRight, wasRight;
    private bool isResting;
    private int enemyAggro;
    private int cameraIndex;
    private float playerStamina;
    private float playerYaw, nextPlayerYaw;
    #endregion

    void Start() {
        #region Initializing fields with error handling
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


        Transform child;
        if ((child = transform.Find("Model")) == null) {
            Debug.LogError("This object is missing a child object called \"Model\".");
            Debug.Break();
        }

        characterModel = child.gameObject;
        
        if ((child = transform.Find("GUI Camera")) == null) {
            Debug.LogError("This object is missing the child object \"GUI Camera\".");
            Debug.Break();
        }
        guiCamera = child.gameObject;

        if ((child = guiCamera.transform.Find("Inventory")) == null) {
            Debug.LogError("The GUI Camera is missing a child object \"Inventory\".");
            Debug.Break();
        }

        if ((inventoryMovement = child.gameObject.GetComponent<HUD_Movement>()) == null) {
            Debug.LogError("\"Inventory\" must have a HUD_Movement component.");
            Debug.Break();
        }
        
        if ((child = guiCamera.transform.Find("Crafting")) == null) {
            Debug.LogError("The GUI Camera is missing a child object \"Crafting\".");
            Debug.Break();
        }
        
        if ((craftingMovement = child.gameObject.GetComponent<HUD_Movement>()) == null) {
            Debug.LogError("\"Crafting\" must have a HUD_Movement component.");
            Debug.Break();
        }


        if ((rigidBody = GetComponent<Rigidbody>()) == null) {
            Debug.LogError("There is no rigidbody attached to " + gameObject + ".");
            Debug.Break();
        }
        
        if ((inventory = GetComponent<CharacterInventory>()) == null) {
            Debug.LogError("There is no CharacterInventory script attached to " + gameObject + ".");
            Debug.Break();
        }
        #endregion

        #region Initializing Dummy Camera Objects

        for (int i = 0; i < cameraSettings.views.Length; i++) {
            cameraSettings.views[i].initCamera(transform);
        }

        ghostCamera_target = cameraSettings.getView(cameraIndex);
        
        ghostCamera_current = new GameObject("Current View");
        ghostCamera_current.transform.parent = transform;
        ghostCamera_current.transform.localPosition = ghostCamera_target.transform.localPosition;
        ghostCamera_current.transform.localRotation = ghostCamera_target.transform.localRotation;
        #endregion

        nextPlayerYaw = playerYaw = transform.localEulerAngles.y;

        Cursor.visible = cameraLocked = false;
        Cursor.lockState = CursorLockMode.Locked;

        playerStamina = maxStamina;

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

        nextVelocity = Vector3.Normalize(nextVelocity) * (enemyAggro > 0 ? runSpeed : walkSpeed) *
            (playerStamina / maxStamina < staminaPenaltyPercent ? 0.7f : 1);

        rigidBody.velocity = new Vector3(nextVelocity.x, rigidBody.velocity.y, nextVelocity.z);
    }
    #endregion

    void Update() {
        checkKeyInputs();
        checkMouseInputs();

        if (isResting) {
            playerStamina = Mathf.Clamp(playerStamina + staminaRecovery, 0, maxStamina);
        }
        else {
            playerStamina = Mathf.Clamp(playerStamina - staminaDecay, 0, maxStamina);
        }
    }

    #region Keyboard Inputs
    private void checkKeyInputs() {
        if (Input.GetKeyDown(keySettings.forward)) {
            forward = wasForward = !(backward = false);
        }
        else if (Input.GetKeyUp(keySettings.forward)) {
            backward = wasBackward;
        }
        if (Input.GetKeyDown(keySettings.back)) {
            backward = wasBackward = !(forward = false);
        }
        else if (Input.GetKeyUp(keySettings.back)) {
            forward = wasForward;
        }
        if (Input.GetKeyDown(keySettings.left)) {
            strafeLeft = wasLeft = !(strafeRight = false);
        }
        else if (Input.GetKeyUp(keySettings.left)) {
            strafeRight = wasRight;
        }
        if (Input.GetKeyDown(keySettings.right)) {
            strafeRight = wasRight = !(strafeLeft = false);
        }
        else if (Input.GetKeyUp(keySettings.right)) {
            strafeLeft = wasLeft;
        }

        if (!Input.GetKey(keySettings.forward)) {
            forward = wasForward = false;
        }
        if (!Input.GetKey(keySettings.back)) {
            backward = wasBackward = false;
        }
        if (!Input.GetKey(keySettings.left)) {
            strafeLeft = wasLeft = false;
        }
        if (!Input.GetKey(keySettings.right)) {
            strafeRight = wasRight = false;
        }

        if (Input.GetKeyDown(keySettings.guiMode)) {
            cameraLocked = !cameraLocked;

            if (cameraLocked) {
                Cursor.lockState = CursorLockMode.None;
                inventoryMovement.toggleHUD(true);
                craftingMovement.toggleHUD(true);
            }
            else {
                Cursor.lockState = CursorLockMode.Locked;
                inventoryMovement.toggleHUD(false);
                craftingMovement.toggleHUD(false);
            }
            Cursor.visible = cameraLocked;
        }

        if (Input.GetKeyDown(keySettings.toggleHUD) && !cameraLocked) {
            inventoryMovement.toggleHUD(!inventoryMovement.getState());
        }

        if (Input.GetKeyDown(keySettings.cycleViewForward)) {
            ghostCamera_target = cameraSettings.getView(++cameraIndex);
        }

        if (Input.GetKeyDown(keySettings.cycleViewBack)) {
            ghostCamera_target = cameraSettings.getView(--cameraIndex);
        }

        if (Input.GetKeyDown(keySettings.pause)) {
            gamePaused = !gamePaused;
        }
    }
    #endregion

    #region Mouse Inputs
    private void checkMouseInputs() {
        if (!cameraLocked) {
            playerYaw += Input.GetAxis("Mouse X") * cameraSettings.sensitivity;
        }
        
        transform.localEulerAngles = new Vector3(0, playerYaw, 0);
        
        castMouseRays();
    }
    #endregion

    #region Cast Mouse Ray for Highlighting Targets/GUI Interactions
    void castMouseRays() {
        RaycastHit collisionInfo;
        Ray mouseRay;

        if (cameraLocked) { // Cast ray from mouse location
            Vector3 mousePosition = Input.mousePosition;
            mouseRay = mainCamera.GetComponent<Camera>().ScreenPointToRay(mousePosition);
            //Debug.DrawRay(mouseRay.origin, 10 * mouseRay.direction, Color.cyan);
            
            #region GUI Interactions
            Ray mouseRayGUI = guiCamera.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
            
            if (Physics.Raycast(mouseRayGUI.origin, mouseRayGUI.direction, out collisionInfo, 100, 1<<9)) {
                Button button = collisionInfo.collider.gameObject.GetComponent<Button>();

                if (Input.GetMouseButton(0)) {
                    button.setPressed();
                }
                else {
                    button.setLit();
                }

                if (Input.GetMouseButtonUp(0)) {
                    button.buttonClicked();
                }
            }
            #endregion
        }
        else { // Free look mode, cast ray from center of screen
            mouseRay = mainCamera.GetComponent<Camera>().ViewportPointToRay(new Vector3(0.5f, 0.5f));
            //Debug.DrawRay(mainCamera.transform.position, 10*mainCamera.transform.forward, Color.green);
        }
        if (Physics.Raycast(mouseRay.origin, mouseRay.direction, out collisionInfo, 100, 1<<8)) {
            MouseTarget target;
            
            if((target = collisionInfo.transform.gameObject.GetComponent<MouseTarget>()) != null) {
                target.setOutline();
            }
        }
    }
    #endregion

    void LateUpdate() {
        updateCameraPosition();
        updateAnimations();
    }

    private void updateCameraPosition() {

        ghostCamera_current.transform.localPosition = Vector3.Lerp(
            ghostCamera_current.transform.localPosition,
            ghostCamera_target.transform.localPosition,
            0.2f
        );
        ghostCamera_current.transform.localRotation = Quaternion.Lerp(
            ghostCamera_current.transform.localRotation,
            ghostCamera_target.transform.localRotation,
            0.2f
        );

        mainCamera.transform.position = ghostCamera_current.transform.position;
        mainCamera.transform.rotation = ghostCamera_current.transform.rotation;
        
        //Debug.DrawRay(transform.position, transform.forward * 10, Color.yellow);
        //Debug.DrawRay(transform.position, mainCamera.transform.position - transform.position, Color.red);
    }

    private void updateAnimations() {
        characterAnimator.SetBool(hash.runningBool, forward || backward || strafeLeft || strafeRight);

		characterAnimator.SetBool(hash.panicBool, enemyAggro > 0);
		
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

    public void addItem(Item item) {
        inventory.addItem(item.itemType);
    }

    public void increaseAggro() {
        enemyAggro++;
    }

    public void decreaseAggro() {
        enemyAggro--;
    }

    public void setResting(bool b) {
        isResting = b;
    }

    public bool getResting() {
        return this.isResting;
    }

    public void damageStamina(float amount) {
        if (playerStamina <= 0.01) {
            playerDeath();
        }
        else {
            playerStamina = Mathf.Clamp(playerStamina - amount, 0, maxStamina);
        }
    }

    private void playerDeath() {
		Application.LoadLevel("Death");
    }

    public float getStaminaPercent() {
        return playerStamina / maxStamina;
    }
}