using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeButtonView : CUIComponent
{
    public CViewSO viewSO;
    

    public GameObject containerLeftTop;
    public GameObject containerRightTop;
    public GameObject containerLeftBottom;
    public GameObject containerRightBottom;

    public GameObject containerLeft;
    public GameObject containerRight;

    private VerticalLayoutGroup verticalLayoutGroupLeft;
    private VerticalLayoutGroup verticalLayoutGroupRight;
    private HorizontalLayoutGroup horizontalLayoutGroup;

    public override void Setup()
    {
        horizontalLayoutGroup = GetComponent<HorizontalLayoutGroup>();
        verticalLayoutGroupLeft = containerLeft.GetComponent<VerticalLayoutGroup>();
        verticalLayoutGroupRight = containerRight.GetComponent<VerticalLayoutGroup>();
    }

    public override void Configure()
    {
        horizontalLayoutGroup.padding = viewSO.padding;
        horizontalLayoutGroup.spacing = viewSO.spacing;
    }
}
