using System.Collections;
using System.Collections.Generic;
// using LeroGames.StatSystem;
using UnityEngine;

namespace LeroGames.PrestigeHell
{
    public class BasicUpgradeVisual : CUpgradeButton
    {
        public Upgrade upgrade;


        public override void Configure()
        {
            base.Configure();

            if (upgrade == null)
                return;
            header.SetText(upgrade.upgradeName);
            description.SetText(upgrade.shortDescription);
            price.SetText(upgrade.GetNextUpgradeCosts());
            bought.SetText(upgrade.currentUpgradeCount.ToString() + "/" + upgrade.upgradeLimit.ToString());
            header.autoSize = true;
            description.autoSize = true;
            price.autoSize = true;
            bought.autoSize = true;
        }

        private void UpdateText()
        {
            price.SetText(upgrade.GetNextUpgradeCosts());
            bought.SetText(upgrade.currentUpgradeCount.ToString() + "/" + upgrade.upgradeLimit.ToString());
        }

        public override void Setup()
        {
            base.Setup();
            upgrade.upgradeApplied += UpgradeApplied;
            upgrade.upgradeCompleted += UpgradeCompleted;
        }

        public void UpgradeApplied(Upgrade upgrade)
        {
            UpdateText();
        }

        public void UpgradeCompleted(Upgrade upgrade)
        {
            Destroy(gameObject);
        }


        public override void OnClick()
        {
            base.OnClick();
            upgrade.DoUpgrade();

        }

        private void OnDestroy()
        {
            upgrade.upgradeApplied -= UpgradeApplied;
            upgrade.upgradeCompleted -= UpgradeCompleted;
        }
    }
}