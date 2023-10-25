using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using LeroGames.Tools;

namespace LeroGames.PrestigeHell
{
    [CreateAssetMenu(fileName = "LevelUpgrade", menuName = "Upgrades/LevelUpgrade")]
    public class LevelUpgradeSO : SerializedScriptableObject, ITooltipInformation
    {
        public string id;
        public string upgradeName;
        public int levelRequirement;
        public List<Upgrade> upgradesToUnlock = new List<Upgrade>();
        public List<LevelUpgradeSO> getBlockedBy = new List<LevelUpgradeSO>();


        public void GetTooltipInformation(out string infoLeft, out string infoRight)
        {
            infoLeft = string.Format("!{0}`\n", upgradeName);
            infoRight = "";
            foreach (var upgrade in upgradesToUnlock)
            {
                if (upgrade is StatsUpgrade statsUpgrade)
                {
                    upgrade.GetTooltipInformation(out string statsInfoLeft, out string statsInfoRight);
                    infoLeft += statsInfoLeft;
                    infoRight += statsInfoRight;
                }
                else
                {
                    infoLeft += string.Format("!{0}`\n", upgrade.shortDescription);
                }
            }
        }


        public void Reset()
        {

        }


    }
}
