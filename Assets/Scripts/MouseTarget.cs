using UnityEngine;
using System.Collections;

public class MouseTarget : MonoBehaviour {

    public Color outlineColor;

    private Renderer[] outlineRenderers;
    private bool outline;
    private bool needUpdate;

	void Start () {
        Transform child;
        if ((child = transform.Find("Model")) == null) {
            Debug.LogError("This object is missing a child object called \"Model\".");
            Debug.Break();
        }

        outlineRenderers = child.gameObject.GetComponents<Renderer>();

        foreach (Renderer r in outlineRenderers) {
            r.material.SetColor("_OutlineColor", Color.clear);
        }
	}
	
    public void setOutline() {
        outline = true;
        needUpdate = true;
    }

    void LateUpdate() {
        if (needUpdate) {
            if (outline) {
                foreach (Renderer r in outlineRenderers) {
                    r.material.SetColor("_OutlineColor", outlineColor);
                }
                outline = false;
            }
            else {
                foreach (Renderer r in outlineRenderers) {
                    r.material.SetColor("_OutlineColor", Color.clear);
                }
                needUpdate = false;
            }
        }
    }
}
