using System.Collections;
using System.Collections.Generic;
using MoreMountains.Tools;
using UnityEngine;
using Sirenix.OdinInspector;
using System;
using Unity.VisualScripting;
using MoreMountains.TopDownEngine;


public class ResourcesManager : MMSingleton<ResourcesManager>, MMEventListener<ResourceEvent>, MMEventListener<TopDownEngineEvent>
{


    public MMSerializableDictionary<ResourceType, float> resources = new MMSerializableDictionary<ResourceType, float>();

    [SerializeField] private AnimationCurve levelCurve;

    protected override void Awake()
    {
        base.Awake();
        foreach (var resource in ResourceType.GetValues(typeof(ResourceType)))
        {
            resources.Add((ResourceType)resource, 0);
        }
    }

    public float GetResourceAmount(ResourceType type)
    {
        return resources[type];
    }

    public int GetResourceAmountInt(ResourceType type)
    {
        return Mathf.RoundToInt(resources[type]);
    }

    public void AddResource(ResourceAmount resourceAmount)
    {
        AddResource(resourceAmount.type, resourceAmount.amount);
    }

    public void AddResource(ResourceType type, float amount)
    {
        resources[type] += amount;
        if(type == ResourceType.LevelPoints)
            CheckLevelUp();
        if(type == ResourceType.Danger)
        {
            GameEvent.Trigger(Eventname.DangerChanged);
        }
        UpdateResource(type);
    }

    public void RemoveResource(ResourceAmount resourceAmount)
    {
        RemoveResource(resourceAmount.type, resourceAmount.amount);
    }

    public void RemoveResource(ResourceType type, float amount)
    {
        resources[type] -= amount;
        UpdateResource(type);
    }


    public void SetResource(ResourceType type, float amount)
    {
        resources[type] = amount;
        UpdateResource(type);
    }

    public bool CanAfford(ResourceAmount[] cost)
    {
        foreach (var resource in cost)
        {
            if (resources[resource.type] < resource.amount)
                return false;
        }
        return true;
    }

    public void Pay(ResourceAmount[] cost)
    {
        foreach (var resource in cost)
        {
            RemoveResource(resource);
            UpdateResource(resource.type);
        }
    }

    private void CheckLevelUp()
    {
        if (resources[ResourceType.LevelPoints]>= GetNextLevelRequirement())
        {
            LevelUp();
        }
    }

    private void LevelUp()
    {
        RemoveResource(ResourceType.LevelPoints, levelCurve.Evaluate(resources[ResourceType.Level]));
        AddResource(ResourceType.Level, 1);
        GameEvent.Trigger(Eventname.LevelUp);
    }

    public float GetNextLevelRequirement()
    {
        return levelCurve.Evaluate(resources[ResourceType.Level] + 1);
    }

    private void Reset()
    {
        SetResource(ResourceType.Level, 0);
        SetResource(ResourceType.LevelPoints, 0);
        SetResource(ResourceType.Danger, 0);
    }

    public virtual void OnMMEvent(ResourceEvent eventType)
    {
        switch (eventType.resourceMethod)
        {
            case ResourceMethods.Add:
                AddResource(eventType.resourceAmount);
                if(eventType.resourceAmount.type == ResourceType.XP)
                    AddResource(ResourceType.LevelPoints, eventType.resourceAmount.amount);
                break;
            case ResourceMethods.Remove:
                RemoveResource(eventType.resourceAmount);
                break;
            case ResourceMethods.Set:
                resources[eventType.resourceAmount.type] = eventType.resourceAmount.amount;
                break;
        } 
    }

    public virtual void OnMMEvent(TopDownEngineEvent eventType)
    {
        switch (eventType.EventType)
        {
            case TopDownEngineEventTypes.RespawnStarted:
                Reset();
                break;
        }
    }

    private void UpdateResource(ResourceType type)
    {
        MyGUIManager.Instance.SendMessageUpwards("UpdateResourceBar", type);
        MyGUIManager.Instance.SendMessageUpwards("UpdateResourceText", type);
    }


    private void OnEnable()
    {
        this.MMEventStartListening<ResourceEvent>();
        this.MMEventStartListening<TopDownEngineEvent>();
    }

    private void OnDisable()
    {
        this.MMEventStopListening<ResourceEvent>();
        this.MMEventStopListening<TopDownEngineEvent>();
    }


}

public struct ResourceEvent
{
    public ResourceMethods resourceMethod;
    public ResourceAmount resourceAmount;

    public ResourceEvent(ResourceMethods pointsMethod, ResourceAmount resourceAmount)
    {
        this.resourceMethod = pointsMethod;
        this.resourceAmount = resourceAmount;
    }

    static ResourceEvent e;
    public static void Trigger(ResourceMethods pointsMethod, ResourceAmount resourceAmount)
    {
        e.resourceMethod = pointsMethod;
        e.resourceAmount = resourceAmount;
        MMEventManager.TriggerEvent(e);
    }
}

    public enum ResourceMethods { Add, Remove, Set }
