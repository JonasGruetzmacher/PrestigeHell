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

    [SerializeField] private List<Upgrade> permanentUpgradeList = new List<Upgrade>();
    [SerializeField] private int maxUpgrades = 3;

    [ShowInInspector, ReadOnly]
    private Queue<Upgrade> upgrades = new Queue<Upgrade>();

    [ShowInInspector, ReadOnly]
    private List<Upgrade> currentUpgrades = new List<Upgrade>();

    [ShowInInspector, ReadOnly]
    private List<Upgrade> allUpgrades = new List<Upgrade>();

    protected void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        allUpgrades = GetScriptableObjects<Upgrade>("Assets/_MyGame/SO/Upgrades");

        foreach (var upgrade in permanentUpgradeList)
        {
            upgrades.Enqueue(upgrade);
        }

        for (int i = 0; i < maxUpgrades; i++)
        {
            if (upgrades.Count == 0)
            {
                break;
            }
            var upgrade = upgrades.Dequeue();
            currentUpgrades.Add(upgrade);
            AddUpgradeToUI(upgrade);
        }
    }

    private void AddUpgradeToUI(Upgrade upgrade)
    {
        var upgradeGO = Instantiate(upgradePrefab, upgradesParent);
        upgradeGO.GetComponent<BasicUpgradeVisual>().upgrade = upgrade;
        upgradeGO.GetComponent<BasicUpgradeVisual>().Init();
    }

    

    private void ResetLevelUpgrades()
    {
        foreach (var upgrade in allUpgrades)
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
