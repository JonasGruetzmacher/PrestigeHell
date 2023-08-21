using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsManager : MonoBehaviour
{
    private List<Stats> statsList;
    private const string statsPath = "Assets/_MyGame/SO/Stats";

    private List<Upgrade> upgradeList;
    private const string upgradePath = "Assets/_MyGame/SO/Upgrades";

    private void OnApplicationQuit()
    {
        statsList = HelperFunctions.GetScriptableObjects<Stats>(statsPath);
        upgradeList = HelperFunctions.GetScriptableObjects<Upgrade>(upgradePath);

        foreach (var stats in statsList)
        {
            stats.ResetAppliedUpgrades();
        }

        foreach (var upgrade in upgradeList)
        {
            upgrade.ResetUpgrade();
        }
    }
}
