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
    }

    private bool CanPurchase()
    {
        foreach (var resourceAmount in cost)
        {
            if (resourceAmount.amount > ResourcesManager.Instance.GetResourceAmount(resourceAmount.type))
            {
                return false;
            }
        }
        return true;
    }

    private void PayUpgrade()
    {
        foreach (var resourceAmount in cost)
        {
            ResourceEvent.Trigger(ResourceMethods.Remove, resourceAmount);
        }
    }

    public override void ResetUpgrade()
    {
        currentUpgradeCount = 0;
    }


}
