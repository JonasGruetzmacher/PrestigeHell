using System.Collections;
using System.Collections.Generic;
using MoreMountains.TopDownEngine;
using UnityEngine;

namespace LeroGames.PrestigeHell
{
    public class ApplyStatsToObject : MonoBehaviour
    {
        [SerializeField] private Stats stats;
        [SerializeField] private StatType healthStatType;

        protected virtual void Start()
        {
            Initialization();
        }

        protected virtual void Initialization()
        {
            ApplyStats();
            if (TryGetComponent(out Health health))
            {
                health.InitializeCurrentHealth();
            }
        }

        public virtual void ApplyStats()
        {
            if (stats == null)
                return;
            stats.Apply(gameObject);
        }

        protected virtual void OnUpgradeApplied(Stats stats, StatsUpgrade upgrade)
        {
            if (this.stats != stats)
                return;
            ApplyStats();
            if(upgrade is StatsUpgrade)
            {
                var statsUpgrade = upgrade as StatsUpgrade;
                if (statsUpgrade.upgradeToApply.ContainsKey(healthStatType))
                {
                    if (TryGetComponent(out Health health))
                    {
                        health.ReceiveHealth(statsUpgrade.upgradeToApply[healthStatType], this.gameObject);
                    }
                }
            }
        }

        protected virtual void OnEnable()
        {
            stats.upgradeApplied += OnUpgradeApplied;
            ApplyStats();
        }

        protected virtual void OnDisable()
        {
            stats.upgradeApplied -= OnUpgradeApplied;
        }


    }
}