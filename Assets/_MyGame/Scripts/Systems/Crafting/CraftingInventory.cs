using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.InventoryEngine;
using MoreMountains.Tools;
using MoreMountains.TopDownEngine;

public class CraftingInventory : MonoBehaviour, MMEventListener<MMInventoryEvent>
{
    public int gridWidth;
    public int gridHeight;
    
    public Inventory grid;
    public OutputInventory output;
    public List<RecipeSO> recipes;
    

    protected virtual bool TryGetOutput()
    {
        foreach (RecipeSO recipe in recipes)
        {
            if (CheckRecipe(recipe))
            {
                output.OutputItem(recipe.resultItem, 1);
                return true;
            }
        }
        if (output.Content[0] != null)
        {
            output.RemoveItem(0, 1);
        }
        return false;
    }

    protected virtual bool CheckRecipe(RecipeSO recipe)
    {
        for (int x = 0; x < recipe.craftGrid.GetLength(0); x++)
        {
            for (int y = 0; y < recipe.craftGrid.GetLength(1); y++)
            {
                if (recipe.GetItem(x, y) != null)
                {
                    if (grid.GetItem(x,y, gridWidth) == null)
                    {
                        return false;
                    }
                    if (grid.GetItem(x, y, gridWidth).ItemID != recipe.GetItem(x, y).ItemID)
                    {
                        return false;
                    }
                }
                if (recipe.GetItem(x, y) == null)
                {
                    if (grid.GetItem(x, y, gridWidth) != null)
                    {
                        return false;
                    }
                }
            }
        }
        return true;
    }

    public void Craft()
    {
        for (int i=0; i<grid.Content.Length; i++)
        {
            if (grid.Content[i] != null)
            {
                grid.RemoveItem(i, 1);
            }
        }
    }

    public void OnMMEvent(MMInventoryEvent inventoryEvent)
    {
        if (inventoryEvent.TargetInventoryName == grid.name)
        {
            if (inventoryEvent.InventoryEventType == MMInventoryEventType.ContentChanged)
            {
                bool test = TryGetOutput();
                Debug.Log("Content Changed");
                Debug.Log(test);
            }
        }
        if (inventoryEvent.TargetInventoryName == output.name)
        {
            if (inventoryEvent.InventoryEventType == MMInventoryEventType.UseRequest)
            {
                Craft();
            }
        }
    }

    private void OnEnable()
    {
        this.MMEventStartListening<MMInventoryEvent>();
    }

    private void OnDisable()
    {
        this.MMEventStopListening<MMInventoryEvent>();
    }
}

public static class InventoryExtensions
{
    public static InventoryItem GetItem(this Inventory inventory, int x, int y, int width)
    {
        return inventory.Content[x + y * width];
    }
}
