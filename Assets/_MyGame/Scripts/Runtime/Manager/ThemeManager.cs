using System.Collections;
using System.Collections.Generic;
using MoreMountains.Tools;
using Sirenix.OdinInspector;
using UnityEngine;

namespace LeroGames.PrestigeHell
{
    [DefaultExecutionOrder(-1)]
    public class ThemeManager : MMSingleton<ThemeManager>
    {
        [SerializeField] private CThemeSO mainTheme;
        [SerializeField] public MMSerializableDictionary<string, CThemeSO> themes;
        [SerializeField] public MMSerializableDictionary<string, CTextSO> textThemes;

        protected override void Awake()
        {
            base.Awake();
        }
        public CThemeSO GetMainTheme()
        {
            return mainTheme;
        }

        public CTextSO GetMainTextTheme()
        {
            return textThemes["MainTheme"];
        }
    }
}