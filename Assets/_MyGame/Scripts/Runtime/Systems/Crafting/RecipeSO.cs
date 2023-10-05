using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using MoreMountains.InventoryEngine;

[CreateAssetMenu(fileName = "Recipe", menuName = "Crafting/RecipeSO")]
public class RecipeSO : SerializedScriptableObject
{
    public string recipeID;
    public string recipeName;
    public string shortDescription;
    public string longDescription;

    public InventoryItem resultItem;
    public int resultQuantity;

    [TableMatrix(HorizontalTitle = "Crafting Grid", VerticalTitle = "")]
    public InventoryItem[,] craftGrid;

    public InventoryItem GetItem(int x, int y)
    {
        return craftGrid[x, y];
    }
}
