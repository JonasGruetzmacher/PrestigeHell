using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using System.Resources;


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
        Debug.Log("Upgrade purchased");
        currentUpgradeCount++;
        foreach (var unitToUpgrade in unitsToUpgrade)
        {
            unitToUpgrade.UnlockUpgrade(this);
        }
        foreach (var blockUpgrade in blockedUpgrades)
        {
            blockUpgrade.Unlock(false);
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

    public override void ResetUpgrade()
    {
        currentUpgradeCount = 0;
        isUnlocked = false;
    }
}
