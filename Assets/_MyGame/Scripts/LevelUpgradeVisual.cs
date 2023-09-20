using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LevelUpgrade))]
public class LevelUpgradeVisual : CButton
{
    private LevelUpgrade levelUpgrade;

    public override void Setup()
    {
        base.Setup();
        levelUpgrade = GetComponent<LevelUpgrade>();
        // levelUpgradeVisual.upgrade.upgradeApplied += UpgradeApplied;
        // levelUpgradeVisual.upgrade.upgradeCompleted += UpgradeCompleted;
    }

    public override void Configure()
    {
        base.Configure();
        
        buttonText.SetText(levelUpgrade.upgrade.upgradeName);
    }

    public override void OnClick()
    {
        base.OnClick();
        levelUpgrade.OnClick();
    }


    public void OnUpgradeStateChanged()
    {
        switch (levelUpgrade.state)
        {
            case LevelUpgradeState.Locked:
                Dissable();
                break;
            case LevelUpgradeState.Unlocked:
                Enable();
                buttonText.SetText(levelUpgrade.upgrade.upgradeName);
                break;
            case LevelUpgradeState.Disabled:
                buttonText.SetText("Locked");
                Dissable();
                break;
            case LevelUpgradeState.Selected:
                buttonText.SetText("Selected");
                Dissable();
                break;
        }
    }

    private void OnEnable()
    {
        levelUpgrade.upgradeStateChanged.AddListener(OnUpgradeStateChanged);
    }

    private void OnDisable()
    {
        levelUpgrade.upgradeStateChanged.RemoveListener(OnUpgradeStateChanged);
    }
}   
