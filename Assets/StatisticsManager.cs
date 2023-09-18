using System.Collections;
using System.Collections.Generic;
using MoreMountains.Tools;
using UnityEngine;

public class StatisticsManager : MMSingleton<StatisticsManager>, MMEventListener<StatisticEvent>
{
    public List<StatisticSO> statistics = new List<StatisticSO>();


    public void OnMMEvent(StatisticEvent statisticEvent)
    {
        foreach (var statistic in statistics)
        {
            if (statistic.statisticType == statisticEvent.type)
            {
                if (statistic.toggleAdditionalAttribute)
                {
                    if (statistic.additionalAttribute == statisticEvent.attribute)
                    {
                        statistic.InreaseValue(statisticEvent.value);
                    }
                }
                else
                {
                    statistic.InreaseValue(statisticEvent.value);
                }
            }
        }
    }

    private void OnApplicationQuit()
    {
        foreach (var statistic in statistics)
        {
            statistic.Reset();
        }
    }

    private void OnEnable()
    {
        this.MMEventStartListening<StatisticEvent>();
    }

    private void OnDisable()
    {
        this.MMEventStopListening<StatisticEvent>();
    }
}
