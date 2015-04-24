using UnityEngine;
using System.Collections;

public class ItemParticles : MonoBehaviour {

	public AudioClip Pickup;

	void OnDestroy(){
		AudioSource.PlayClipAtPoint (Pickup, transform.position);
		Object.Instantiate((Resources.Load("Effects/CFX_SmokePuffs", typeof(GameObject)) as GameObject), transform.position, Quaternion.identity);
	}
}
