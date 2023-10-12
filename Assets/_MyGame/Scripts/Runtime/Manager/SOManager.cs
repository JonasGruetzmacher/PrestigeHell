using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static LeroGames.Tools.HelperFunctions;

namespace LeroGames.PrestigeHell
{
    public class SOManager : MonoBehaviour
    {
        private List<Stats> statsList;
        private const string statsPath = "Assets/_MyGame/SO/Stats";

        private List<Upgrade> upgradeList;
        private const string upgradePath = "Assets/_MyGame/SO/Upgrades";

        private void OnApplicationQuit()
        {
            statsList = GetScriptableObjects<Stats>(statsPath);
            upgradeList = GetScriptableObjects<Upgrade>(upgradePath);



            foreach (var upgrade in upgradeList)
            {
                upgrade.ResetUpgrade();
            }
        }
    }
}