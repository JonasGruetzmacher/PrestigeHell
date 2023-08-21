using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(menuName = "Upgrade")]
public abstract class Upgrade : SerializedScriptableObject
{
    public string upgradeName;
    public string description { get; private set; }
    public List<ResourceAmount> cost = new List<ResourceAmount>();
    public int upgradeLimit = 1;
    public int currentUpgradeCount { get; protected set; }

    [Button]
    public abstract void DoUpgrade();

    public abstract void ResetUpgrade();

}
