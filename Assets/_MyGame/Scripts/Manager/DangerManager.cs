using System.Collections;
using System.Collections.Generic;
using MoreMountains.Tools;
using MoreMountains.TopDownEngine;
using UnityEngine;

public class DangerManager : MMSingleton<DangerManager>, MMEventListener<TopDownEngineEvent>
{
    private float dangerProgress;
    [SerializeField] private float secondsToDanger = 20f;

    private void Start()
    {
        StartDangerProgress();
    }

    private void Reset()
    {
        StopAllCoroutines();
        dangerProgress = 0;
    }

    public float GetDangerProgress()
    {
        return dangerProgress;
    }

    private void StartDangerProgress()
    {
        StartCoroutine(DangerCoroutine());
    }

    private IEnumerator DangerCoroutine()
    {
        while(true)
        {
            while(dangerProgress <= 100)
            {
                dangerProgress += 1;
                yield return new WaitForSeconds(secondsToDanger / 100f);
            }
            ResourceEvent.Trigger(ResourceMethods.Add, new ResourceAmount(ResourceType.Danger, 1));
            dangerProgress -= 100;
        }
    }

    public virtual void OnMMEvent(TopDownEngineEvent eventType)
    {
        switch(eventType.EventType)
        {
            case TopDownEngineEventTypes.RespawnStarted:
                Reset();
                break;
            case TopDownEngineEventTypes.RespawnComplete:
                StartDangerProgress();
                break;
        }   
    }

    private void OnEnable()
    {
        this.MMEventStartListening<TopDownEngineEvent>();
    }

    private void OnDisable()
    {
        this.MMEventStopListening<TopDownEngineEvent>();
    }
}
