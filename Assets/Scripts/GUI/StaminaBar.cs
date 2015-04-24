using UnityEngine;
using System.Collections;

public class StaminaBar : MonoBehaviour {
    private CharacterControls player;

    void Start () {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterControls>();
    }

	void Update () {
        float rotationAmount = Mathf.LerpAngle(-125.51f, 43.23f, player.getStaminaPercent());
        transform.localEulerAngles = new Vector3(0, 0, rotationAmount); 
	}
}
