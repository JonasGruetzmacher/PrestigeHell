using System.Collections;
using System.Collections.Generic;
using MoreMountains.Tools;
using UnityEngine;

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
}

public enum Stat
{
    health,
    speed,
    touchDamage,
    
}
