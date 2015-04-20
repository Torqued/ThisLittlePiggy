using UnityEngine;
using System.Collections;

public class BuildStrawHouse : _PlayerButtonAction {
    public override void buttonClicked() {
        CraftingRecipes.craft(Crafting.HouseStraw, playerInventory);
    }
}
