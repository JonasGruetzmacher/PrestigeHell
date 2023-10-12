using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using System;
using LeroGames.Tools;
using System.Resources;
using Unity.VisualScripting.YamlDotNet.Core.Tokens;
using UnityEngine.PlayerLoop;

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


        private void Awake()
        {
            Initialize();
        }

        [Button]
        public void Initialize()
        {
            baseStats.Clear();
            upgradedStats.Clear();
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

        public void AddUpgrade(StatsUpgrade upgrade)
        {
            appliedUpgrades.Add(upgrade);
            UpdateUpgradedStats();
            upgradeApplied?.Invoke(this, upgrade);
        }

        public void RemoveUpgrade(StatsUpgrade upgrade)
        {
            appliedUpgrades.Remove(upgrade);
            UpdateUpgradedStats();
            upgradeApplied?.Invoke(this, null);
        }

        private void UpdateUpgradedStats()
        {
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

                // if (stat.Key == Stat.groupSize && IncludesStat(Stat.groupSizeVariance))
                // {
                //     infoLeft += string.Format("{0}: {1} - {2}\n", stat.Key.ToMarkedString(), GetStat(stat.Key) - GetStat(Stat.groupSizeVariance), GetStat(stat.Key) + GetStat(Stat.groupSizeVariance));
                //     continue;
                // }
                infoLeft += string.Format("{0}: {1}\n", stat.Key.ToMarkedString(), GetStat(stat.Key).ToString("F2"));
            }

            // foreach (var instanceStat in instanceStats)
            // {
            //     if (instanceStat.Key.ToMarkedString() == null)
            //         continue;
            //     if (instanceStat.Key == Stat.health && IncludesStat(Stat.healthVariance))
            //     {
            //         float minHealth = GetStat(instanceStat.Key) * (1f - GetStat(Stat.healthVariance));
            //         float maxHealth = GetStat(instanceStat.Key) * (1f + GetStat(Stat.healthVariance));
            //         infoLeft += string.Format("{0}: {1} - {2}\n", instanceStat.Key.ToMarkedString(), minHealth.ToString("F2"), maxHealth.ToString("F2"));
            //         continue;
            //     }
            //     infoLeft += string.Format("{0}: {1}\n", instanceStat.Key.ToMarkedString(), GetStat(instanceStat.Key).ToString("F2"));
            // }
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
