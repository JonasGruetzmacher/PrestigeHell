using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "Statistic", menuName = "StatisticSO")]
public class StatisticSO : ScriptableObject
{
    [DisableInEditorMode]
    public float value;
    public string statisticName;
    public string description;
    public StatisticType statisticType;

    public bool toggleAdditionalAttribute;
    [ShowIf("toggleAdditionalAttribute")]
    public string additionalAttribute;
}
