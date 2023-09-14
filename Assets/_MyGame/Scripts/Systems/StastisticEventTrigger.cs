using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public enum TriggerType
{
    onEnable = 0,
    onDisable = 1,
    onStart = 2,
    onDestroy = 3,

}

public class StastisticEventTrigger : MonoBehaviour
{
    public StatisticEvent statisticEvent;
    public TriggerType triggerType;

    private void Start()
    {
        if (triggerType == TriggerType.onStart)
        {
            TriggerEvent();
        }
    }

    private void OnEnable()
    {
        if (triggerType == TriggerType.onEnable)
        {
            TriggerEvent();
        }
    }

    private void OnDisable()
    {
        if (triggerType == TriggerType.onDisable)
        {
            TriggerEvent();
        }
    }

    private void OnDestroy()
    {
        if (triggerType == TriggerType.onDestroy)
        {
            TriggerEvent();
        }
    }

    

    [Button]
    public void TriggerEvent()
    {
        statisticEvent.Trigger();
    }
}
