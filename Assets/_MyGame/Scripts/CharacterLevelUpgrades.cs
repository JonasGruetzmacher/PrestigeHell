using System.Collections;
using System.Collections.Generic;
using MoreMountains.TopDownEngine;
using UnityEngine;
using MoreMountains.Tools;

public class CharacterLevelUpgrades : MonoBehaviour
{
    [MMReadOnly]
	public bool InventoryIsOpen;

    // protected override void HandleInput()
    // {
    //     if (!AbilityAuthorized)
    //     {
    //         return;
    //     }
    //     if (_inputManager.SwitchCharacterButton.State.CurrentState == MMInput.ButtonStates.ButtonDown)
    //     {
    //         ToggleUpgrades();
    //     }
    // }

    // private void ToggleUpgrades()
    // {
    //     if (UpgradesIsOpen)
    //     {
    //         CloseUpgrades();
    //     }
    //     else
    //     {
    //         OpenUpgrades();
    //     }
    // }
}
