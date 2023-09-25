using System.Collections;
using System.Collections.Generic;
using MoreMountains.Tools;
using Sirenix.OdinInspector;
using UnityEngine;
using System;

public abstract class Upgrade : SerializedScriptableObject, ITooltipInformation
{
    public string upgradeName;
    public string description;
    public Dictionary<ResourceType, float> upgradeCost = new Dictionary<ResourceType, float>();
    public List<Upgrade> blockedUpgrades = new List<Upgrade>();
    public bool isUnlocked = false;
    public bool isBlocked = false;
    // public MMSerializableDictionary<ResourceType, float> upgradeCost = new MMSerializableDictionary<ResourceType, float>();
    // public MMSerializableDictionary<ResourceType, AnimationCurve> upgradeCostCurve = new MMSerializableDictionary<ResourceType, AnimationCurve>();
    public int upgradeLimit = 1;
    public event Action<Upgrade> upgradeStateChanged;
    public event Action<Upgrade> upgradeCompleted;
    public event Action<Upgrade> upgradeApplied;
    public event Action<Upgrade> upgradeReset;
    public int currentUpgradeCount = 0;
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

    public virtual void Unlock(bool unlock = true)
    {
        isUnlocked = unlock;
        upgradeStateChanged?.Invoke(this);
    }

    public virtual void BlockUpgrade()
    {
        isBlocked = true;
        upgradeStateChanged?.Invoke(this);
    }

    public virtual void ApplyUpgrade()
    {
        currentUpgradeCount++;
        upgradeApplied?.Invoke(this);
        if (currentUpgradeCount >= upgradeLimit)
        {
            Completed();
        }
    }

    public virtual void ResetUpgrade()
    {
        currentUpgradeCount = 0;
        isUnlocked = false;
        isBlocked = false;
        upgradeReset?.Invoke(this);
    }

    public virtual void Completed()
    {
        upgradeCompleted?.Invoke(this);
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

    public virtual void GetTooltipInformation(out string infoLeft, out string infoRight)
    {
        infoLeft = "";
        infoRight = "";

        infoLeft += string.Format("!{0}`\n", upgradeName);
        infoLeft += string.Format("{0}`\n", description);

        if (GetNextUpgradeCost().Count > 0)
        {
            infoRight += string.Format("{0}`\n", GetNextUpgradeCosts());
        }
    }

}
