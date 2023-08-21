using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using System;
using System.Linq;

[CreateAssetMenu(menuName = "Stats")]
public class Stats : SerializedScriptableObject
{
    public Dictionary<Stat, float> instanceStats = new Dictionary<Stat, float>();
    public Dictionary<Stat, float> stats = new Dictionary<Stat, float>();
    private List<StatsUpgrade> appliedUpgrades = new List<StatsUpgrade>();

    public event Action<Stats, StatsUpgrade> upgradeApplied;

    public bool IncludesStat(Stat stat)
    {
        if (instanceStats.ContainsKey(stat))
            return true;
        else if (stats.ContainsKey(stat))
            return true;
        else
        {
            return false;
        }
    }
    public float GetStat(Stat stat)
    {
        if(instanceStats.TryGetValue(stat, out var instanceValue))
            return GetUpgradedValue(stat, instanceValue);
        else if (stats.TryGetValue(stat, out var value))
            return GetUpgradedValue(stat, value);
        else
        {
            Debug.LogError($"Stat {stat} not found in {this.name}");
            return 0;
        }
    }

    public int GetStatAsInt(Stat stat)
    {
        return (int)GetStat(stat);
    }

    public void UnlockUpgrade(StatsUpgrade upgrade)
    {
        appliedUpgrades.Add(upgrade);
        upgradeApplied?.Invoke(this, upgrade);
    }

    public float GetUpgradedValue(Stat stat, float baseValue)
    {
        foreach (var upgrade in appliedUpgrades)
        {
            if(!upgrade.upgradeToApply.TryGetValue(stat, out var upgradeValue))
                continue;
            if(upgrade.isPercentageUpgrade)
                baseValue *= (upgradeValue / 100f) + 1f;
            else
                baseValue += upgradeValue;
        }
        return baseValue;
    }

    [Button]
    public void ResetAppliedUpgrades()
    {
        appliedUpgrades.Clear();
    }
}
