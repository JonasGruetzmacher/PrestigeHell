using System.Collections;
using System.Collections.Generic;
using MoreMountains.Tools;
using MoreMountains.TopDownEngine;
using UnityEngine;

public class CharacterStats : CharacterAbility, MMEventListener<GameEvent>
{
    [SerializeField] private Stats stats;

    private Dictionary<Stat, float> localStats = new Dictionary<Stat, float>();

    protected DamageOnTouch touchDamage;

    protected override void OnEnable()
    {
        base.OnEnable();
        stats.upgradeApplied += OnUpgradeApplied;
        this.MMEventStartListening<GameEvent>();
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        stats.upgradeApplied -= OnUpgradeApplied;
        this.MMEventStopListening<GameEvent>();
    }

    protected  override void Initialization()
    {
        base.Initialization();

        touchDamage = GetComponent<DamageOnTouch>();

        localStats = new Dictionary<Stat, float>(stats.instanceStats);

        
        ApplyStats();
        _health.InitializeCurrentHealth();

    }

    private void ApplyStats()
    {
        if (stats.IncludesStat(Stat.speed))
        {
            _characterMovement.WalkSpeed = stats.GetStat(Stat.speed);
            _characterMovement.ResetSpeed();
        }
        if (stats.IncludesStat(Stat.health))
        {
            _health.MaximumHealth = stats.GetStat(Stat.health);
            _health.InitialHealth = stats.GetStat(Stat.health);
            _health.UpdateHealthBar(true);
        }
        if (stats.IncludesStat(Stat.touchDamage))
        {
            if(touchDamage != null)
            {
                touchDamage.MinDamageCaused = stats.GetStat(Stat.touchDamage);
                touchDamage.MaxDamageCaused = stats.GetStat(Stat.touchDamage);
            }
            else
            {
                Debug.LogError("No touch damage component found");
            }
        }
    }

    private void OnUpgradeApplied(Stats stat, Upgrade upgrade)
    {
        if (stat != stats)
            return;
        ApplyStats();

        if(upgrade is StatsUpgrade)
        {
            var statsUpgrade = upgrade as StatsUpgrade;
            if (statsUpgrade.upgradeToApply.ContainsKey(Stat.health))
                _health.ReceiveHealth(statsUpgrade.upgradeToApply[Stat.health], this.gameObject);
        }
        
    }

    public void OnMMEvent(GameEvent eventType)
    {
        switch (eventType.eventName)
        {
            case Eventname.DangerChanged:
                ApplyStats();
                break;
            default:
                break;
        }
    }
}
