using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;

public class CustomToggle : CUIComponent
{
    public ImageToggleSO toggleSO;

    public Toggle toggle;
    public Image backgroundImage;
    public Image highlightImage;
    public CText text;

    public Sprite sprite;

    private RectTransform backgroundRectTransform;

    public override void Setup()
    {
        backgroundRectTransform = backgroundImage.GetComponent<RectTransform>();
    }

    public override void Configure()
    {
        highlightImage.sprite = sprite;
        backgroundImage.sprite = sprite;

        highlightImage.color = toggleSO.onColor;

    }

    public void SetSprite(Sprite sprite)
    {
        this.sprite = sprite;
        Configure();
    }

    [Button("Toggle")]
    public void Toggle()
    {
        toggle.isOn = !toggle.isOn;
    }

    [Button("SetText")]
    public void SetText(string text)
    {
        this.text.SetText(text);
    }

    public void SetToggle(bool value)
    {
        toggle.isOn = value;
    }

    public bool IsOn()
    {
        return toggle.isOn;
    }
}
