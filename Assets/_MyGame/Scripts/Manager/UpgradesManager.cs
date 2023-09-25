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

    [ShowInInspector, ReadOnly, SerializeField]
    private List<Upgrade> allUpgrades = new List<Upgrade>();

    [ShowInInspector, ReadOnly, SerializeField]
    private List<Upgrade> levelUpgrades = new List<Upgrade>();

    [ShowInInspector, ReadOnly]
    private List<Upgrade> activeUpgrades = new List<Upgrade>();

    protected void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
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
            
            upgrade.upgradeCompleted += OnUpgradeCompleted;

            AddUpgradeToUI(upgrade);
        }
    }

    private void AddUpgradeToUI(Upgrade upgrade)
    {
        var upgradeGO = Instantiate(upgradePrefab, upgradesParent);
        upgradeGO.GetComponent<BasicUpgradeVisual>().upgrade = upgrade;
        upgradeGO.GetComponent<BasicUpgradeVisual>().Init();
    }

    private void OnUpgradeCompleted(Upgrade upgrade)
    {
        upgrade.upgradeCompleted -= OnUpgradeCompleted;
        currentUpgrades.Remove(upgrade);

        if (upgrades.Count > 0)
        {
            var newUpgrade = upgrades.Dequeue();
            currentUpgrades.Add(newUpgrade);
            newUpgrade.upgradeCompleted += OnUpgradeCompleted;
            AddUpgradeToUI(newUpgrade);
        }
    }

    

    private void ResetLevelUpgrades()
    {
        foreach (var upgrade in levelUpgrades)
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

    private void OnEnable()
    {
        this.MMEventStartListening<TopDownEngineEvent>();
    }

    private void OnDisable()
    {
        this.MMEventStopListening<TopDownEngineEvent>();
    }

    private void OnValidate()
    {
        allUpgrades = GetScriptableObjects<Upgrade>("Assets/_MyGame/SO/Upgrades");
        levelUpgrades = GetScriptableObjects<Upgrade>("Assets/_MyGame/SO/Upgrades/LevelUpgrades");
    }
}
