using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.TopDownEngine;

public class MyLevelSelector : LevelSelector
{
    [SerializeField] private CustomEventSystem.GameEvent onSave;

    protected override void LoadScene(string newSceneName)
    {
        onSave?.Raise(this, LevelName);
        base.LoadScene(newSceneName);
    }

    public override void RestartLevel()
    {
        onSave?.Raise(this, LevelName);
        base.RestartLevel();
    }

    public override void ReloadLevel()
    {
        onSave?.Raise(this, LevelName);
        base.ReloadLevel();
    }

}
