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
        private VerticalLayoutGroup verticalLayoutGroup;

        public override void Setup()
        {
            imageLeft = containerLeft.GetComponent<Image>();
            imageRight = containerRight.GetComponent<Image>();

            horizontalLayoutGroup = GetComponent<HorizontalLayoutGroup>();
            verticalLayoutGroup = GetComponent<VerticalLayoutGroup>();
        }

        public override void Configure()
        {
            if (horizontalLayoutGroup != null)
            {
                horizontalLayoutGroup.padding = viewSO.padding;
                horizontalLayoutGroup.spacing = viewSO.spacing;
            }
            else if (verticalLayoutGroup != null)
            {
                verticalLayoutGroup.padding = viewSO.padding;
                verticalLayoutGroup.spacing = viewSO.spacing;
            }
            
            if(imageLeft != null) imageLeft.color = GetThemeSO().secondary_bg;
            if(imageRight != null) imageRight.color = GetThemeSO().secondary_bg;
        }
    }
}