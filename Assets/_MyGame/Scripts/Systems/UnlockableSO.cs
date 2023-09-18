using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public abstract class UnlockableSO : SerializedScriptableObject
{
    [SerializeField] public bool unlocked { get; protected set; }
    public abstract void Unlock();

    public virtual void Reset()
    {
        unlocked = false;
    }

}
