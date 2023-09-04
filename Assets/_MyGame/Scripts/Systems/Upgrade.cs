using System.Collections;
using System.Collections.Generic;
using MoreMountains.Tools;
using Sirenix.OdinInspector;
using UnityEngine;
using System;

public abstract class Upgrade : SerializedScriptableObject
{
    public string upgradeName;
    public string description;
    public Dictionary<ResourceType, float> upgradeCost = new Dictionary<ResourceType, float>();
    public List<Upgrade> blockedUpgrades = new List<Upgrade>();
    public bool isUnlocked = false;
    // public MMSerializableDictionary<ResourceType, float> upgradeCost = new MMSerializableDictionary<ResourceType, float>();
    // public MMSerializableDictionary<ResourceType, AnimationCurve> upgradeCostCurve = new MMSerializableDictionary<ResourceType, AnimationCurve>();
    public int upgradeLimit = 1;
    public event Action<Upgrade, bool> upgradeStateChanged;
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

    public virtual void Unlock(bool unlock = true)
    {
        Debug.Log("Unlocking " + upgradeName);
        isUnlocked = unlock;
        upgradeStateChanged?.Invoke(this, unlock);
    }

    public virtual String GetNextUpgradeCosts()
    {
        String costs = "";
        foreach (var resource in upgradeCost)
        {
            costs += resource.Value.ToString() + " " + resource.Key.ToString() + "\n";
        }
        return costs;
    }

}
