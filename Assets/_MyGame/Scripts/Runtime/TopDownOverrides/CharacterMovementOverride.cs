using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.TopDownEngine;
using MoreMountains.Tools;
using LeroGames.Tools;

namespace LeroGames.PrestigeHell
{
    public class CharacterMovementOverride : CharacterMovement
    {
        [MMInspectorGroup("Overrides", true, 10)]
        public FloatVariable movementSpeed;

        protected override void HandleMovement()
        {
            MovementSpeed = movementSpeed.Value;
            base.HandleMovement();
        }


    }
}