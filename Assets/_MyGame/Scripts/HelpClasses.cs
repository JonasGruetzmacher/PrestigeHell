using System.Collections;
using System.Collections.Generic;
using MoreMountains.Tools;
using Unity.VisualScripting;
using UnityEngine;
using Sirenix.OdinInspector;
using Sirenix.Serialization;

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
    xPGain = 6,
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
