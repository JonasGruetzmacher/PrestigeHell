using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.TopDownEngine;

public class ResourceLootDrop : PickableItem
{
    public ResourceAmount resourceAmount;
    [SerializeField] private CustomEventSystem.GameEvent resourceLootEvent;

    protected override void Pick(GameObject picker)
    {
        resourceLootEvent?.Raise(this, resourceAmount);
    }
}
