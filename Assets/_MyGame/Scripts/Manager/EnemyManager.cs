using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Tools;
using MoreMountains.TopDownEngine;

public class EnemyManager : MMSingleton<EnemyManager>, MMEventListener<TopDownEngineEvent>
{
    [SerializeField] private List<MMMultipleObjectPooler> enemyPoolers;


    public virtual void Reset()
    {
        foreach(var obj in FindObjectsOfType<MMPoolableObject>())
        {
            if (obj.gameObject.layer == LayerMask.NameToLayer("Enemies"))
				obj.Destroy();
        }
    }

    public virtual void OnMMEvent(TopDownEngineEvent engineEvent)
		{
			switch (engineEvent.EventType)
			{
				case TopDownEngineEventTypes.RespawnStarted:
                    Debug.Log("RespawnStarted");
					Reset();
					break;
			}
		}


    /// <summary>
		/// OnDisable, we start listening to events.
		/// </summary>
		protected virtual void OnEnable()
		{
			this.MMEventStartListening<TopDownEngineEvent>();
		}

		/// <summary>
		/// OnDisable, we stop listening to events.
		/// </summary>
		protected virtual void OnDisable()
		{
			this.MMEventStopListening<TopDownEngineEvent>();
		}
}
