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

    private static Dictionary<Crafting, CraftingRecipe> crafting;

    static CraftingRecipes() {
        crafting = new Dictionary<Crafting, CraftingRecipe>();

        crafting[Crafting.HouseBricks] = new RecipeHouseBricks();
        crafting[Crafting.HouseSticks] = new RecipeHouseSticks();
        crafting[Crafting.HouseStraw] = new RecipeHouseStraw();
        crafting[Crafting.HouseWood] = new RecipeHouseWood();
        crafting[Crafting.Rope] = new RecipeRope();
    }

    public static bool canCraft(Crafting craft, Inventory i) {
        return crafting[craft].canCraft(i);
    }

    public static bool craft(Crafting craft, Inventory i) {
        if (canCraft(craft, i)) {
            crafting[craft].craftingResult(i);
            return true;
        }
        return false;
    }
    
    private abstract class CraftingRecipe {
        public abstract bool canCraft(Inventory i);

        public abstract void craftingResult(Inventory i);
    }

    #region Recipe Definitions
    private class RecipeHouseBricks : CraftingRecipe {
        override public bool canCraft(Inventory i) {
            return true;
        }
        
        override public void craftingResult(Inventory i) {
            
        }
    }
    
    private class RecipeHouseSticks : CraftingRecipe {
        override public bool canCraft(Inventory i) {
            return true;
        }
        
        override public void craftingResult(Inventory i) {
            
        }
    }

    private class RecipeHouseStraw : CraftingRecipe {
        override public bool canCraft(Inventory i) {
            return true;
        }
        
        override public void craftingResult(Inventory i) {
            
        }
    }

    private class RecipeHouseWood : CraftingRecipe {
        override public bool canCraft(Inventory i) {
            return true;
        }
        
        override public void craftingResult(Inventory i) {
            
        }
    }
    
    private class RecipeRope : CraftingRecipe {
        override public bool canCraft(Inventory i) {
            return true;
        }
        
        override public void craftingResult(Inventory i) {
            
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