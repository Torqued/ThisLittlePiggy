using UnityEngine;
using System.Collections;

public class MouseTarget : MonoBehaviour {

    public Color outlineColor;

    private Renderer outlineRenderer;
    private bool outline;
    private bool needUpdate;

	void Start () {
        Transform child;
        if ((child = transform.Find("Model")) == null) {
            Debug.LogError("This object is missing a child object called \"Model\".");
            Debug.Break();
        }

        if ((outlineRenderer = child.gameObject.GetComponent<Renderer>()) == null) {
            Debug.LogError("The \"Model\" GameObject must have a renderer component.");
            Debug.Break();
        }

        outlineRenderer.material.SetColor("_OutlineColor", Color.clear);
	}
	
    public void setOutline() {
        outline = true;
        needUpdate = true;
    }

    void LateUpdate() {
        if (needUpdate) {
            if (outline) {
                outlineRenderer.material.SetColor("_OutlineColor", outlineColor);
                outline = false;
            }
            else {
                outlineRenderer.material.SetColor("_OutlineColor", Color.clear);
                needUpdate = false;
            }
        }
    }
}
