using System.Collections;
using System.Collections.Generic;
using MoreMountains.Tools;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;


public class LevelUpgrade : MonoBehaviour, MMEventListener<GameEvent>, IUpgrade, ITooltipInformation
{
    [field: SerializeField] public Upgrade upgrade {get; private set;}

    public LevelUpgradeState state { get; private set; }
    public UnityEvent upgradeStateChanged;

    [SerializeField] private int levelRequirement;
    
    [SerializeField] private bool alreadyGotUnlocked = false;

    public void SetButtonState(LevelUpgradeState state)
    {
        if (state == LevelUpgradeState.Unlocked && !alreadyGotUnlocked)
        {
            alreadyGotUnlocked = true;
        }
        this.state = state;
        upgradeStateChanged?.Invoke();
    }

    public void OnClick()
    {
        upgrade.DoUpgrade();
        SetButtonState(LevelUpgradeState.Selected);
    }

    private void OnUpgradeStateChanged(Upgrade upgrade)
    {
        CheckState();
    }
    
    private void CheckState()
    {
        if (upgrade.isUnlocked)
        {
            Debug.Log("Upgrade is unlocked");
            return;
        }
        if (upgrade.isBlocked)
        {
            Debug.Log("Upgrade is blocked");
            SetButtonState(LevelUpgradeState.Disabled);
            return;
        }
        if (ResourcesManager.Instance.GetResourceAmount(ResourceType.Level) >= levelRequirement)
        {
            Debug.Log("Level requirement met");
            SetButtonState(LevelUpgradeState.Unlocked);
            return;
        }
        Debug.Log("Level requirement not met");
        SetButtonState(LevelUpgradeState.Locked);

    }

    public void OnMMEvent(GameEvent eventType)
    {
        if (eventType.eventName == Eventname.LevelUp)
        {
            CheckState();
        }
    }

    public void GetTooltipInformation(out string infoLeft, out string infoRight)
    {
        upgrade.GetTooltipInformation(out infoLeft, out infoRight);
        infoRight += string.Format("Level {0}", levelRequirement);
    }

    private void OnEnable()
    {
        CheckState();
        upgrade.upgradeStateChanged += OnUpgradeStateChanged;
        // upgrade.upgradeReset += ResetUpgrade;
        this.MMEventStartListening<GameEvent>();
    }

    private void OnDisable()
    {
        upgrade.upgradeStateChanged -= OnUpgradeStateChanged;
        // upgrade.upgradeReset -= ResetUpgrade;
        this.MMEventStopListening<GameEvent>();
    }

}
