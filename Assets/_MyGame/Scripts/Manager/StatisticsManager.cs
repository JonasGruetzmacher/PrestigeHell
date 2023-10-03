using System.Collections;
using System.Collections.Generic;
using MoreMountains.Tools;
using UnityEngine;
using System.Linq;

public class StatisticsManager : MMSingleton<StatisticsManager>, IDataPersistence
{
    public List<StatisticSO> statistics = new List<StatisticSO>();


    public void LoadData(GameData gameData)
    {
        foreach (var statistic in statistics)
        {
            if (gameData.statistics.ContainsKey(statistic.id))
            {
                statistic.SetValue(gameData.statistics[statistic.id]);
            }
        }
    }

    public void SaveData(GameData gameData)
    {
        foreach (var statistic in statistics)
        {
            if (gameData.statistics.ContainsKey(statistic.id))
            {
                gameData.statistics.Remove(statistic.id);
            }
            gameData.statistics.Add(statistic.id, statistic.value);
        }
    }

    public void OnStatisticEvent(Component sender, object data)
    {
        if (data is ResourceAmount resourceAmount)
        {
            foreach (var statistic in statistics)
            {
                if (statistic.statisticType == StatisticType.Collect)
                {
                    if (statistic.resourceType == resourceAmount.type)
                    {
                        statistic.InreaseValue(1f);
                    }
                }
            }
        }

        if (data is CharacterInformationSO characterInformation)
        {
            foreach (var statistic in statistics)
            {
                if (statistic.statisticType == StatisticType.Kill)
                {
                    if (statistic.characterInformations.Contains(characterInformation))
                    {
                        statistic.InreaseValue(1f);
                    }
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
}
