using UnityEngine;
using System.Collections;

public class Button : MonoBehaviour {

    GameObject icon_back;
    GameObject icon_lit;
    GameObject icon_pressed;

    private bool lit;
    private bool pressed;
    private bool needUpdate;

	void Start () {
        icon_back = transform.Find("Icon_Back").gameObject;
        icon_lit = transform.Find("Icon_Lit").gameObject;
        icon_pressed = transform.Find("Icon_Pressed").gameObject;
        
        icon_back.SetActive(true);
        icon_lit.SetActive(false);
        icon_pressed.SetActive(false);
	}
	
    public void setLit() {
        lit = true;
        needUpdate = true;
    }

    public void setPressed() {
        pressed = true;
        needUpdate = true;
    }

    public void onClick() {
        Debug.Log("Clicked " + gameObject.name);
    }

    void LateUpdate() {
        if (needUpdate) {
            icon_back.SetActive(!(lit || pressed));
            icon_lit.SetActive(lit && !pressed);
            icon_pressed.SetActive(pressed);

            needUpdate = lit = pressed = false;
        }
    }
}
