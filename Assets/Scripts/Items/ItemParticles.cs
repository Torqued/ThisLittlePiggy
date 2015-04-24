using UnityEngine;
using System.Collections;

public class ItemParticles : MonoBehaviour {

	public AudioClip Pickup;

	public void OnPickup(){
		AudioSource.PlayClipAtPoint (Pickup, transform.position);
		Object.Instantiate((Resources.Load("Effects/Poof", typeof(GameObject)) as GameObject), transform.position, Quaternion.identity);
	}
}
