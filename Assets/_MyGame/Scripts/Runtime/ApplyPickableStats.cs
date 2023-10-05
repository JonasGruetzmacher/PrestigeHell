using System.Collections;
using System.Collections.Generic;
// using LeroGames.StatSystem;
using MoreMountains.TopDownEngine;
using UnityEngine;

namespace LeroGames.PrestigeHell
    {
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
            if (stat == null)
                return;
            ApplyStats();
        }

        private void ApplyStats()
        {
            if (stat.IncludesStat(Stat.XPGain))
            {
                if (_pickableItem != null)
                {
                    if(_pickableItem is XPDrop)
                    {
                        XPDrop xpDrop = _pickableItem as XPDrop;
                        xpDrop.XPAmount = (int)stat.GetStat(Stat.XPGain);
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
            if (stat == null)
                return;
            stat.upgradeApplied += OnUpgradeApplied;
            ApplyStats();
        }

        private void OnDisable()
        {
            if (stat == null)
                return;
            stat.upgradeApplied -= OnUpgradeApplied;
        }
    }
}