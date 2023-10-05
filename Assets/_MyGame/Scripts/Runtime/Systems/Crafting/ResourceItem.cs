using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.InventoryEngine;

namespace LeroGames.PrestigeHell
{
    [CreateAssetMenu(fileName = "ResourceItem", menuName = "Crafting/ResourceItem")]
    public class ResourceItem : InventoryItem
    {
        public ResourceAmount resourceAmount;

        public override bool Use(string playerID)
        {
            ResourcesManager.Instance.AddResource(resourceAmount);
            return true;
        }
    }
}