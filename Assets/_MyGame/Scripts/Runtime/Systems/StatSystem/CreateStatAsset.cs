using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

namespace LeroGames.PrestigeHell
{
    public static class CreateStatAsset
    {
        public static Stat CreateStat(this StatType statType, string name, float baseValue = 0f)
        {
            var stat = ScriptableObject.CreateInstance<Stat>();
            stat.name = name;
            stat.StatType = statType;
            if (!Directory.Exists(string.Format("Assets/_MyGame/SO/Stats/Stats/{0}", name)))
            {
                Directory.CreateDirectory(string.Format("Assets/_MyGame/SO/Stats/Stats/{0}", name));
            }
            AssetDatabase.CreateAsset(stat, string.Format("Assets/_MyGame/SO/Stats/Stats/{0}/{0}{1}.asset", name, statType.ToString()));

            AssetDatabase.SaveAssets();
            stat.CreateStatAsset(baseValue, name);

            return stat;
        }
    }
}