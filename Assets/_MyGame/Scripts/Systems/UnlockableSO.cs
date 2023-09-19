using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.UIElements;

public abstract class UnlockableSO : SerializedScriptableObject
{
    [SerializeField] public bool unlocked { get; protected set; }
    public abstract void Unlock();
    public abstract string GetTextDescription();

    public virtual void Reset()
    {
        unlocked = false;
    }

}
