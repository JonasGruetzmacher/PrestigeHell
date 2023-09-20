using System.Collections;
using System.Collections.Generic;
using MoreMountains.TopDownEngine;
using UnityEngine;
using MoreMountains.Tools;
using System;
using Sirenix.OdinInspector;

public class Spawner : TimedSpawner
{
    [ShowInInspector]
    private List<Transform> spawnPoints = new List<Transform>();


    protected override void Initialization()
    {
        base.Initialization();
        spawnPoints = new List<Transform>(GetComponentsInChildren<Transform>());
        spawnPoints.Remove(transform);
    }

    protected override void Spawn()
    {
        GameObject nextGameObject = ObjectPooler.GetPooledGameObject();

			// mandatory checks
			if (nextGameObject==null) { return; }
			if (nextGameObject.GetComponent<MMPoolableObject>()==null)
			{
				throw new Exception(gameObject.name+" is trying to spawn objects that don't have a PoolableObject component.");		
			}	

			// we activate the object
			nextGameObject.gameObject.SetActive(true);
			nextGameObject.gameObject.MMGetComponentNoAlloc<MMPoolableObject>().TriggerOnSpawnComplete();

			// we check if our object has an Health component, and if yes, we revive our character
			Health objectHealth = nextGameObject.gameObject.MMGetComponentNoAlloc<Health> ();
			if (objectHealth != null) 
			{
				objectHealth.Revive ();
			}

			// we position the object
			nextGameObject.transform.position = spawnPoints[UnityEngine.Random.Range(0, spawnPoints.Count)].position;

			// we reset our timer and determine the next frequency
			_lastSpawnTimestamp = Time.time;
			DetermineNextFrequency ();
    }
}
