using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using System;
using LeroGames.Tools;

namespace LeroGames.PrestigeHell
{            
    [CreateAssetMenu(menuName = "Character/StatsSO")]
    public class Stats : SerializedScriptableObject, ITooltipInformation
    {
        public List<Stat> stats = new List<Stat>();
        
        [ShowInInspector, ReadOnly]private SerializableDictionary<StatType, FloatVariable> baseStats = new SerializableDictionary<StatType, FloatVariable>();
        [ShowInInspector, ReadOnly]private SerializableDictionary<StatType, FloatVariable> upgradedStats = new SerializableDictionary<StatType, FloatVariable>();

        [DictionaryDrawerSettings(DisplayMode = DictionaryDisplayOptions.OneLine)]
        public Dictionary<StatType, ScalingStat> statCurves = new Dictionary<StatType, ScalingStat>();
        [ShowInInspector, ReadOnly] private List<StatsUpgrade> appliedUpgrades = new List<StatsUpgrade>();

        public event Action<Stats, StatsUpgrade> upgradeApplied;


        [Button]
        public void Initialize()
        {
            baseStats.Clear();
            upgradedStats.Clear();
            appliedUpgrades.Clear();
            if (stats == null)
                return;
            foreach (var stat in stats)
            {
                stat?.Reset();
                baseStats?.Add(stat.StatType, stat.BaseValue);
                upgradedStats?.Add(stat.StatType, stat.UpgradedValue);
            }
            UpdateUpgradedStats();
        }

        public bool IncludesStat(StatType statType)
        {
            if (stats == null)
                return false;

            foreach (var stat in stats)
            {
                if (stat.StatType == statType)
                    return true;
            }
            return false;
        }

        public float GetBaseStat(StatType statType)
        {
            if (!baseStats.ContainsKey(statType))
            {
                return 0f;
            }
            if (baseStats[statType] != null)
            {
                return baseStats[statType].Value;
            }
            return 0f;
        }

        public float GetStat(StatType statType)
        {
            if (upgradedStats.ContainsKey(statType) && upgradedStats[statType] != null)
            {
                return upgradedStats[statType].Value;
            }
            return GetBaseStat(statType);
        }

        /// <summary>
        /// Adds the upgrade to the list of applied upgrades and updates the upgraded stats
        /// </summary>
        /// <param name="upgrade"></param>
        public void AddUpgrade(StatsUpgrade upgrade)
        {
            appliedUpgrades.Add(upgrade);
            UpdateUpgradedStats();
            upgradeApplied?.Invoke(this, upgrade);
        }


        /// <summary>
        /// Removes the upgrade from the list of applied upgrades and updates the upgraded stats
        /// </summary>
        /// <param name="upgrade"></param>
        public void RemoveUpgrade(StatsUpgrade upgrade)
        {
            if (appliedUpgrades == null)
                return;
            appliedUpgrades.Remove(upgrade);
            UpdateUpgradedStats();
            upgradeApplied?.Invoke(this, null);
        }

        public void UpdateUpgradedStats()
        {
            if (upgradedStats == null)
                return;
            foreach (var stat in upgradedStats)
            {
                if (stat.Value == null)
                    continue;
                AddScaling(stat.Key, stat.Value);
                AddUpgrades(stat.Key, stat.Value);
            }
        }


        private void AddScaling(StatType statType, FloatVariable upgradedStat)
        {
            if (statCurves == null)
            {
                upgradedStat.SetValue(GetBaseStat(statType));
                return;
            }

            if (!statCurves.ContainsKey(statType))
            {
                upgradedStat.SetValue(GetBaseStat(statType));
                return;
            }
            if (!Application.isPlaying)
            {
                upgradedStat.SetValue(GetBaseStat(statType));
                return;
            }
            upgradedStat.Value = baseStats[statType].Value + statCurves[statType].curve.Evaluate(ResourcesManager.Instance.GetResourceAmount(ResourceType.Danger));
        }

        private void AddUpgrades(StatType statType, FloatVariable upgradedStat)
        {
            if (appliedUpgrades == null)
            {
                return;
            }
            foreach (var upgrade in appliedUpgrades)
            {
                if(!upgrade.upgradeToApply.TryGetValue(statType, out var upgradeValue))
                    continue;
                if(upgrade.isPercentageUpgrade)
                    upgradedStat.Value *= (upgradeValue / 100f) + 1f;
                else
                    upgradedStat.Value += upgradeValue;
            }
        }

        public void GetTooltipInformation(out string infoLeft, out string infoRight)
        {
            infoLeft = "";
            infoRight = "";
            foreach (var stat in upgradedStats)
            {
                if (stat.Key.ToMarkedString() == null)
                    continue;

                infoLeft += string.Format("{0}: {1}\n", stat.Key.ToMarkedString(), GetStat(stat.Key).ToString("F2"));
            }
        }

        private void OnValidate()
        {
            Initialize();
        }

        private void OnEnable()
        {
            Initialize();
        }

        [Button]
        public void Reset()
        {
            appliedUpgrades.Clear();
            foreach (var stat in stats)
            {
                stat.Reset();
            }
            UpdateUpgradedStats();
        }

        [Button]
        public void AddScalingStat(StatType statType, (float, float)[] keyframes)
        {
            if (statCurves.ContainsKey(statType))
            {
                statCurves.Remove(statType);
            }
            statCurves.Add(statType, new ScalingStat(keyframes));
        }
    }
    // public enum Stat
    // {
    //     health = 0,
    //     speed = 1,
    //     touchDamage = 2,
    //     attackSpeed = 3,
    //     damageReduction = 4,
    //     collectRange = 5,
    //     XPGain = 6,
    //     healthVariance = 7,
    //     groupSize = 8,
    //     groupSizeVariance = 9,
    //     groupSpawnRadius = 10,
    // }
}
