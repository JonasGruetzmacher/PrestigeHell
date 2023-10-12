using System.Collections;
using System.Collections.Generic;
using LeroGames.Tools;
using MoreMountains.TopDownEngine;
using MoreMountains.Tools;
using UnityEngine;

namespace LeroGames.PrestigeHell
{
    public class DamageOnTouchOverride : DamageOnTouch
    {
        [MMInspectorGroup("Overrides", true, 10)]
        public FloatVariable damageOnTouch;

        protected override void Update()
        {
            if (damageOnTouch == null)
            {
                return;
            }
            MinDamageCaused = damageOnTouch.Value;
            MaxDamageCaused = damageOnTouch.Value;
            base.Update();
        }
    }
}