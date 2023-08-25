using System.Collections;
using System.Collections.Generic;
using MoreMountains.TopDownEngine;
using UnityEngine;

public class ApplyPickableStats : MonoBehaviour
{
    [SerializeField] private Stats stat;

    private PickableItem _pickableItem;

    private void Start()
    {
        Initialization();
    }

    private void Initialization()
    {
        _pickableItem = GetComponent<PickableItem>();
        ApplyStats();
    }

    private void ApplyStats()
    {
        if (stat.IncludesStat(Stat.xPGain))
        {
            if (_pickableItem != null)
            {
                if(_pickableItem is XPDrop)
                {
                    XPDrop xpDrop = _pickableItem as XPDrop;
                    xpDrop.XPAmount = (int)stat.GetStat(Stat.xPGain);
                }
            }
        }
        if (stat.IncludesStat(Stat.collectRange))
        {
            if (_pickableItem != null)
            {
                _pickableItem.GetComponent<CircleCollider2D>().radius = stat.GetStat(Stat.collectRange);
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
        ApplyStats();
    }

    private void OnDisable()
    {
        stat.upgradeApplied -= OnUpgradeApplied;
    }
}
