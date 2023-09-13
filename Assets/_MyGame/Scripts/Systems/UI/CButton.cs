using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Events;

public class CButton : CUIComponent
{
    public CThemeSO theme;
    public Style style;
    public UnityEvent onClick;

    private Button button;
    protected TextMeshProUGUI buttonText;

    public override void Setup()
    {
        button = GetComponentInChildren<Button>();
        buttonText = GetComponentInChildren<TextMeshProUGUI>();
    }

    public override void Configure()
    {
        ColorBlock colorBlock = button.colors;
        colorBlock.normalColor = theme.GetBackgroundColor(style);
        button.colors = colorBlock;

        buttonText.color = theme.GetTextColor(style);
    }

    public virtual void Dissable()
    {
        button.interactable = false;
    }

    public virtual void Enable()
    {
        button.interactable = true;
    }

    public virtual void Selected()
    {
        button.interactable = false;
    }

    public virtual void OnClick()
    {
        onClick.Invoke();
    }
    
}
