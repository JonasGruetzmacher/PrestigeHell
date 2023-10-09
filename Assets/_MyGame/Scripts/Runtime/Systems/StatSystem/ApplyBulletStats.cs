using System.Collections;
using System.Collections.Generic;
// using LeroGames.StatSystem;
using MoreMountains.TopDownEngine;
using UnityEngine;

namespace LeroGames.PrestigeHell
{
    public class ApplyBulletStats : MonoBehaviour
    {
        [SerializeField] private Stats stat;

        DamageOnTouch touchDamage;

        private void Start()
        {
            Initialization();
        }

        private void Initialization()
        {
            touchDamage = GetComponent<DamageOnTouch>();

            ApplyStats();
        }

        private void ApplyStats()
        {
            if (stat.IncludesStat(Stat.touchDamage))
            {
                if (touchDamage != null)
                {
                    touchDamage.MinDamageCaused = stat.GetStat(Stat.touchDamage);
                    touchDamage.MaxDamageCaused = stat.GetStat(Stat.touchDamage);
                }
            }
        }

        private void OnUpgradeApplied(Stats stats, StatsUpgrade upgrade)
        {
            if (stat != stats)
                return;
        }

        private void OnEnable()
        {
            stat.upgradeApplied += OnUpgradeApplied;
            ApplyStats();
        }

        private void OnDisable()
        {
            stat.upgradeApplied -= OnUpgradeApplied;
        }
    }
}