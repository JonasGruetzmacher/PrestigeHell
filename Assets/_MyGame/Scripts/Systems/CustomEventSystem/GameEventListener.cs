using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace CustomEventSystem
{
    [System.Serializable]
    public class CustomGameEvent : UnityEvent<Component, object> { }
    public class GameEventListener : MonoBehaviour
    {
        public GameEvent gameEvent;
        public CustomGameEvent response;

        public void OnEnable()
        {
            gameEvent.RegisterListener(this);
        }

        public void OnDisable()
        {
            gameEvent.UnregisterListener(this);
        }

        public void OnEventRaised(Component sender, object data)
        {
            response.Invoke(sender, data);
        }
    }
}
