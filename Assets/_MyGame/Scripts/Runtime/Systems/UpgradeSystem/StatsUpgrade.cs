using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using LeroGames.PrestigeHell;

namespace LeroGames.PrestigeHell
{
    [System.Serializable]
    [CreateAssetMenu(menuName = "Upgrades/Stats Upgrade")]
    public class StatsUpgrade : Upgrade
    {
        public List<Stats> unitsToUpgrade = new List<Stats>();
        public Dictionary<Stat, float> upgradeToApply = new Dictionary<Stat, float>();
        public bool isPercentageUpgrade = false;

        [Button]
        public override void DoUpgrade()
        {
            if (!CanPurchase())
            {
                Debug.Log("Not enough resources");
                return;
            }
            if (currentUpgradeCount >= upgradeLimit)
            {
                Debug.Log("Upgrade limit reached");
                return;
            }
            PayUpgrade();
            ApplyUpgrade();
            foreach (var unitToUpgrade in unitsToUpgrade)
            {
                unitToUpgrade.UnlockUpgrade(this);
            }
            foreach (var blockUpgrade in blockedUpgrades)
            {
                blockUpgrade.BlockUpgrade();
            }
        }

        public override void ForceUpgrade(int amount)
        {
            ResetUpgrade();
            for (int i = 0; i < amount; i++)
            {
                ApplyUpgrade();
                foreach (var unitToUpgrade in unitsToUpgrade)
                {
                    unitToUpgrade.UnlockUpgrade(this);
                }
                foreach (var blockUpgrade in blockedUpgrades)
                {
                    blockUpgrade.BlockUpgrade();
                }
            }
        }

        public override void ResetUpgrade()
        {
            base.ResetUpgrade();
            foreach (var unitToUpgrade in unitsToUpgrade)
            {
                unitToUpgrade.RemoveUpgrade(this);
            }
        }

        private bool CanPurchase()
        {
            if(GetNextUpgradeCost() == null)
            {
                return true;
            }
            foreach (var resourceAmount in GetNextUpgradeCost())
            {
                if (resourceAmount.Value > ResourcesManager.Instance.GetResourceAmount(resourceAmount.Key))
                {
                    return false;
                }
            }
            return true;
        }

        private void PayUpgrade()
        {
            if (GetNextUpgradeCost() == null)
            {
                return;
            }
            foreach (var resourceAmount in GetNextUpgradeCost())
            {
                ResourceEvent.Trigger(ResourceMethods.Remove, new ResourceAmount(resourceAmount.Key, (int)resourceAmount.Value));
            }
        }
    }
}