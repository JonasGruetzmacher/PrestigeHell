using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Sirenix.OdinInspector;

namespace LeroGames.PrestigeHell
{
    public class CText : CUIComponent
    {
        public CTextSO textSO;
        public Style style;
        public bool autoSize = false;

        private TextMeshProUGUI textMeshProUGUI;

        public override void Setup()
        {
            textMeshProUGUI = GetComponentInChildren<TextMeshProUGUI>();
        }

        public override void Configure()
        {
            CThemeSO theme = GetThemeSO();
            if (theme == null) { return; }

            textMeshProUGUI.font = textSO.font;
            textMeshProUGUI.fontSize = textSO.fontSize;
            textMeshProUGUI.color = theme.GetTextColor(style);
            textMeshProUGUI.enableAutoSizing = autoSize;
            textMeshProUGUI.fontSizeMin = 5;
        }

        [Button("SetText")]
        public void SetText(string text)
        {
            textMeshProUGUI.text = text;
        }
    }
}