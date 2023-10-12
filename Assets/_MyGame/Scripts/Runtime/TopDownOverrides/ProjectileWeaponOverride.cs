using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.TopDownEngine;
using LeroGames.Tools;
using MoreMountains.Tools;

namespace LeroGames.PrestigeHell
{
    public class ProjectileWeaponOverride : ProjectileWeapon
    {
        [MMInspectorGroup("Overrides", true, 10)]
        public FloatVariable attackSpeed;

        protected override void Update()
        {
            if (attackSpeed == null)
            {
                return;
            }
            TimeBetweenUses = 1f/attackSpeed.Value;
            base.Update();
        }
    }
}    