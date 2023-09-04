using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class CUpgradeButton : CUIComponent
{
    public CThemeSO theme;
    public Style backgroundStyle;
    public UnityEvent onClick;

    public UpgradeButtonView view;

    private Button button;
    
    protected CText header;
    protected CText description;
    protected CText price;
    protected CText bought;


    public override void Setup()
    {
        button = GetComponentInChildren<Button>();

        header = view.containerLeftTop.GetComponentInChildren<CText>();
        description = view.containerLeftBottom.GetComponentInChildren<CText>();
        price = view.containerRightTop.GetComponentInChildren<CText>();
        bought = view.containerRightBottom.GetComponentInChildren<CText>();

        view.Setup();
        header.Setup();
        description.Setup();
        price.Setup();
        bought.Setup();


    }

    public override void Configure()
    {
        ColorBlock colorBlock = button.colors;
        colorBlock.normalColor = theme.GetBackgroundColor(backgroundStyle);
        button.colors = colorBlock;

        header.style = backgroundStyle;
        description.style = backgroundStyle;
        price.style = backgroundStyle;
        bought.style = backgroundStyle;

        header.Configure();
        description.Configure();
        price.Configure();
        bought.Configure();
        view.Configure();
    }
}
