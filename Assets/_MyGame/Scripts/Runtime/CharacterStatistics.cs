using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Tools;
using MoreMountains.TopDownEngine;

namespace LeroGames.PrestigeHell
{
    public class CharacterStatistics : CharacterAbility, MMEventListener<GameEvent>
    {
        protected override void HandleInput()
        {
            if (_inputManager as MyInputManager != null)
            {
                if ((_inputManager as MyInputManager).StatisticsButton.State.CurrentState == MMInput.ButtonStates.ButtonDown)
                {
                    TriggerStatistics();
                }
            }
        }

        protected virtual void TriggerStatistics()
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
            MMGameEvent.Trigger("ToggleStatistics");
        }

        protected virtual void UnlockAbility()
        {
            PermitAbility(true);
        }

        public virtual void OnMMEvent(GameEvent gameEvent)
        {
            
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