using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.TopDownEngine;
using LeroGames.Tools;
using MoreMountains.Tools;

namespace LeroGames.PrestigeHell
{
    public class HealthOverride : Health
    {
        [MMInspectorGroup("Overrides", true, 10)]
        public  FloatVariable initialHealth;
        public  FloatVariable healthVariance;

        public override void InitializeCurrentHealth()
        {
            if (initialHealth == null)
            {
                
                return;
            }
            if (healthVariance != null)
            {
                InitialHealth = initialHealth.Value * (1 + Random.Range(-healthVariance.Value, healthVariance.Value));
                MaximumHealth = InitialHealth;
            }
            else
            {
                InitialHealth = initialHealth.Value;
                MaximumHealth = InitialHealth;
            }
            base.InitializeCurrentHealth();
        }

        public override void ResetHealthToMaxHealth()
        {
            InitializeCurrentHealth();
            base.ResetHealthToMaxHealth();
            
        }
    }
}