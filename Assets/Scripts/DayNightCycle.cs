using UnityEngine;
using System.Collections;

public class DayNightCycle : MonoBehaviour {

	void Update () {
        float lightAngle = Time.time * 3;
        transform.eulerAngles = new Vector3(lightAngle, 0, 0);
	}

    public float timeOfDay() {
        return Time.time * 3 % 360 / 360;
    }

    public bool isNightTime() {
        return false;
    }
}