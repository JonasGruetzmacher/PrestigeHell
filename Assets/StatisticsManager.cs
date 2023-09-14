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
                        statistic.value += statisticEvent.value;
                    }
                }
                else
                {
                    statistic.value += statisticEvent.value;
                }
            }
        }
    }

    private void OnApplicationQuit()
    {
        foreach (var statistic in statistics)
        {
            statistic.value = 0;
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
