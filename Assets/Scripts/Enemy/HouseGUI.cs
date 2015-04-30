using UnityEngine;
using System.Collections;

public class HouseGUI : MonoBehaviour {

	
	public bool attacked = false; 

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void EnableGUI() {
		this.GetComponent<Renderer>().enabled = true;
	}

	public void DisableGUI() {
		this.GetComponent<Renderer>().enabled = false;
	}

	public void FadeGUI() {
		attacked = true;
		this.GetComponent<Renderer>().material.color = new Vector4(this.GetComponent<Renderer>().material.color.r, 
			this.GetComponent<Renderer>().material.color.g, this.GetComponent<Renderer>().material.color.b, 0.5f);
	}
}
