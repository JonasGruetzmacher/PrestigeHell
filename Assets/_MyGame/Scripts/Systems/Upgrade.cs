using System.Collections;
using System.Collections.Generic;
using MoreMountains.Tools;
using Sirenix.OdinInspector;
using UnityEngine;

public abstract class Upgrade : SerializedScriptableObject
{
    public string upgradeName;
    public string description;
    public Dictionary<ResourceType, float> upgradeCost = new Dictionary<ResourceType, float>();
    // public MMSerializableDictionary<ResourceType, float> upgradeCost = new MMSerializableDictionary<ResourceType, float>();
    // public MMSerializableDictionary<ResourceType, AnimationCurve> upgradeCostCurve = new MMSerializableDictionary<ResourceType, AnimationCurve>();
    public int upgradeLimit = 1;
    public int currentUpgradeCount { get; set; }
    public Dictionary<ResourceType, float> GetNextUpgradeCost()
    {
        // List<ResourceAmount> nextUpgradeCost = new List<ResourceAmount>();
        // foreach (var resource in upgradeCostCurve)
        // {
        //     nextUpgradeCost.Add(new ResourceAmount(resource.Key, (int)resource.Value.Evaluate(currentUpgradeCount)));
        // }
        // return nextUpgradeCost;
        return upgradeCost;
    }


    [Button]
    public abstract void DoUpgrade();

    public abstract void ResetUpgrade();

}
