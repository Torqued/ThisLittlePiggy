using UnityEngine;
using System.Collections;

public class StaminaBar : MonoBehaviour {
    private CharacterControls player;

    void Start () {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterControls>();
    }

	void Update () {
        float targetRotation = Mathf.LerpAngle(-125.51f, 43.23f, player.getStaminaPercent());
        
        float rotationAmount = Mathf.LerpAngle(transform.localEulerAngles.z, targetRotation, 0.2f);

        transform.localEulerAngles = new Vector3(0, 0, rotationAmount); 
	}
}
