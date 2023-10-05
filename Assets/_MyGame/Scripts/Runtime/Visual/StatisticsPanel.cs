using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace LeroGames.PrestigeHell
{
    public class StatisticsPanel : MonoBehaviour
    {
        public Transform contentParent;
        public GameObject contentViewPrefab;
        private Dictionary<StatisticSO, ContentViewStatistic> statisticContentViews = new Dictionary<StatisticSO, ContentViewStatistic>();

        private void OnEnable()
        {
            List <StatisticSO> statistics = new List<StatisticSO>(StatisticsManager.Instance.statistics);
            statistics = statistics.Reverse<StatisticSO>().ToList();

            foreach (var statistic in statistics)
            {
                if (statisticContentViews.ContainsKey(statistic))
                {
                    continue;
                }
                if (statistic.value == 0)
                {
                    continue;
                }
                var contentView = Instantiate(contentViewPrefab, contentParent).GetComponent<ContentViewStatistic>();
                contentView.statisticSO = statistic;
                contentView.Init();
                statisticContentViews.Add(statistic, contentView);
            }
        }
    }
}