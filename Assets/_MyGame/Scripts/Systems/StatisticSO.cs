using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "Statistic", menuName = "StatisticSO")]
public class StatisticSO : SerializedScriptableObject
{
    [ShowInInspector]
    public float value{ get; private set;}
    public string statisticName;
    public string description;

    [Header("Event")]
    public StatisticType statisticType;

    public bool toggleAdditionalAttribute;
    [ShowIf("toggleAdditionalAttribute")]
    public string additionalAttribute;

    [Header("Unlockables")]
    [SerializeField]
    public Dictionary<UnlockableSO, float> unlockables = new Dictionary<UnlockableSO, float>();
    private List<(UnlockableSO, float)> unlockablesList = new List<(UnlockableSO, float)>();
    private int currentGoalIndex = 0;


    public void OnValidate()
    {
        foreach (var unlockable in unlockables)
        {
            unlockablesList.Add((unlockable.Key, unlockable.Value));
        }
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
