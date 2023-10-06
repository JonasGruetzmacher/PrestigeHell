using System.Collections;
using System.Collections.Generic;
// using LeroGames.StatSystem;
using MoreMountains.Tools;
using MoreMountains.TopDownEngine;
using UnityEngine;

namespace LeroGames.PrestigeHell
{
    public class CharacterStats : CharacterAbility
    {
        public Stats stats;

        private Dictionary<Stat, float> localStats = new Dictionary<Stat, float>();

        protected DamageOnTouch touchDamage;

        protected override void OnEnable()
        {
            base.OnEnable();
            if (gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                stats.upgradeApplied += OnUpgradeApplied;
            }
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            stats.upgradeApplied -= OnUpgradeApplied;
        }

        protected  override void Initialization()
        {
            base.Initialization();

            touchDamage = GetComponent<DamageOnTouch>();

            localStats = new Dictionary<Stat, float>(stats.instanceStats);

            
            ApplyStats();
            _health.InitializeCurrentHealth();

        }

        public void InitWithStats()
        {   
            Initialization();
        }

        private void ApplyStats()
        {
            if (stats == null)
                return;
            if (_characterMovement != null && stats.IncludesStat(Stat.speed))
            {
                _characterMovement.WalkSpeed = stats.GetStat(Stat.speed);
                _characterMovement.ResetSpeed();
            }
            if (_health != null && stats.IncludesStat(Stat.health))
            {
                float health = stats.GetStat(Stat.health);
                if (stats.IncludesStat(Stat.healthVariance))
                {
                    float healthVariance = Random.Range(-stats.GetStat(Stat.healthVariance), stats.GetStat(Stat.healthVariance));
                    health = health * (1 + healthVariance);
                }
                _health.MaximumHealth = health;

                _health.InitialHealth = health;
                _health.UpdateHealthBar(true);
            }
            if (stats.IncludesStat(Stat.touchDamage))
            {
                if(touchDamage != null)
                {
                    touchDamage.MinDamageCaused = stats.GetStat(Stat.touchDamage);
                    touchDamage.MaxDamageCaused = stats.GetStat(Stat.touchDamage);
                }
            }
        }

        private void OnUpgradeApplied(Stats stat, Upgrade upgrade)
        {
            if (stat != stats)
                return;
            if (gameObject.tag != "Player")
                return;
            
            ApplyStats();

            if(upgrade is StatsUpgrade)
            {
                var statsUpgrade = upgrade as StatsUpgrade;
                if (statsUpgrade.upgradeToApply.ContainsKey(Stat.health))
                    _health.ReceiveHealth(statsUpgrade.upgradeToApply[Stat.health], this.gameObject);
            }
            
        }
    }
}