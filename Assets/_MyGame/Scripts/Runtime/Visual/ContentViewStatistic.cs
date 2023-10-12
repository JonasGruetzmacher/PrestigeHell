using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using MoreMountains.Tools;

namespace LeroGames.PrestigeHell
{
    public class ContentViewStatistic : ContentView
    {
        public StatisticSO statisticSO;

        private TextMeshProUGUI leftText;
        private TextMeshProUGUI rightText;
        private MMProgressBar progressBar;

        public override void Setup()
        {
            base.Setup();

            leftText = containerLeft.GetComponentInChildren<TextMeshProUGUI>();
            rightText = containerRight.GetComponentInChildren<TextMeshProUGUI>();
            progressBar = containerRight.GetComponentInChildren<MMProgressBar>();
        }

        public override void Configure()
        {
            base.Configure();

            if (statisticSO == null)
            {
                return;
            }
            leftText.text = statisticSO.statisticName;
            progressBar.SetBar(statisticSO.value, 0f, statisticSO.GetNextGoalValue());
            rightText.text = statisticSO.GetTextDescription();
        }

        private void OnEnable()
        {
            Setup();
        }
    }
}