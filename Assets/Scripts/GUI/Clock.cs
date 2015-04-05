using UnityEngine;
using System.Collections;

public class Clock : MonoBehaviour {
    private DayNightCycle dayNightCycle;

	void Start () {
        GameObject[] gameControllers = GameObject.FindGameObjectsWithTag("GameController");
        if (gameControllers.Length != 1) {
            Debug.LogError("Make sure this scene has exactly 1 object tagged \"GameController\".");
            Debug.Break();
        }
        
        if ((dayNightCycle = gameControllers[0].GetComponent<DayNightCycle>()) == null) {
            Debug.LogError("The GameController in this scene must have a \"DayNightCycle\" component.");
            Debug.Break();
        }
	}
	
	void Update () {
        float angle = -77.5f + dayNightCycle.timeOfDay()*360;
        transform.localEulerAngles = new Vector3(0, 0, angle);
	}
}
