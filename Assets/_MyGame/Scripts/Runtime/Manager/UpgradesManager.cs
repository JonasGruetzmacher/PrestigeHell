using System.Collections;
using System.Collections.Generic;
using MoreMountains.Tools;
using MoreMountains.TopDownEngine;
using Sirenix.OdinInspector;
using UnityEngine;
using System;
using LeroGames.Tools;
using static LeroGames.Tools.HelperFunctions;

namespace LeroGames.PrestigeHell
{
    public class UpgradesManager : MMSingleton<UpgradesManager>, MMEventListener<TopDownEngineEvent>, IDataPersistence
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
            // Initialize();
        }

        private void Initialize()
        {
            currentUpgrades.Clear();
            activeUpgrades.Clear();
            upgrades.Clear();
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
                if (upgrade.IsCompleted())
                {
                    activeUpgrades.Add(upgrade);
                    i--;
                }
                else
                {
                    currentUpgrades.Add(upgrade);
                    upgrade.upgradeCompleted += OnUpgradeCompleted;
                }
            }

            UpdateUI();
        }

        public void LoadData(GameData gameData)
        {
            foreach (var upgrade in allUpgrades)
            {
                upgrade.ResetUpgrade();
                if (gameData.permanentUpgrades == null)
                {
                    continue;
                }
                if (gameData.permanentUpgrades.ContainsKey(upgrade.id))
                {
                    upgrade.ForceUpgrade(gameData.permanentUpgrades[upgrade.id]);
                }
            }
            Initialize();
        }

        public void SaveData(GameData gameData)
        {
            foreach (var upgrade in allUpgrades)
            {
                if (gameData.permanentUpgrades.ContainsKey(upgrade.id))
                {
                    gameData.permanentUpgrades.Remove(upgrade.id);
                }
                gameData.permanentUpgrades.Add(upgrade.id, upgrade.currentUpgradeCount);
            }
        }

        private void UpdateUI()
        {
            upgradesParent.ClearChildren();
            foreach (var upgrade in currentUpgrades)
            {
                var upgradeGO = Instantiate(upgradePrefab, upgradesParent);
                upgradeGO.GetComponent<BasicUpgradeVisual>().upgrade = upgrade;
                upgradeGO.GetComponent<BasicUpgradeVisual>().Init();    
            }
        }

        private void OnUpgradeCompleted(Upgrade upgrade)
        {
            Debug.Log("OnUpgradeCompleted" + upgrade.name);
            upgrade.upgradeCompleted -= OnUpgradeCompleted;
            currentUpgrades.Remove(upgrade);

            FillUpgradeSlots();
        }

        private void FillUpgradeSlots()
        {
            while (currentUpgrades.Count < maxUpgrades)
            {
                if (upgrades.Count == 0)
                {
                    Debug.Log("No more upgrades");
                    break;
                }
                var newUpgrade = upgrades.Dequeue();
                currentUpgrades.Add(newUpgrade);
                newUpgrade.upgradeCompleted += OnUpgradeCompleted;
            }
            UpdateUI();
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
}