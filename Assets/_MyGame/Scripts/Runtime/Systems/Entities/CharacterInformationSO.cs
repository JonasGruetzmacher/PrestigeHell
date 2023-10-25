using System.Collections;
using System.Collections.Generic;
using System.Linq;

// using LeroGames.StatSystem;
using LeroGames.Tools;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using UnityEditor;
using UnityEngine;

namespace LeroGames.PrestigeHell
{
    [CreateAssetMenu(fileName = "CharacterInformation", menuName = "Character/InformationSO")]
    public class CharacterInformationSO : SerializedScriptableObject, ITooltipInformation
    {
        public List<StatType> requiredStats = new List<StatType>();
        public Stats stats;
        public string characterName;
        public Sprite characterSprite;

        public GameObject characterPrefab;
        public int poolSize = 20;


        public void Reset()
        {
            stats.Reset();
        }
        
        public void GetTooltipInformation(out string infoLeft, out string infoRight)
        {
            infoRight = "";
            infoLeft = string.Format("~{0}`\n", characterName);
            
            stats.GetTooltipInformation(out string statsInfoLeft, out string statsInfoRight);
            infoLeft += statsInfoLeft;
            infoRight += statsInfoRight;
        }

        [Button]
        public void Initialize()
        {
            foreach (var statType in requiredStats)
            {
                if (stats.IncludesStat(statType))
                {
                    continue;
                }
                // var asset = AssetDatabase.FindAssets(string.Format("{0}{1}", characterName, statType), new[] {string.Format("Assets/_MyGame/SO/Stats/Stats/{0}", characterName)});

            //     if (!asset.IsNullOrEmpty() && AssetDatabase.LoadAssetAtPath<Stat>(asset.First()) != null)
            //     {
            //         stats.stats.Add(AssetDatabase.LoadAssetAtPath<Stat>(string.Format("Assets/_MyGame/SO/Stats/Stats/{0}/{0}{1}.asset", characterName, statType)));
            //         Debug.Log(string.Format("Found {0} for {1}", statType, characterName));
            //     }
            //     else
            //     {
            //         // Stat stat = statType.CreateStat(characterName);
            //         // Debug.Log(string.Format("Created {0} for {1}", stat.name, characterName));
            //         // stats.stats.Add(stat);
            //     }
            }
        }

    }
}