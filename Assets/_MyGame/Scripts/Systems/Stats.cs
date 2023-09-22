using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using System;
using System.Linq;
using System.Resources;

[CreateAssetMenu(menuName = "Character/StatsSO")]
public class Stats : SerializedScriptableObject, ITooltipInformation
{
    public Dictionary<Stat, float> instanceStats = new Dictionary<Stat, float>();
    public Dictionary<Stat, float> stats = new Dictionary<Stat, float>();

    [DictionaryDrawerSettings(DisplayMode = DictionaryDisplayOptions.OneLine)]
    public Dictionary<Stat, ScalingStat> statCurves = new Dictionary<Stat, ScalingStat>();
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

    public void RemoveUpgrade(StatsUpgrade upgrade)
    {
        appliedUpgrades.Remove(upgrade);
    }

    public float GetUpgradedValue(Stat stat, float baseValue)
    {
        baseValue = AddScaling(baseValue, stat);
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

    private float AddScaling(float baseValue, Stat stat)
    {
        if(statCurves == null)
            return baseValue;
        if(!statCurves.TryGetValue(stat, out var curve))
            return baseValue;
        return baseValue + curve.curve.Evaluate(ResourcesManager.Instance.GetResourceAmount(ResourceType.Danger));
    }

    public void GetTooltipInformation(out string infoLeft, out string infoRight)
    {
        infoLeft = "";
        infoRight = "";
        foreach (var stat in stats)
        {
            infoLeft += string.Format("!{0}`: {1}\n", stat.Key, GetStat(stat.Key).ToString("0.00"));
        }
    }

    [Button]
    public void ResetAppliedUpgrades()
    {
        appliedUpgrades.Clear();
    }
}
