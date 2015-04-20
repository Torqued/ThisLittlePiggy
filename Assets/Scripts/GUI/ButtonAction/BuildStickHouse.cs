using UnityEngine;
using System.Collections;

public class BuildStickHouse : _PlayerButtonAction {
    public override void buttonClicked() {
        CraftingRecipes.craft(Crafting.HouseSticks, playerInventory);
    }
}
