using UnityEngine;
using System.Collections;

public class DayNightCycle : MonoBehaviour {

    public float dayNightLength = 180; // Seconds

    public Gradient lightColors;
    public Gradient skyColors;

    private Light sun;
    private Camera mainCamera;

    void Start () {
        sun = transform.Find("Sun").gameObject.GetComponent<Light>();
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

	void Update () {
        sun.color = lightColors.Evaluate(dayNightLerp());
        sun.intensity = Mathf.Clamp(dayNightLerp(), 0.1f, 1);

        mainCamera.backgroundColor = skyColors.Evaluate(dayNightLerp());
	}

    public float timeOfDay() {
        return Time.time % dayNightLength / dayNightLength;
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
        float time = timeOfDay();

        return time >= 0.55 && time <= 0.95;
    }
}