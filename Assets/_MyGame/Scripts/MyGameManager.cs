using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.TopDownEngine;
using MoreMountains.Tools;

public class MyGameManager : GameManager
{
    public bool levelUpPanelActive = false;

    public override void Pause(PauseMethods method = PauseMethods.PauseMenu, bool pause = true)
    {
        if (levelUpPanelActive)
        {
            return;
        }
        base.Pause(method, pause);
        
    }

    public virtual void OpenLevelUpgrades()
    {
        if (levelUpPanelActive || _pauseMenuOpen)
        { 
            return; 
        }

        Pause(PauseMethods.NoPauseMenu, false);
        if (GUIManager.HasInstance)
        {
            (GUIManager.Instance  as MyGUIManager).SetLevelUpPanel(true);
            levelUpPanelActive = true;
        }
    }

    public virtual void CloseLevelUpgrades()
    {
        UnPause(PauseMethods.NoPauseMenu);
        if (GUIManager.HasInstance)
        {
            (GUIManager.Instance as MyGUIManager).SetLevelUpPanel(false);
            levelUpPanelActive = false;
        }
    }

    public override void OnMMEvent(MMGameEvent gameEvent)
    {
        base.OnMMEvent(gameEvent);
        if (gameEvent.EventName == "ToggleLevelUpgrades")
        {
            if (levelUpPanelActive) 
            {
                MMGameEvent.Trigger("LevelUpgradesClosed");
            }
            else
            {
                MMGameEvent.Trigger("LevelUpgradesOpened");
            }
        }
        if (gameEvent.EventName == "LevelUpgradesOpened")
        {
            OpenLevelUpgrades();
        }

        if (gameEvent.EventName == "LevelUpgradesClosed")
        {
            CloseLevelUpgrades();
        }
    }
}
