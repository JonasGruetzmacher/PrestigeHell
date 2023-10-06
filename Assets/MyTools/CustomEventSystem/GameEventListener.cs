using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace LeroGames.Tools
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