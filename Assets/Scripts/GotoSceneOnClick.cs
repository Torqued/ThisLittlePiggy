using UnityEngine;
using System.Collections;

public class GotoSceneOnClick : MonoBehaviour {
	
	public string s;

	void OnMouseDown() {
		Application.LoadLevel(s);
	}
}
