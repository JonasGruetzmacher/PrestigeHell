using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class UpgradeButton : MonoBehaviour
{
    [SerializeField] private Upgrade upgrade;
    [SerializeField] private Button button;

    [SerializeField] private TextMeshProUGUI upgradeName;
    [SerializeField] private TextMeshProUGUI upgradeDescription;
    [SerializeField] private TextMeshProUGUI upgradeCost;


    private void Start()
    {
        upgradeName.text = upgrade.upgradeName;
        // upgradeDescription.text = upgrade.description;
        string text = "";
        foreach (var resource in upgrade.GetNextUpgradeCost())
        {
            text += resource.ToPrettyString() + "\n";
        }
        upgradeCost.text = text;
    }

    public void OnClick()
    {
        upgrade.DoUpgrade();
        if(upgrade.currentUpgradeCount >= upgrade.upgradeLimit)
            button.interactable = false;
    }
}
