using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;

namespace LeroGames.PrestigeHell
{
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

            imageTop.color = GetThemeSO().primary_bg;
            imageCenter.color = GetThemeSO().secondary_bg;
            imageBottom.color = GetThemeSO().tertiary_bg;
        }
    }
}