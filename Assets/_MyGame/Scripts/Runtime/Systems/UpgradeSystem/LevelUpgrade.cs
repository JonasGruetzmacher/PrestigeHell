using System.Collections;
using System.Collections.Generic;
using MoreMountains.Tools;
using UnityEngine;
using UnityEngine.Events;
using LeroGames.Tools;
using log4net.Core;
using MoreMountains.TopDownEngine;
// using LeroGames.StatSystem;

namespace LeroGames.PrestigeHell
{
    public class LevelUpgrade
    {
        
        public LevelUpgradeSO levelUpgradeSO { get; private set; }
        
        private bool alreadyGotUnlocked = false;
        private bool blocked = false;
        private bool selected = false;


        public enum LevelUpgradeState
        {
            Hidden = 0,
            Shown = 1,
            Pickable = 4,
            Selected = 2,
            Blocked = 3,
            Unassigned = 100
        }

        public LevelUpgrade(LevelUpgradeSO levelUpgradeSO)
        {
            this.levelUpgradeSO = levelUpgradeSO;
            alreadyGotUnlocked = false;
        }

        public string GetShortDescription()
        {
            if (blocked)
            {
                return "Blocked";
            }
            if (selected)
            {
                return "Selected";
            }
            return levelUpgradeSO.upgradeName;
        }

        public void SelectLevelUpgrade()
        {
            selected = true;
            foreach (var upgrade in levelUpgradeSO.upgradesToUnlock)
            {
                Debug.Log("Applying upgrade " + upgrade.upgradeName);
                upgrade.ForceUpgrade();
            }
        }

        public void ResetLevelUpgrade()
        {
            selected = false;
            blocked = false;
            foreach (var upgrade in levelUpgradeSO.upgradesToUnlock)
            {
                upgrade.ResetUpgrade();
            }
        }

        public void BlockLevelUpgrade()
        {
            blocked = true;
        }


        public void GetTooltipInformation(out string infoLeft, out string infoRight)
        {
            levelUpgradeSO.GetTooltipInformation(out infoLeft, out infoRight);
            infoRight += string.Format("Level {0}", levelUpgradeSO.levelRequirement);
        }

        public LevelUpgradeState GetLevelUpgradeState()
        {
            if (blocked)
            {
                return LevelUpgradeState.Blocked;
            }
            if (selected)
            {
                return LevelUpgradeState.Selected;
            }
            if (ResourcesManager.Instance.GetResourceAmountInt(ResourceType.Level) >= levelUpgradeSO.levelRequirement)
            {
                alreadyGotUnlocked = true;
                return LevelUpgradeState.Pickable;
            }
            if (alreadyGotUnlocked)
            {
                return LevelUpgradeState.Shown;
            }
            return LevelUpgradeState.Hidden;

        }

        public void Reset()
        {
            alreadyGotUnlocked = false;
            blocked = false;
            selected = false;
        }

    }
}