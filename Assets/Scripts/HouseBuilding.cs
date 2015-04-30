using UnityEngine;
using System.Collections;

public class HouseBuilding : MonoBehaviour {
	public enum HouseType {
		Straw,
		Sticks,
		Bricks
	}
	public float t;
	public bool builtHouse;
	public HouseType h;
	// Use this for initialization
	void Start () {
		builtHouse = false;
		t = Time.time;
		Object.Instantiate((Resources.Load("Effects/ConstructionEffect", typeof(GameObject)) as GameObject), (transform.position - new Vector3(0,5,5)), Quaternion.identity);
	}
	
	// Update is called once per frame
	void Update () {
		if (!builtHouse && (Time.time - t) > 15f) {
			if (h == HouseType.Sticks)
				Object.Instantiate ((Resources.Load ("Houses/LogHouse", typeof(GameObject)) as GameObject), transform.position, Quaternion.identity);
			if (h == HouseType.Straw)
				Object.Instantiate ((Resources.Load ("Houses/StrawHousePrefab", typeof(GameObject)) as GameObject), transform.position, Quaternion.identity);
			if (h == HouseType.Bricks)
				Object.Instantiate ((Resources.Load ("Houses/BrickHouse", typeof(GameObject)) as GameObject), transform.position, Quaternion.identity);
			builtHouse = true;
		}
		if (!builtHouse) {
			if(!GetComponent<AudioSource> ().isPlaying)
				GetComponent<AudioSource> ().Play();
		}
		else{
			if(GetComponent<AudioSource> ().isPlaying)
				GetComponent<AudioSource> ().Stop();
		}
	}
}
