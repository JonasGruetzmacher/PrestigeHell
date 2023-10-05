using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LeroGames.Tools
{
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

    }

    [System.Serializable]
    public class SerializableDictionary<TKey, TValue> : Dictionary<TKey, TValue>, ISerializationCallbackReceiver
    {
        [SerializeField] private List<TKey> keys = new List<TKey>();
        [SerializeField] private List<TValue> values = new List<TValue>();

        public void OnBeforeSerialize()
        {
            keys.Clear();
            values.Clear();
            foreach (var pair in this)
            {
                keys.Add(pair.Key);
                values.Add(pair.Value);
            }
        }

        public void OnAfterDeserialize()
        {
            this.Clear();
            for (int i = 0; i < keys.Count; i++)
            {
                this.Add(keys[i], values[i]);
            }
        }
    }
}
