using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.TopDownEngine;

public class XPDrop : PickableItem
{
    public int XPAmount = 1;

    protected override void Pick(GameObject picker)
    {
        // we send a new points event for the GameManager to catch (and other classes that may listen to it too)
        ResourceEvent.Trigger(ResourceMethods.Add, new ResourceAmount(ResourceType.XP, XPAmount));
    }
}

