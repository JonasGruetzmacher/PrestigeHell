using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;



public class CView : CUIComponent
{
    public CViewSO viewSO;

    public GameObject containerTop;
    public GameObject containerCenter;
    public GameObject containerBottom;

    private Image imageTop;
    private Image imageCenter;
    private Image imageBottom;

    private VerticalLayoutGroup verticalLayoutGroup;

    public override void Setup()
    {
        imageTop = containerTop.GetComponent<Image>();
        imageCenter = containerCenter.GetComponent<Image>();
        imageBottom = containerBottom.GetComponent<Image>();

        verticalLayoutGroup = GetComponent<VerticalLayoutGroup>();
    }

    public override void Configure()
    {
        verticalLayoutGroup.padding = viewSO.padding;
        verticalLayoutGroup.spacing = viewSO.spacing;

        imageTop.color = viewSO.themeSO.primary_bg;
        imageCenter.color = viewSO.themeSO.secondary_bg;
        imageBottom.color = viewSO.themeSO.tertiary_bg;
    }
}
