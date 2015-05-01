using UnityEngine;
using System.Collections;

public class HouseGUI : MonoBehaviour {

	
	public bool attacked = false; 

	public GameObject emptyBar;
	public GameObject fullBar;
	public GameObject text;

	private float healthPercentage = 1.0f;

	private AudioSource snoring; 
	// Use this for initialization
	void Awake() {
		snoring = this.GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void EnableGUI() {
		this.GetComponent<Renderer>().enabled = true;
		emptyBar.GetComponent<Renderer>().enabled = true;
		fullBar.GetComponent<Renderer>().enabled = true;
		text.GetComponent<Renderer>().enabled = true;
		snoring.Play();
	}

	public void DisableGUI() {
		this.GetComponent<Renderer>().enabled = false;
		emptyBar.GetComponent<Renderer>().enabled = false;
		fullBar.GetComponent<Renderer>().enabled = false;
		text.GetComponent<Renderer>().enabled = false;
		snoring.Stop();
	}

	public void FadeGUI() {
		attacked = true;
		this.GetComponent<Renderer>().material.color = new Vector4(this.GetComponent<Renderer>().material.color.r, 
			this.GetComponent<Renderer>().material.color.g, this.GetComponent<Renderer>().material.color.b, 0.5f);
		snoring.Stop();
	}

	public void updateHealth(int currentHealth, int maxHealth) {
		healthPercentage = (currentHealth * 1.0f) / (maxHealth * 1.0f);
		// scale the full bar down based on health percentage
		// then translate it to the left based on how much it was scaled
		Vector3 newScale = new Vector3( healthPercentage, 1f , 1f );
		fullBar.transform.localScale = newScale;
		Vector3 newPosition = fullBar.transform.localPosition;
		newPosition.x = (1.0f - healthPercentage) * -5.0f;
 		fullBar.transform.localPosition = newPosition;

		fullBar.GetComponent<Renderer>().enabled = false;
		fullBar.GetComponent<Renderer>().enabled = true;
	}
}
