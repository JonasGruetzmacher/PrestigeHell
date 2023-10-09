using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using System;
using LeroGames.Tools;
using System.Resources;
using Unity.VisualScripting.YamlDotNet.Core.Tokens;

namespace LeroGames.PrestigeHell
{            
    [CreateAssetMenu(menuName = "Character/StatsSO")]
    public class Stats : SerializedScriptableObject, ITooltipInformation
    {
        public Dictionary<Stat, float> instanceStats = new Dictionary<Stat, float>();
        public Dictionary<Stat, float> stats = new Dictionary<Stat, float>();

        [DictionaryDrawerSettings(DisplayMode = DictionaryDisplayOptions.OneLine)]
        public Dictionary<Stat, ScalingStat> statCurves = new Dictionary<Stat, ScalingStat>()
        {
            { Stat.health, new ScalingStat(new float[] {0,20}, new float[] {0,100}) }
        };
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
                Debug.Log($"Stat {stat} not found in {this.name}");
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
                if (stat.Key.ToMarkedString() == null)
                    continue;

                if (stat.Key == Stat.groupSize && IncludesStat(Stat.groupSizeVariance))
                {
                    infoLeft += string.Format("{0}: {1} - {2}\n", stat.Key.ToMarkedString(), GetStat(stat.Key) - GetStat(Stat.groupSizeVariance), GetStat(stat.Key) + GetStat(Stat.groupSizeVariance));
                    continue;
                }
                infoLeft += string.Format("{0}: {1}\n", stat.Key.ToMarkedString(), GetStat(stat.Key).ToString("F2"));
            }

            foreach (var instanceStat in instanceStats)
            {
                if (instanceStat.Key.ToMarkedString() == null)
                    continue;
                if (instanceStat.Key == Stat.health && IncludesStat(Stat.healthVariance))
                {
                    float minHealth = GetStat(instanceStat.Key) * (1f - GetStat(Stat.healthVariance));
                    float maxHealth = GetStat(instanceStat.Key) * (1f + GetStat(Stat.healthVariance));
                    infoLeft += string.Format("{0}: {1} - {2}\n", instanceStat.Key.ToMarkedString(), minHealth.ToString("F2"), maxHealth.ToString("F2"));
                    continue;
                }
                infoLeft += string.Format("{0}: {1}\n", instanceStat.Key.ToMarkedString(), GetStat(instanceStat.Key).ToString("F2"));
            }
        }

        [Button]
        public void ResetAppliedUpgrades()
        {
            appliedUpgrades.Clear();
        }

        [Button]
        public void AddScalingStat(Stat stat, (float, float)[] keyframes)
        {
            if (statCurves.ContainsKey(stat))
            {
                statCurves.Remove(stat);
            }
            statCurves.Add(stat, new ScalingStat(keyframes));
        }
    }
    public enum Stat
    {
        health = 0,
        speed = 1,
        touchDamage = 2,
        attackSpeed = 3,
        damageReduction = 4,
        collectRange = 5,
        XPGain = 6,
        healthVariance = 7,
        groupSize = 8,
        groupSizeVariance = 9,
        groupSpawnRadius = 10,
    }
}
