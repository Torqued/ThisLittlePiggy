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

    public int hourOfDay() {
        return (7 + (int)(timeOfDay() * 24)) % 24;
    }

    public float dayNightLerp() {
        float time = timeOfDay();

        if (time >= 0.55 && time <= 0.95) {
            return 0;
        }
        else if (time >= 0.05 && time <= 0.45) {
            return 1;
        }
        else if (time > 0.45 && time < 0.55) {
            return Mathf.Clamp01(1 - (time - 0.45f) * 10);
        }
        else {
            return Mathf.Clamp01((time + 0.05f) % 1 * 10);
        }
    }

    public bool isNightTime() {
        return false;
    }
}