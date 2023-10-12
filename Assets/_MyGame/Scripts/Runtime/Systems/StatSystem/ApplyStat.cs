using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.TopDownEngine;
using LeroGames.Tools;

namespace LeroGames.PrestigeHell
{
    public static class ApplyStat 
    {
        public static void Apply(this Stats stats, GameObject objectToApplyTo)
        {

            foreach (var stat in stats.stats)
            {
                // stat.Key.Apply(stats.GetStat(stat.Key), objectToApplyTo, stats);
            }
        }

        public static void Apply(this FloatVariable stat, float value, GameObject objectToApplyTo, Stats stats = null)
        {
            // switch (stat)
            // {
            //     case Stat.speed:
            //         ApplySpeed(value, objectToApplyTo, stats);
            //         break;
            //     case Stat.health:
            //         ApplyHealth(value, objectToApplyTo, stats);
            //         break;
            //     case Stat.touchDamage:
            //         ApplyTouchDamage(value, objectToApplyTo, stats);
            //         break;
            //     case Stat.collectRange:
            //         ApplyCollectRange(value, objectToApplyTo, stats);
            //         break;
            //     case Stat.XPGain:
            //         ApplyXPGain(value, objectToApplyTo, stats);
            //         break;
            //     case Stat.attackSpeed:
            //         ApplyAttackSpeed(value, objectToApplyTo, stats);
            //         break;
            // }
        }

        private static void ApplyAttackSpeed(float value, GameObject objectToApplyTo, Stats stats)
        {
            objectToApplyTo.TryGetComponent(out ProjectileWeapon weapon);
            if (weapon != null)
            {
                weapon.TimeBetweenUses = 1f/value;
            }
        }

        private static void ApplyXPGain(float value, GameObject objectToApplyTo, Stats stats)
        {
            objectToApplyTo.TryGetComponent(out XPDrop xpDrop);
            if (xpDrop != null)
            {
                xpDrop.XPAmount = (int)value;
            }
        }


        private static void ApplySpeed(float value, GameObject objectToApplyTo, Stats stats = null)
        {
            objectToApplyTo.TryGetComponent(out CharacterMovement characterMovement);
            if (characterMovement != null)
            {
                characterMovement.WalkSpeed = value;
                characterMovement.ResetSpeed();
            }
        }

        private static void ApplyHealth(float value, GameObject objectToApplyTo, Stats stats = null)
        {
            objectToApplyTo.TryGetComponent(out Health health);
            if (health == null)
            {
                Debug.Log("Trying to apply health to object without health component");
                return;
            }
            // float variance = Random.Range(-stats.GetStat(Stat.healthVariance), stats.GetStat(Stat.healthVariance));
            // float healthValue = value * (1 + variance);

            // health.MaximumHealth = healthValue;
            // health.InitialHealth = healthValue;
            health.UpdateHealthBar(true);
        }

        private static void ApplyTouchDamage(float value, GameObject objectToApplyTo, Stats stats = null)
        {
            objectToApplyTo.TryGetComponent(out DamageOnTouch damageOnTouch);
            if (damageOnTouch == null)
            {
                Debug.Log("Trying to apply touch damage to object without damage on touch component");
                return;
            }
            damageOnTouch.MinDamageCaused = value;
            damageOnTouch.MaxDamageCaused = value;
        }

        private static void ApplyCollectRange(float value, GameObject objectToApplyTo, Stats stats = null)
        {
            objectToApplyTo.TryGetComponent(out Collider2D collider);
            if (collider == null)
            {
                Debug.Log("Trying to apply collect range to object without collider");
                return;
            }
            if (collider is CircleCollider2D circleCollider)
            {
                circleCollider.radius = value;
            }
            else if (collider is BoxCollider2D boxCollider)
            {
                boxCollider.size = new Vector2(value, value);
            }
        }
    }
}