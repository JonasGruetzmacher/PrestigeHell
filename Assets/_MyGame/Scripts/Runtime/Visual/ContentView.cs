using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace LeroGames.PrestigeHell
{
    public class ContentView : CUIComponent
    {
        public CViewSO viewSO;

        public GameObject containerLeft;
        public GameObject containerRight;

        private Image imageLeft;
        private Image imageRight;

        private HorizontalLayoutGroup horizontalLayoutGroup;

        public override void Setup()
        {
            imageLeft = containerLeft.GetComponent<Image>();
            imageRight = containerRight.GetComponent<Image>();

            horizontalLayoutGroup = GetComponent<HorizontalLayoutGroup>();
        }

        public override void Configure()
        {
            horizontalLayoutGroup.padding = viewSO.padding;
            horizontalLayoutGroup.spacing = viewSO.spacing;

            imageLeft.color = viewSO.themeSO.secondary_bg;
            imageRight.color = viewSO.themeSO.secondary_bg;
        }
    }
}