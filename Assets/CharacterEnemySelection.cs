using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Tools;
using MoreMountains.TopDownEngine;

public class CharacterEnemySelection : CharacterAbility, MMEventListener<GameEvent>
{
    protected override void HandleInput()
    {
        if (_inputManager as MyInputManager != null)
        {
            if ((_inputManager as MyInputManager).EnemySelectionButton.State.CurrentState == MMInput.ButtonStates.ButtonDown)
            {
                TriggerEnemySelection();
            }
        }
    }

    protected virtual void TriggerEnemySelection()
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
        MMGameEvent.Trigger("ToggleEnemySelection");
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

