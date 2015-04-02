using UnityEngine;
using System.Collections;

public class Inventory : MonoBehaviour {

	// Resources
	public int brick, grass, straw, stick;
	public int rope;
	void Start () {
		brick = 0;
		grass = 0;
		straw = 0;
		stick = 0;
		rope = 0;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter(Collision other)
	{
		GameObject g = other.collider.gameObject;
		if (g.tag == "Item") {
			if(g.GetComponent<Item>().itemType == "Brick")
			{
				brick ++;
				Destroy(g);
			}
			else if(g.GetComponent<Item>().itemType == "Grass")
			{
				grass++;
				Destroy(g);
			}
			else if(g.GetComponent<Item>().itemType == "Straw")
			{
				straw++;
				Destroy(g);
			}
			else if(g.GetComponent<Item>().itemType == "Stick")
			{
				stick++;
				Destroy(g);
			}

			
		}
	}
}
