using UnityEngine;
using System.Collections;

public class CraftingGui : MonoBehaviour {

	// Use this for initialization
	public GUISkin gs;
	private bool display;
	public Texture blank;
	private CharacterInventory inventory;


	void Start () {
		display = false;
		inventory = GameObject.FindGameObjectWithTag ("Player").GetComponent<CharacterInventory> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown ("c")) {
						display = !display;
					}
	}

	void OnGUI () {
		// Make a background box
		if (display) {
			GUI.BeginGroup(new Rect(20, Screen.height / 2 - 150, 250, 300));
			GUI.Box(new Rect(0, 0, 250, 300), "Construction");
		
			// Make the first button. If it is pressed, Application.Loadlevel (1) will be executed
//			if (GUI.Button(new Rect(10, 50, 150, 40), "Rope")){
//				CraftingRecipes.craft (Crafting.Rope, inventory);
//			}
			if (GUI.Button(new Rect(10, 100, 150, 40), "Straw House")) {
				CraftingRecipes.craft (Crafting.HouseStraw, inventory);
			}
//			if (GUI.Button(new Rect(170, 100, 70, 40), "Use Fairy")) {
//				CraftingRecipes.craftWithFairy (Crafting.HouseStraw, inventory);
//			}
			if (GUI.Button(new Rect(10, 150, 150, 40), "Stick House")) {
				CraftingRecipes.craft (Crafting.HouseSticks, inventory);
			}
//			if (GUI.Button(new Rect(170, 150, 70, 40), "Use Fairy")) {
//				CraftingRecipes.craftWithFairy (Crafting.HouseSticks, inventory);
//			}
			if (GUI.Button(new Rect(10, 200, 150, 40), "Brick House")) {
				CraftingRecipes.craft (Crafting.HouseBricks, inventory);
			}
//			if (GUI.Button(new Rect(170, 200, 70, 40), "Use Fairy")) {
//				CraftingRecipes.craftWithFairy (Crafting.HouseBricks, inventory);
//			}
			GUI.EndGroup();
		}
	}


}
