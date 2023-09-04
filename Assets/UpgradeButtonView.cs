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

    private GameObject containerLeft;
    private GameObject containerRight;

    private VerticalLayoutGroup verticalLayoutGroup;
    private HorizontalLayoutGroup horizontalLayoutGroup;

    public override void Setup()
    {
        verticalLayoutGroup = GetComponent<VerticalLayoutGroup>();
        horizontalLayoutGroup = GetComponent<HorizontalLayoutGroup>();
    }

    public override void Configure()
    {
        
    }
}
