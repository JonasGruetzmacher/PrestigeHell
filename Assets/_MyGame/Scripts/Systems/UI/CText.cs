using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Sirenix.OdinInspector;


public class CText : CUIComponent
{
    public CTextSO textSO;
    public Style style;

    private TextMeshProUGUI textMeshProUGUI;

    public override void Setup()
    {
        textMeshProUGUI = GetComponentInChildren<TextMeshProUGUI>();
    }

    public override void Configure()
    {
        textMeshProUGUI.font = textSO.font;
        textMeshProUGUI.fontSize = textSO.fontSize;
        textMeshProUGUI.color = textSO.theme.GetTextColor(style);
    }
}
