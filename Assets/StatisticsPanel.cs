using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatisticsPanel : MonoBehaviour
{
    public Transform contentParent;
    public GameObject contentViewPrefab;
    private Dictionary<StatisticSO, ContentViewStatistic> statisticContentViews = new Dictionary<StatisticSO, ContentViewStatistic>();

    private void OnEnable()
    {
        
        foreach (var statistic in StatisticsManager.Instance.statistics)
        {
            if (statisticContentViews.ContainsKey(statistic))
            {
                continue;
            }
            Debug.Log("StatisticsPanel OnEnable");
            var contentView = Instantiate(contentViewPrefab, contentParent).GetComponent<ContentViewStatistic>();
            contentView.statisticSO = statistic;
            contentView.Init();
            statisticContentViews.Add(statistic, contentView);
        }
    }
}
