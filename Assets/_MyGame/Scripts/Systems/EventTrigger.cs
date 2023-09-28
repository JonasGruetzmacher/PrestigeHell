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
    OnKilled = 4,

}

public class EventTrigger : MonoBehaviour
{
    public CustomEventSystem.GameEvent gameEvent;
    public TriggerType triggerType;

    [ShowIf("triggerType", TriggerType.OnKilled)]
    public CharacterInformationSO killedCharacter;

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
    [ShowIf("triggerType", TriggerType.OnKilled)]
    [Button("Trigger")]
    public void OnKilled()
    {
        if (triggerType == TriggerType.OnKilled)
        {
            TriggerEvent(killedCharacter);
        }
    }

    public void TriggerEvent(object data = null)
    {
        gameEvent?.Raise(this, data);
    }
}
