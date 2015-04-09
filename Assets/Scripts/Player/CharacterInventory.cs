using UnityEngine;
using System.Collections;
using System.Collections.Generic;

#region Crafting

public enum Crafting {
    HouseBricks,
    HouseSticks,
    HouseStraw,
    //HouseWood,
    Rope
}

public static class CraftingRecipes {

    private static Dictionary<Crafting, ICraftingRecipe> crafting;

    static CraftingRecipes() {
        crafting = new Dictionary<Crafting, ICraftingRecipe>();

        crafting[Crafting.HouseBricks] = new RecipeHouseBricks();
        crafting[Crafting.HouseSticks] = new RecipeHouseSticks();
        crafting[Crafting.HouseStraw] = new RecipeHouseStraw();
        //crafting[Crafting.HouseWood] = new RecipeHouseWood();
        crafting[Crafting.Rope] = new RecipeRope();
    }

    public static bool canCraft(Crafting craft, CharacterInventory i) {
        return crafting[craft].canCraft(i);
    }

    public static bool craft(Crafting craft, CharacterInventory i) {
        if (canCraft(craft, i)) {
            crafting[craft].craftingResult(i);
            return true;
        }
        return false;
    }
    
    private interface ICraftingRecipe {
        bool canCraft(CharacterInventory i);
        void craftingResult(CharacterInventory i);
		//void craftingWithFairyResult (CharacterInventory i);
    }

    #region Recipe Definitions
    private class RecipeHouseBricks : ICraftingRecipe {
        public bool canCraft(CharacterInventory i) {
			if (i.getAmount (ItemType.Bricks) >= 10 && i.getAmount(ItemType.Sticks) >= 4) {
				return true;
			}
			return false;
        }
        
        public void craftingResult(CharacterInventory i) {
            
        }

		public void craftingWithFairyResult (CharacterInventory i){
		
		}
    }
    
    private class RecipeHouseSticks : ICraftingRecipe {
        public bool canCraft(CharacterInventory i) {
			if (i.getAmount (ItemType.Rope) >= 4 && i.getAmount(ItemType.Sticks) >= 8) {
				return true;
			}
			return false;
        }
        
        public void craftingResult(CharacterInventory i) {
            
        }

		public void craftingWithFairyResult (CharacterInventory i){
			
		}
    }

    private class RecipeHouseStraw : ICraftingRecipe {
        public bool canCraft(CharacterInventory i) {
			if (i.getAmount (ItemType.Straw) >= 5 && i.getAmount(ItemType.Rope) >= 2) {
				return true;
			}
			return false;
        }
        
        public void craftingResult(CharacterInventory i) {
            i.removeItem(ItemType.Straw, 5);
            i.removeItem(ItemType.Rope, 2);

			placeHouse (i);
        }

		public void craftingWithFairyResult (CharacterInventory i){
			i.removeItem(ItemType.Straw, 3);
			i.removeItem(ItemType.Rope, 1);
			i.removeItem(ItemType.Fairy, 1);
			
			placeHouse (i);
		}

		private void placeHouse(CharacterInventory i){
			GameObject player = i.gameObject;
			Vector3 position = 3*Vector3.Normalize(new Vector3(player.transform.forward.x, 0, player.transform.forward.z)) +
				player.transform.position;
			Object.Instantiate(Resources.Load("SpawnPrefabs/StrawHouse"), position, Quaternion.identity);
		}
    }

    private class RecipeRope : ICraftingRecipe {
        public bool canCraft(CharacterInventory i) {
            return i.getAmount(ItemType.Grass) >= 2;
        }
        
        public void craftingResult(CharacterInventory i) {
            i.removeItem(ItemType.Grass, 2);
            i.addItem(ItemType.Rope);
        }


    }
    #endregion
}
#endregion

public class CharacterInventory : MonoBehaviour {
    private Dictionary<ItemType, int> inventory;
    private Dictionary<ItemType, TextMesh> display;

    void Start() {
        inventory = new Dictionary<ItemType, int>();
        foreach (ItemType i in System.Enum.GetValues(typeof(ItemType))) {
            inventory[i] = 0;
        }

        display = new Dictionary<ItemType, TextMesh>();

        Transform child = transform.Find("GUI Camera");
        child = child.Find("FromTop");

        display[ItemType.Grass] = child.Find("Grass").gameObject.GetComponent<TextMesh>();
        display[ItemType.Straw] = child.Find("Straw").gameObject.GetComponent<TextMesh>();
        display[ItemType.Sticks] = child.Find("Sticks").gameObject.GetComponent<TextMesh>();
        display[ItemType.Bricks] = child.Find("Bricks").gameObject.GetComponent<TextMesh>();
        display[ItemType.Rope] = child.Find("Rope").gameObject.GetComponent<TextMesh>();
    }

    public void addItem(ItemType item) {
        addItem(item, 1);
    }

    public void addItem(ItemType item, int amount) {
        inventory[item] += amount;
        display[item].text = "" + inventory[item];
    }

    public void removeItem(ItemType item, int amount) {
        inventory[item] -= amount;
        display[item].text = "" + inventory[item];
    }

    public int getAmount(ItemType item) {
        return inventory[item];
    }
}