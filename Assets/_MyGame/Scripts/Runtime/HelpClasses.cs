using System.Collections;
using System.Collections.Generic;
using MoreMountains.Tools;
using Unity.VisualScripting;
using UnityEngine;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using System.Linq;
using log4net.Core;
using LeroGames.Tools;

namespace LeroGames.PrestigeHell
{
    public static class HelperFunctions
    {
        

        public static void SetLootWeight(this MMLootTableGameObjectSO lootTable, GameObject lootDropPrefab, int weight)
        {
            if (lootTable == null)
                return;
            if (lootDropPrefab == null)
                return;
            foreach (var loot in lootTable.LootTable.ObjectsToLoot)
            {
                if (loot.Loot == lootDropPrefab)
                {
                    loot.Weight = weight;
                    lootTable.ComputeWeights();
                    return;
                }
            }
            // Debug.LogError("LootDropPrefab not found in LootTable");
        }

        public static void RemoveLoot(this MMLootTableGameObjectSO lootTable, GameObject lootDropPrefab)
        {
            SetLootWeight(lootTable, lootDropPrefab, 0);
        }

        public static void ForEach<T>(this IEnumerable<T> source, System.Action<T> action)
        {
            source.ThrowIfNull("source");
            action.ThrowIfNull("action");
            foreach (var element in source)
            {
                action(element);
            }
        }

        public static void ThrowIfNull<T>(this T argument, string name) where T : class
        {
            if (argument == null)
            {
                throw new System.ArgumentNullException(name);
            }
        }

        public static int CountAllOf<T>(this List<T> list, T item)
        {
            int count = 0;
            foreach (var i in list)
            {
                if (i.Equals(item))
                    count++;
            }
            return count;
        }

        public static void ClearChildren(this Transform transform)
        {
            foreach (Transform child in transform)
            {
                GameObject.Destroy(child.gameObject);
            }
        }

        public static string ToPrettyString<T,V>(this Dictionary<T,V> dict)
        {
            string text = "";
            foreach (var pair in dict)
            {
                text += pair.ToPrettyString() + "\n";
            }
            return text;
        }

        public static string ToPrettyString<T,V>(this KeyValuePair<T,V> pair)
        {
            return $"{pair.Key}: {pair.Value}";
        }

        public static string ToMarkedString(this StatType stat)
        {

            switch (stat.name)
            {
                case "health":
                    return "!Max Health`";
                // case Stat.speed:
                //     return "!Move Speed`";
                // case Stat.touchDamage:
                //     return "!Damage`";
                // case Stat.attackSpeed:
                //     return "!Attack Speed`";
                // case Stat.damageReduction:
                //     return "!Damage Reduction`";
                // case Stat.collectRange:
                //     return "!Collect Range`";
                // case Stat.XPGain:
                //     return "!XP Gain`";
                // case Stat.healthVariance:
                //     return null;
                // case Stat.groupSize:
                //     return "!Group Size`";
                // case Stat.groupSizeVariance:
                //     return null;
                // case Stat.groupSpawnRadius:
                //     return null;
                default:
                    return stat.ToString();
            }
        }

        public static CButton.ButtonState ToButtonState(this LevelUpgrade.LevelUpgradeState state)
        {
            switch (state)
            {
                case LevelUpgrade.LevelUpgradeState.Blocked:
                    return CButton.ButtonState.Blocked;
                case LevelUpgrade.LevelUpgradeState.Hidden:
                    return CButton.ButtonState.Hidden;
                case LevelUpgrade.LevelUpgradeState.Pickable:
                    return CButton.ButtonState.Activated;
                case LevelUpgrade.LevelUpgradeState.Selected:
                    return CButton.ButtonState.Selected;
                case LevelUpgrade.LevelUpgradeState.Shown:
                    return CButton.ButtonState.Disabled;
                default:
                    return CButton.ButtonState.Hidden;
            }
        }

    }



    [System.Serializable]
    public struct ResourceAmount
    {
        public ResourceAmount(ResourceType type, int amount)
        {
            this.type = type;
            this.amount = amount;
        }

        public ResourceType type;
        public int amount;

        public static ResourceAmount operator +(ResourceAmount a, ResourceAmount b)
        {
            return new ResourceAmount(a.type, a.amount + b.amount);
        }

        public static ResourceAmount operator -(ResourceAmount a, ResourceAmount b)
        {
            return new ResourceAmount(a.type, a.amount - b.amount);
        }

        public override string ToString()
        {
            return $"{type} {amount}";
        }

        public string ToPrettyString()
        {
            return $"{type}: {amount}";
        }

        public void ClearResource()
        {
            amount = 0;
        }
    }

    public enum ResourceType
    {
        XP,
        Level,
        LevelPoints,
        Danger,
        dangerPoints,
    }



    public struct ScalingStat
    {
        [HideLabel]
        public AnimationCurve curve;

        public ScalingStat(AnimationCurve curve)
        {
            this.curve = curve;
        }

        public ScalingStat(float[] keys, float[] values)
        {
            this.curve = new AnimationCurve();
            for (int i = 0; i < keys.Length; i++)
            {
                this.curve.AddKey(keys[i], values[i]);
            }
        }

        public ScalingStat((float, float)[] keyFrames)
        {
            this.curve = new AnimationCurve();
            for (int i = 0; i < keyFrames.Length; i++)
            {
                this.curve.AddKey(keyFrames[i].Item1, keyFrames[i].Item2);
            }
        }
    }

    public struct GameEvent
    {
        public Eventname eventName;

        public GameEvent(Eventname name)
        {
            this.eventName = name;
        }


        static GameEvent e;
        public static void Trigger(Eventname eventName)
        {
            e.eventName = eventName;
            MMEventManager.TriggerEvent(e);
        }
    }

    [System.Serializable]
    public struct StatisticEvent
    {
        public StatisticType type;
        public string attribute;
        public float value;
        public StatisticEvent(StatisticType type, string attribute = "", float value = 0)
        {
            this.type = type;
            this.attribute = attribute;
            this.value = value;
        }

        static StatisticEvent e;

        public static void Trigger(StatisticType type, string attribute = "", float value = 0)
        {
            e.type = type;
            e.attribute = attribute;
            e.value = value;
            MMEventManager.TriggerEvent(e);
        }

        public void Trigger()
        {
            MMEventManager.TriggerEvent(this);
        }
    }

    public enum Eventname
    {
        DangerChanged = 0,
        LevelUp = 1,
    }



    public enum Style
    {
        None = 0,
        Primary = 1,
        Secondary = 2,
        Tertiary = 3,
    }

    public enum StatisticType
    {
        None = 0,
        Kill = 1,
        Collect = 2,
        Death = 3,
    }
}