using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.TopDownEngine;
using LeroGames.Tools;

namespace LeroGames.PrestigeHell
{
    public class ResourceLootDrop : PickableItem
    {
        public ResourceAmount resourceAmount;
        [SerializeField] private LeroGames.Tools.GameEvent resourceLootEvent;

        protected override void Pick(GameObject picker)
        {
            resourceLootEvent?.Raise(this, resourceAmount);
        }
    }
}