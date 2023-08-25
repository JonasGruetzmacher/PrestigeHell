using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.TopDownEngine;


public class ApplyWeaponStats : MonoBehaviour
{
    [SerializeField] private Stats stat;

    private Weapon _weapon;

    private void Start()
    {
        Initialization();
    }

    private void Initialization()
    {
        _weapon = GetComponent<Weapon>();

        ApplyStats();
    }

    private void ApplyStats()
    {
        if(stat.IncludesStat(Stat.attackSpeed))
        {
            if(_weapon != null)
            {
                float timeBetweenShots = 1f / stat.GetStat(Stat.attackSpeed);
                _weapon.TimeBetweenUses = timeBetweenShots;
            }
            else
            {
                Debug.LogWarning("No weapon component found on " + gameObject.name);
            }
        }
    }

    private void OnUpgradeApplied(Stats stats, StatsUpgrade upgrade)
    {
        if (stat != stats)
            return;
        ApplyStats();
    }

    private void OnEnable()
    {
        stat.upgradeApplied += OnUpgradeApplied;
    }

    private void OnDisable()
    {
        stat.upgradeApplied -= OnUpgradeApplied;
    }
}
