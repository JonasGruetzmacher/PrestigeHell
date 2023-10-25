using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static LeroGames.Tools.HelperFunctions;
using LeroGames.Tools;
using Sirenix.OdinInspector;

namespace LeroGames.PrestigeHell
{
    public class SOManager : MonoBehaviour
    {
        [ReadOnly]
        private List<Stats> statsList;
        private const string statsPath = "Assets/_MyGame/SO/Stats";

        private List<Upgrade> upgradeList;
        private const string upgradePath = "Assets/_MyGame/SO/Upgrades";


        private void Awake()
        {
            statsList = GetScriptableObjects<Stats>(statsPath);
            upgradeList = GetScriptableObjects<Upgrade>(upgradePath);
        }

        private void OnApplicationQuit()
        {
            statsList = GetScriptableObjects<Stats>(statsPath);
            upgradeList = GetScriptableObjects<Upgrade>(upgradePath);
            List<Stat> upgradedStats = GetScriptableObjects<Stat>("Assets/_MyGame/SO/Stats/UpgradedStats");

            foreach (var stat in upgradedStats)
            {
                stat.Reset();
            }

            foreach (var upgrade in upgradeList)
            {
                upgrade.ResetUpgrade();
            }
        }

        public void OnDangerUp(Component sender, object data)
        {
            if (data is ResourceAmount resourceAmount)
            {
                if (resourceAmount.type == ResourceType.Danger)
                {
                    foreach (var stat in statsList)
                    {
                        stat.UpdateUpgradedStats();
                    }
                }
            }
        }

        public void OnRestart(Component sender, object data)
        {
            foreach (var stat in statsList)
            {
                stat.UpdateUpgradedStats();
            }
        }
    }
}