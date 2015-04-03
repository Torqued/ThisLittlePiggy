using UnityEngine;
using System.Collections;
using System.Collections.Generic;

#region Crafting

public enum Crafting {
    HouseBricks,
    HouseSticks,
    HouseStraw,
    HouseWood,
    Rope
}

public static class CraftingRecipes {

    private static Dictionary<Crafting, ICraftingRecipe> crafting;

    static CraftingRecipes() {
        crafting = new Dictionary<Crafting, ICraftingRecipe>();

        crafting[Crafting.HouseBricks] = new RecipeHouseBricks();
        crafting[Crafting.HouseSticks] = new RecipeHouseSticks();
        crafting[Crafting.HouseStraw] = new RecipeHouseStraw();
        crafting[Crafting.HouseWood] = new RecipeHouseWood();
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
    }

    #region Recipe Definitions
    private class RecipeHouseBricks : ICraftingRecipe {
        public bool canCraft(CharacterInventory i) {
            return true;
        }
        
        public void craftingResult(CharacterInventory i) {
            
        }
    }
    
    private class RecipeHouseSticks : ICraftingRecipe {
        public bool canCraft(CharacterInventory i) {
            return true;
        }
        
        public void craftingResult(CharacterInventory i) {
            
        }
    }

    private class RecipeHouseStraw : ICraftingRecipe {
        public bool canCraft(CharacterInventory i) {
            return i.getAmount(ItemType.Straw) >= 5 && i.getAmount(ItemType.Rope) >= 2;
        }
        
        public void craftingResult(CharacterInventory i) {
            i.removeItem(ItemType.Straw, 5);
            i.removeItem(ItemType.Rope, 2);

            GameObject player = i.gameObject;
            Vector3 position = 3*Vector3.Normalize(new Vector3(player.transform.forward.x, 0, player.transform.forward.z)) +
                player.transform.position;
            Object.Instantiate(Resources.Load("SpawnPrefabs/StrawHouse"), position, Quaternion.identity);
        }
    }

    private class RecipeHouseWood : ICraftingRecipe {
        public bool canCraft(CharacterInventory i) {
            return true;
        }
        
        public void craftingResult(CharacterInventory i) {
            
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

    void Start() {
        inventory = new Dictionary<ItemType, int>();
        foreach (ItemType i in System.Enum.GetValues(typeof(ItemType))) {
            inventory[i] = 0;
        }
    }

    public void addItem(ItemType item) {
        inventory[item]++;
    }

    public void removeItem(ItemType item, int amount) {
        inventory[item] -= amount;
    }

    public int getAmount(ItemType item) {
        return inventory[item];
    }
}