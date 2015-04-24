using UnityEngine;
using System.Collections;

public class DayNightGui : MonoBehaviour {

    public Gradient baseColor;

    private DayNightCycle dayNightCycle;
    private GameObject day;
    private GameObject night;
    private GameObject baseUI;

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

        Transform child;

        if ((child = transform.Find("Day_GUI")) == null) {
            Debug.LogError("This GameObject must have a child named \"Day_GUI\".");
            Debug.Break();
        }
        day = child.gameObject;
        
        if ((child = transform.Find("Night_GUI")) == null) {
            Debug.LogError("This GameObject must have a child named \"Night_GUI\".");
            Debug.Break();
        }
        night = child.gameObject;
        
        if ((child = transform.Find("Base_GUI")) == null) {
            baseUI = null;
        }
        else {
            baseUI = child.gameObject;
        }
	}
	
	// Update is called once per frame
	void Update () {
        Color dayColor = Color.white;
        Color nightColor = Color.white;

        dayColor.a = dayNightCycle.dayNightLerp();
        nightColor.a = 1-dayNightCycle.dayNightLerp();

        day.GetComponent<Renderer>().material.color = dayColor;
        night.GetComponent<Renderer>().material.color = nightColor;

        if (baseUI != null) {
            baseUI.GetComponent<Renderer>().material.color = baseColor.Evaluate(dayNightCycle.dayNightLerp());
        }
	}
}
