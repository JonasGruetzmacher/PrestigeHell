using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using LeroGames.PrestigeHell;
using LeroGames.Tools;

namespace LeroGames.PrestigeHell
{
    [System.Serializable]
    [CreateAssetMenu(menuName = "Upgrades/Stats Upgrade")]
    public class StatsUpgrade : Upgrade
    {
        public List<Stats> unitsToUpgrade = new List<Stats>();
        public Dictionary<StatType, float> upgradeToApply = new Dictionary<StatType, float>();
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
                unitToUpgrade?.AddUpgrade(this);
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
                    unitToUpgrade?.AddUpgrade(this);
                }
            }
        }

        public override void ResetUpgrade()
        {
            base.ResetUpgrade();
            if (unitsToUpgrade == null)
            {
                return;
            }
            foreach (var unitToUpgrade in unitsToUpgrade)
            {
                unitToUpgrade?.RemoveUpgrade(this);
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

        public override void GetTooltipInformation(out string infoLeft, out string infoRight)
        {
            infoLeft = "";
            infoRight = "";

            foreach (var upgrade in upgradeToApply)
            {
                infoLeft += string.Format("{0}: {1}{2}`\n", upgrade.Key.ToMarkedString(), upgrade.Value.ToString("F2"), isPercentageUpgrade ? "%" : "");
            }
            
        }
    }
}