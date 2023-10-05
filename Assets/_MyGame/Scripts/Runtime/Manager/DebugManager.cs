using System.Collections;
using System.Collections.Generic;
using MoreMountains.Tools;
using UnityEngine;
using Sirenix.OdinInspector;

namespace LeroGames.PrestigeHell
{
    public class DebugManager : MMSingleton<DebugManager>
    {
        public ResourceAmount resourceAmount;


        [Button]
        public void AddResource()
        {
            ResourcesManager.Instance.AddResource(resourceAmount);
        }
    }
}