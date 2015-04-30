using UnityEngine;
using System.Collections;

public class MenuButton : MonoBehaviour {

	private GameObject button; 
	public string sceneName; 


	// Use this for initialization
	void Awake () {
		button = this.gameObject;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown("Fire1")) {
			var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit; 
			Debug.Log("gets here1");
			if (Physics.Raycast(ray, out hit, 30.0f)) {
				Application.LoadLevel(sceneName);
				Debug.Log("gets here2");
			}
				
		}
	}


}
