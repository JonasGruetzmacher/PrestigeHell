using System.Collections;
using System.Collections.Generic;
using MoreMountains.Tools;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;


public class LevelUpgrade : MonoBehaviour, MMEventListener<GameEvent>, IUpgrade
{
    [field: SerializeField] public Upgrade upgrade {get; private set;}

    public LevelUpgradeState state { get; private set; }
    public UnityEvent upgradeStateChanged;

    [SerializeField] private int levelRequirement;
    
    private bool alreadyGotUnlocked = false;

    private void Start()
    {
        SetButtonState(LevelUpgradeState.Locked);
        CheckIfUnlocked();
    }

    public void SetButtonState(LevelUpgradeState state)
    {
        this.state = state;
        upgradeStateChanged?.Invoke();
    }

    public void OnClick()
    {
        upgrade.DoUpgrade();
        SetButtonState(LevelUpgradeState.Selected);
    }

    private void OnUpgradeStateChanged(Upgrade upgrade, bool unlock)
    {
        if (unlock)
        {
            SetButtonState(LevelUpgradeState.Unlocked);
        }
        else
        {
            SetButtonState(LevelUpgradeState.Disabled);
        }
    }

    private void ResetUpgrade(Upgrade upgrade)
    {
        SetButtonState(LevelUpgradeState.Locked);
    }
    
    private void CheckIfUnlocked()
    {
        if (ResourcesManager.Instance.GetResourceAmount(ResourceType.Level) >= levelRequirement)
        {
            if(!alreadyGotUnlocked)
                SetButtonState(LevelUpgradeState.Unlocked);
        }
    }

    public void OnMMEvent(GameEvent eventType)
    {
        if (eventType.eventName == Eventname.LevelUp)
        {
            CheckIfUnlocked();
        }
    }

    private void OnEnable()
    {
        upgrade.upgradeStateChanged += OnUpgradeStateChanged;
        upgrade.upgradeReset += ResetUpgrade;
        this.MMEventStartListening<GameEvent>();
    }

    private void OnDisable()
    {
        upgrade.upgradeStateChanged -= OnUpgradeStateChanged;
        upgrade.upgradeReset -= ResetUpgrade;
        this.MMEventStopListening<GameEvent>();
    }

}
