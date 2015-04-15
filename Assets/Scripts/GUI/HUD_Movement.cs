using UnityEngine;
using System.Collections;

public class HUD_Movement : MonoBehaviour {

    public Vector3 onPosition;
    public Vector3 offPosition;

    private Vector3 targetPosition;
    private bool inventoryOn = false;

	void Start () {
        transform.localPosition = offPosition;
        targetPosition = offPosition;
	}
	
	void Update () {
        transform.localPosition = Vector3.Lerp(transform.localPosition, targetPosition, 0.2f);
	}

    public void toggleHUD() {
        targetPosition = (inventoryOn = !inventoryOn) ? onPosition : offPosition;
    }
}
