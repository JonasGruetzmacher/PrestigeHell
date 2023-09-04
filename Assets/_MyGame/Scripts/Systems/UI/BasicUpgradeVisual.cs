using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicUpgradeVisual : CUpgradeButton
{
    public Upgrade upgrade;


    public override void Configure()
    {
        base.Configure();

        header.SetText(upgrade.name);
        description.SetText(upgrade.description);
        price.SetText(upgrade.GetNextUpgradeCosts());
        bought.SetText(upgrade.currentUpgradeCount.ToString() + "/" + upgrade.upgradeLimit.ToString());
        header.autoSize = true;
        description.autoSize = true;
        price.autoSize = true;
        bought.autoSize = true;
    }
}
