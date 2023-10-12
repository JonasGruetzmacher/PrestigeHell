using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using System;
using LeroGames.Tools;

namespace LeroGames.PrestigeHell
{
    public abstract class Upgrade : BaseSO, ITooltipInformation
    {
        public bool isPayed = true;
        [ShowIf("isPayed")]
        public Dictionary<ResourceType, float> upgradeCost = new Dictionary<ResourceType, float>();
        public int upgradeLimit = 1;
        public int currentUpgradeCount = 0;
        public event Action<Upgrade> upgradeStateChanged;
        public event Action<Upgrade> upgradeCompleted;
        public event Action<Upgrade> upgradeApplied;
        public event Action<Upgrade> upgradeReset;
        public Dictionary<ResourceType, float> GetNextUpgradeCost()
        {
            return upgradeCost;
        }


        [Button]
        public abstract void DoUpgrade();

        public virtual void Unlock(bool unlock = true)
        {
            upgradeStateChanged?.Invoke(this);
        }

        public virtual void ApplyUpgrade()
        {
            currentUpgradeCount++;
            upgradeApplied?.Invoke(this);
            if (currentUpgradeCount >= upgradeLimit)
            {
                Completed();
            }
        }

        public virtual void ForceUpgrade(int count = 1)
        {
            ResetUpgrade();
            currentUpgradeCount = count;
            upgradeApplied?.Invoke(this);
            if (currentUpgradeCount >= upgradeLimit)
            {
                Completed();
            }
        }

        public virtual void ResetUpgrade()
        {
            currentUpgradeCount = 0;
            upgradeReset?.Invoke(this);
        }

        public virtual void Completed()
        {
            upgradeCompleted?.Invoke(this);
        }

        public virtual bool IsCompleted()
        {
            return currentUpgradeCount >= upgradeLimit;
        }

        public virtual String GetNextUpgradeCosts()
        {
            String costs = "";
            foreach (var resource in upgradeCost)
            {
                costs += resource.Value.ToString() + " " + resource.Key.ToString() + "\n";
            }
            return costs;
        }

        public virtual void GetTooltipInformation(out string infoLeft, out string infoRight)
        {
            infoLeft = "";
            infoRight = "";

            infoLeft += string.Format("!{0}`\n", upgradeName);
            infoLeft += string.Format("{0}`\n", shortDescription);

            if (GetNextUpgradeCost().Count > 0)
            {
                infoRight += string.Format("{0}`\n", GetNextUpgradeCosts());
            }
        }

    }
}