using UnityEngine;
using System.Collections;

public class Minimap : MonoBehaviour {

    #region Serializable Fields
    public Vector2 cameraPosition;
    public float mapSize;
    #endregion

    #region Private Instance Variables
    private GameObject player;
    private Camera mapCamera;
    #endregion

    void Start () {
        GameObject[] players = GameObject.FindGameObjectsWithTag ("Player");

        if (players.Length != 1) {
            Debug.LogError("This scene must have exactly one GameObject tagged \"Player\".");
            Debug.Break();
        }

        #region Camera Initialization
        mapCamera = gameObject.GetComponent<Camera>();
        if (mapCamera == null) {
            mapCamera = new Camera();
        }

        recalculateMinimapSize();
        #endregion

        player = players[0];
    }

    void OnGui() {
        Debug.Log("Hello");
        Rect pixelRect = mapCamera.pixelRect;
        GUI.Box(new Rect(pixelRect.left - 1, pixelRect.top - 1, pixelRect.width + 1, pixelRect.height + 1), "");
    }

    void Update() {
        transform.position = player.transform.position + Vector3.up * 100;
    }

    private void recalculateMinimapSize() {
        mapCamera.backgroundColor = Color.black;
        mapCamera.clearFlags = CameraClearFlags.SolidColor;
        
        float screenAspect = 1.0f * Screen.width / Screen.height;
        float height = mapSize;
        float width = height / screenAspect;
        mapCamera.rect = new Rect(1 - cameraPosition.x / screenAspect, 1-cameraPosition.y, width, height);
    }
}
