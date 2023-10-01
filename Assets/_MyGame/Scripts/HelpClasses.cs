using System.Collections;
using System.Collections.Generic;
using MoreMountains.Tools;
using Unity.VisualScripting;
using UnityEngine;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using System.Linq;

public static class HelperFunctions
{
    public static List<T> GetScriptableObjects<T>(string path) where T : ScriptableObject
    {
#if UNITY_EDITOR


        string[] guids = UnityEditor.AssetDatabase.FindAssets("t:" + typeof(T).ToString(), new[] { path });
        List<T> scriptableObjects = new List<T>();

        foreach (var guid in guids)
        {
            UnityEditor.AssetDatabase.GUIDToAssetPath(guid);
            string assetPath = UnityEditor.AssetDatabase.GUIDToAssetPath(guid);
            scriptableObjects.Add(UnityEditor.AssetDatabase.LoadAssetAtPath(assetPath, typeof(T)) as T);
        }

        return scriptableObjects;
#else
        return null;
#endif
    }

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

    public static string ToMarkedString(this Stat stat)
    {

        switch (stat)
        {
            case Stat.health:
                return "!Max Health`";
            case Stat.speed:
                return "!Move Speed`";
            case Stat.touchDamage:
                return "!Damage`";
            case Stat.attackSpeed:
                return "!Attack Speed`";
            case Stat.damageReduction:
                return "!Damage Reduction`";
            case Stat.collectRange:
                return "!Collect Range`";
            case Stat.XPGain:
                return "!XP Gain`";
            case Stat.healthVariance:
                return null;
            case Stat.groupSize:
                return "!Group Size`";
            case Stat.groupSizeVariance:
                return null;
            case Stat.groupSpawnRadius:
                return null;
            default:
                return stat.ToString();
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

public struct ScalingStat
{
    [HideLabel]
    public AnimationCurve curve;
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

public enum LevelUpgradeState
{
    Locked = 0,
    Unlocked = 1,
    Selected = 2,
    Disabled = 3,
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
