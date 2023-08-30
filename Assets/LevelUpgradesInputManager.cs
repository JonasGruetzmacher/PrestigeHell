using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using MoreMountains.Tools;
using MoreMountains.TopDownEngine;
using UnityEngine;

public class LevelUpgradesInputManager : MonoBehaviour
{
    public CanvasGroup levelUpgradesCanvasGroup;
    protected CanvasGroup _canvasGroup;
    public CanvasGroup Overlay;
    public float OverlayActiveOpacity = 0.85f;
    public float OverlayInactiveOpacity = 0f;

    [MMReadOnly]
    public bool LevelUpgradesIsOpen;

    protected virtual void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    protected virtual void Start()
    {
        LevelUpgradesIsOpen = false;
    }

    protected virtual void Update()
    {
        HandleInput();
    }

    protected virtual void HandleInput()
    {  
        if ((InputManager.Instance as MyInputManager).LevelUpgradesButton.State.CurrentState == MMInput.ButtonStates.ButtonUp && Application.isFocused)
        {
            ToggleLevelUpgrades();
        }
    }
    public virtual void ToggleLevelUpgrades()
    {
        if (LevelUpgradesIsOpen)
        {
            CloseLevelUpgrades();
        }
        else
        {
            OpenLevelUpgrades();
        }
    }

    public virtual void OpenLevelUpgrades()
    {
        if (_canvasGroup != null)
			{
				_canvasGroup.blocksRaycasts = true;
			}

        // we open our inventory
        // MMInventoryEvent.Trigger(MMInventoryEventType.InventoryOpens, null, TargetInventoryDisplay.TargetInventoryName, TargetInventoryDisplay.TargetInventory.Content[0], 0, 0, TargetInventoryDisplay.PlayerID);
        MMGameEvent.Trigger("LevelUpgradesOpen");
        LevelUpgradesIsOpen = true;

        StartCoroutine(MMFade.FadeCanvasGroup(levelUpgradesCanvasGroup, 0.2f, 1f));
        StartCoroutine(MMFade.FadeCanvasGroup(Overlay, 0.2f, OverlayActiveOpacity));
    }

    public virtual void CloseLevelUpgrades()
		{
			if (_canvasGroup != null)
			{
				_canvasGroup.blocksRaycasts = false;
			}
			// we close our inventory
			// MMInventoryEvent.Trigger(MMInventoryEventType.InventoryCloses, null, TargetInventoryDisplay.TargetInventoryName, null, 0, 0, TargetInventoryDisplay.PlayerID);
			MMGameEvent.Trigger("LevelUpgradesClose");
			LevelUpgradesIsOpen = false;

			StartCoroutine(MMFade.FadeCanvasGroup(levelUpgradesCanvasGroup, 0.2f, 0f));
			StartCoroutine(MMFade.FadeCanvasGroup(Overlay, 0.2f, OverlayInactiveOpacity));
		}
}
