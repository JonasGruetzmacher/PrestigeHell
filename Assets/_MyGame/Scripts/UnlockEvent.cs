using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UnlockEvent", menuName = "Unlockables/UnlockEvent")]
public class UnlockEvent : UnlockableSO
{
    public CustomEventSystem.GameEvent unlockEvent;
    public override void Unlock()
    {
        unlockEvent.Raise(null, GetTextDescription());
    }

    public override string GetTextDescription()
    {
        return "Unlock new event: " + unlockEvent.name;
    }
}
