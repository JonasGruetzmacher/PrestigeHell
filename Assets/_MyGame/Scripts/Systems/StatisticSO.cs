using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using Sirenix.Serialization;

[CreateAssetMenu(fileName = "Statistic", menuName = "StatisticSO")]
public class StatisticSO : SerializedScriptableObject
{
    [ShowInInspector]
    public float value{ get; private set;}
    public string statisticName;
    public string description;

    [Header("Event")]
    public StatisticType statisticType;
    [ShowIf("statisticType", StatisticType.Collect)]
    public ResourceType resourceType;

    [ShowIf("statisticType", StatisticType.Kill)]
    public List<CharacterInformationSO> characterInformations;

    [Header("Unlockables")]
    public Dictionary<UnlockableSO, float> unlockables = new Dictionary<UnlockableSO, float>();
    
    [OdinSerialize]
    private List<(UnlockableSO, float)> unlockablesList = new List<(UnlockableSO, float)>();
    private int currentGoalIndex = 0;


    public void OnValidate()
    {
        unlockablesList.Clear();
        foreach (var unlockable in unlockables)
        {
            if (!unlockablesList.Contains((unlockable.Key, unlockable.Value)))
            {
                unlockablesList.Add((unlockable.Key, unlockable.Value));
            }
        }
    }

    public float GetNextGoalValue()
    {
        if (currentGoalIndex < unlockablesList.Count)
        {
            return unlockablesList[currentGoalIndex].Item2;
        }
        return 100000f;
    }

    public string GetTextDescription()
    {
        string text = "";
        if (currentGoalIndex < unlockablesList.Count)
        {
            text = value + "/" + unlockablesList[currentGoalIndex].Item2;
            text += " --- " + unlockablesList[currentGoalIndex].Item1.GetTextDescription();
        }
        else
        {
            text = value + " --- No more goals.";
        }
        return text;
    }

    public void InreaseValue(float value)
    {
        this.value += value;
        if (currentGoalIndex < unlockablesList.Count)
        {
            if (this.value >= unlockablesList[currentGoalIndex].Item2)
            {
                unlockablesList[currentGoalIndex].Item1.Unlock();
                currentGoalIndex++;
            }
        }
    }

    public void Reset()
    {
        value = 0;
        currentGoalIndex = 0;
        foreach (var unlockable in unlockablesList)
        {
            unlockable.Item1.Reset();
        }
    }
}
