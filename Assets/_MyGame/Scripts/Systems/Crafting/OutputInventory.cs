using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.InventoryEngine;

public class OutputInventory : Inventory
{
    public override bool AddItem(InventoryItem itemToAdd, int quantity)
    {
        return false;
    }
    public override bool AddItemAt(InventoryItem itemToAdd, int quantity, int slotID)
    {
        return false;
    }

    public virtual bool OutputItem(InventoryItem itemToOutput, int quantity)
    {
    // if the item to add is null, we do nothing and exit
        if (itemToOutput == null)
        {
            Debug.LogWarning(this.name + " : The item you want to add to the inventory is null");
            return false;
        }

        List<int> list = InventoryContains(itemToOutput.ItemID);
        // if there's at least one item like this already in the inventory and it's stackable
        // if (list.Count > 0 && itemToOutput.MaximumStack > 1)
        // {
        //     // we store items that match the one we want to add
        //     for (int i = 0; i < list.Count; i++)
        //     {
        //         // if there's still room in one of these items of this kind in the inventory, we add to it
        //         if (Content[list[i]].Quantity < itemToOutput.MaximumStack)
        //         {
        //             // we increase the quantity of our item
        //             Content[list[i]].Quantity += quantity;
        //             // if this exceeds the maximum stack
        //             if (Content[list[i]].Quantity > Content[list[i]].MaximumStack)
        //             {
        //                 InventoryItem restToAdd = itemToOutput;
        //                 int restToAddQuantity = Content[list[i]].Quantity - Content[list[i]].MaximumStack;
        //                 // we clamp the quantity and add the rest as a new item
        //                 Content[list[i]].Quantity = Content[list[i]].MaximumStack;
        //                 AddItem(restToAdd, restToAddQuantity);
        //             }
        //             MMInventoryEvent.Trigger(MMInventoryEventType.ContentChanged, null, this.name, null, 0, 0, PlayerID);
        //             return true;
        //         }
        //     }
        // }
        // if we've reached the max size of our inventory, we don't add the item
        if (NumberOfFilledSlots >= Content.Length)
        {
            return false;
        }
        while (quantity > 0)
        {
            if (quantity > itemToOutput.MaximumStack)
            {
                AddItem(itemToOutput, itemToOutput.MaximumStack);
                quantity -= itemToOutput.MaximumStack;
            }
            else
            {
                AddItemToArray(itemToOutput, quantity);
                quantity = 0;
            }
        }
        // if we're still here, we add the item in the first available slot
        MMInventoryEvent.Trigger(MMInventoryEventType.ContentChanged, null, this.name, null, 0, 0, PlayerID);
        return true;
    }

    public override bool MoveItemToInventory(int sourceIndex, Inventory targetInventory, int targetIndex)
    {
        if (targetInventory == this)
        {
            return false;
        }
        MMInventoryEvent.Trigger(MMInventoryEventType.UseRequest, null, this.name, null, 0, 0, PlayerID);
        return base.MoveItemToInventory(sourceIndex, targetInventory, targetIndex);

    }
}
