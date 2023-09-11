using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.TopDownEngine;
using MoreMountains.Tools;

public class MyGameManager : GameManager
{
    protected override void Awake()
    {
        base.Awake();
    }

    public override void OnMMEvent(MMGameEvent gameEvent)
    {
        base.OnMMEvent(gameEvent);
        switch (gameEvent.EventName)
        {
            case "LevelUpgradesOpen":
                Pause(PauseMethods.NoPauseMenu, false);
                break;
            case "LevelUpgradesClose":
                UnPause(PauseMethods.NoPauseMenu);
                break;
        }
    }
}
