using System.Collections;
using System.Collections.Generic;
using MoreMountains.Tools;
using UnityEngine;
using UnityEngine.UI;


public class LevelUpgrade : MonoBehaviour, MMEventListener<GameEvent>, IUpgrade
{
    [field: SerializeField] public Upgrade upgrade {get; private set;}


    [SerializeField] private GameObject upgradeButton;

    [SerializeField] private int levelRequirement;
    
    private bool alreadyGotUnlocked = false;

    private void Start()
    {
        SetButtonState(LevelUpgradeState.Locked);
        CheckIfUnlocked();
    }

    public void SetButtonState(LevelUpgradeState state)
    {
        switch (state)
        {
            case LevelUpgradeState.Locked:
                upgradeButton.SetActive(false);
                break;
            case LevelUpgradeState.Unlocked:
                upgradeButton.SetActive(true);
                break;
            case LevelUpgradeState.Selected:
                upgradeButton.SetActive(true);
                upgradeButton.GetComponentInChildren<MMTouchButton>().DisabledColor = Color.green;
                upgradeButton.GetComponentInChildren<MMTouchButton>().DisableButton();
                break;
            case LevelUpgradeState.Disabled:
                upgradeButton.SetActive(true);
                upgradeButton.GetComponentInChildren<MMTouchButton>().DisableButton();
                break;
        }
    }

    public int GetLevelRequirement()
    {
        return levelRequirement;
    }

    public void OnClick()
    {
        upgrade.DoUpgrade();
        SetButtonState(LevelUpgradeState.Selected);
    }

    private void OnUpgradeStateChanged(Upgrade upgrade, bool unlock)
    {
        Debug.Log("Upgrade state changed");
        Debug.Log(this.upgrade.upgradeName + " " + upgrade.upgradeName + " " + unlock);
        if (unlock)
        {
            SetButtonState(LevelUpgradeState.Unlocked);
        }
        else
        {
            SetButtonState(LevelUpgradeState.Disabled);
        }
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
        this.MMEventStartListening<GameEvent>();
    }

    private void OnDisable()
    {
        upgrade.upgradeStateChanged -= OnUpgradeStateChanged;
        this.MMEventStopListening<GameEvent>();
    }

}
