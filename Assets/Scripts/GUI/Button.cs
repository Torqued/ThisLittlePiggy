using UnityEngine;
using System.Collections;

public class Button : MonoBehaviour {

    GameObject icon_back;
    GameObject icon_lit;
    GameObject icon_pressed;

    _ButtonAction buttonAction;

    private bool lit;
    private bool pressed;

	void Start () {
        icon_back = transform.Find("Icon_Back").gameObject;
        icon_lit = transform.Find("Icon_Lit").gameObject;
        icon_pressed = transform.Find("Icon_Pressed").gameObject;

        buttonAction = transform.Find("Button_Pressed").gameObject.GetComponent<_ButtonAction>();
        
        icon_back.SetActive(true);
        icon_lit.SetActive(false);
        icon_pressed.SetActive(false);
	}
	
    public void setLit() {
        lit = true;
    }

    public void setPressed() {
        pressed = true;
    }

    public void buttonClicked() {
        buttonAction.buttonClicked();
    }

    void LateUpdate() {
        if (lit || pressed) {
            icon_back.SetActive(false);
            icon_lit.SetActive(lit && !pressed);
            icon_pressed.SetActive(pressed);

            lit = pressed = false;
        }
        else {
            icon_back.SetActive(true);
            icon_lit.SetActive(false);
            icon_pressed.SetActive(false);
        }
    }
}
