using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ContentViewStatistic : ContentView
{
    public StatisticSO statisticSO;

    private TextMeshProUGUI leftText;
    private TextMeshProUGUI rightText;

    public override void Setup()
    {
        base.Setup();

        leftText = containerLeft.GetComponentInChildren<TextMeshProUGUI>();
        rightText = containerRight.GetComponentInChildren<TextMeshProUGUI>();
    }

    public override void Configure()
    {
        base.Configure();

        if (statisticSO == null)
        {
            return;
        }
        leftText.text = statisticSO.statisticName;
        rightText.text = statisticSO.value.ToString();
    }

    private void OnEnable()
    {
        Configure();
    }
}
