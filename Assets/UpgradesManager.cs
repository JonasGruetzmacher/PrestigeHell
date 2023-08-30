using System.Collections;
using System.Collections.Generic;
using MoreMountains.Tools;
using MoreMountains.TopDownEngine;
using Sirenix.OdinInspector;
using UnityEngine;
using static HelperFunctions;


public class UpgradesManager : MMSingleton<UpgradesManager>, MMEventListener<TopDownEngineEvent>
{
    [SerializeField] private GameObject upgradePrefab;
    [SerializeField] private Transform upgradesParent;

    [ShowInInspector, ReadOnly]
    private List<Upgrade> _upgrades = new List<Upgrade>();

    protected void Start()
    {
        

        Initialize();

        
    }

    private void Initialize()
    {
        _upgrades = GetScriptableObjects<Upgrade>("Assets/_MyGame/SO/Upgrades");

        foreach (var upgrade in _upgrades)
        {
            AddUpgradeToUI(upgrade);
        }

    }

    private void AddUpgradeToUI(Upgrade upgrade)
    {
        var upgradeGO = Instantiate(upgradePrefab, upgradesParent);
        upgradeGO.GetComponent<UpgradeButton>().SetUpgrade(upgrade);
        upgradeGO.GetComponent<UpgradeButton>().Initialize();
    }

    

    private void ResetLevelUpgrades()
    {
        foreach (var upgrade in _upgrades)
        {
            upgrade.ResetUpgrade();
        }
    }

    public void OnMMEvent(TopDownEngineEvent eventType)
    {
        if (eventType.EventType == TopDownEngineEventTypes.RespawnStarted)
        {
            ResetLevelUpgrades();
        }
    }
}
