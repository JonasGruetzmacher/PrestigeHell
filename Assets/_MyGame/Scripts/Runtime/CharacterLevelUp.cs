using System.Collections;
using System.Collections.Generic;
using MoreMountains.TopDownEngine;
using UnityEngine;
using MoreMountains.Tools;

namespace LeroGames.PrestigeHell
    {
    public class CharacterLevelUp : CharacterAbility, MMEventListener<GameEvent>
    {
        protected override void HandleInput()
        {
            if (_inputManager as MyInputManager != null)
            {
                if ((_inputManager as MyInputManager).LevelUpgradesButton.State.CurrentState == MMInput.ButtonStates.ButtonDown)
                {
                    TriggerLevelUpgrades();
                }
            }
        }

        protected virtual void TriggerLevelUpgrades()
        {
            if (_condition.CurrentState == CharacterStates.CharacterConditions.Dead)
            {
                return;
            }

            if (!AbilityAuthorized)
            {
                return;
            }
            PlayAbilityStartFeedbacks();
            MMGameEvent.Trigger("ToggleLevelUpgrades");
        }

        protected virtual void UnlockAbility()
        {
            PermitAbility(true);
        }

        public virtual void OnMMEvent(GameEvent gameEvent)
        {
            if (gameEvent.eventName == Eventname.LevelUp)
            {
                if (!AbilityAuthorized)
                {
                    UnlockAbility();
                }
            }
        }

        protected override void OnEnable()
        {
            this.MMEventStartListening<GameEvent>();
        }

        protected override void OnDisable()
        {
            this.MMEventStopListening<GameEvent>();
        }

    }
}