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
        bool v = base.MoveItemToInventory(sourceIndex, targetInventory, targetIndex);
        MMInventoryEvent.Trigger(MMInventoryEventType.UseRequest, null, this.name, null, 0, 0, PlayerID);
        return v;

    }
}
