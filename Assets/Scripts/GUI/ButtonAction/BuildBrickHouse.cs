using UnityEngine;
using System.Collections;

public class BuildBrickHouse : _PlayerButtonAction {
    public override void buttonClicked() {
        CraftingRecipes.craft(Crafting.HouseBricks, playerInventory);
    }
}