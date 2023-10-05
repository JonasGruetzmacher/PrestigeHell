using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.TopDownEngine;

namespace LeroGames.PrestigeHell
{
    public class MyLevelSelector : LevelSelector
    {
        [SerializeField] private LeroGames.Tools.GameEvent onSave;

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
}