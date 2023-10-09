using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.UIElements;
using LeroGames.Tools;

namespace LeroGames.PrestigeHell
{
    public abstract class UnlockableSO : SerializedScriptableObject, ITooltipInformation
    {
        [SerializeField] public bool unlocked { get; protected set; }
        public string unlockID;
        public string shortDescription;
        public string longDescription;
        public abstract void Unlock();
        public virtual string GetTextDescription()
        {
            return shortDescription;
        }
        
        public virtual void GetTooltipInformation(out string infoLeft, out string infoRight)
        {
            infoRight = null;
            infoLeft = longDescription;
        }

        public virtual void Reset()
        {
            unlocked = false;
        }

    }
}