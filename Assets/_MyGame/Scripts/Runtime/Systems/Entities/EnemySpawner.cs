using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Tools;
using System;
using MoreMountains.TopDownEngine;
using Sirenix.OdinInspector;
// using LeroGames.StatSystem;

namespace LeroGames.PrestigeHell
{
    public class EnemySpawner : TimedSpawner, MMEventListener<TopDownEngineEvent>
    {
        [Header("Stat Types")]
        public StatType groupSizeStatType;
        public StatType groupSizeVarianceStatType;
        public StatType groupSpawnRadiusStatType;
        public float groupSpawnRadius = 1f;

        [ShowInInspector]
        private List<Transform> spawnPoints = new List<Transform>();

        protected override void Initialization()
        {
            base.Initialization();
            spawnPoints = new List<Transform>(GetComponentsInChildren<Transform>());
            spawnPoints.Remove(transform);
        }

        public virtual void Spawn(Vector2 position, GameObject nextGameObject, float spawnRadius = 0f)
        {
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
            nextGameObject.transform.position = new Vector2(position.x + UnityEngine.Random.Range(-spawnRadius, spawnRadius), position.y + UnityEngine.Random.Range(-spawnRadius, spawnRadius));

            // we reset our timer and determine the next frequency
            _lastSpawnTimestamp = Time.time;
            DetermineNextFrequency ();
        }
        public virtual void Spawn(Vector2 position)
        {
            GameObject nextGameObject = ObjectPooler.GetPooledGameObject();

            int groupSize = 1;
            float groupSpawnRadius = 1f;

            nextGameObject.TryGetComponent(out CharacterInformation characterInformation);
            if (characterInformation != null)
            {
                Stats stats = characterInformation.CharacterInformationSO.stats;

                if (stats.IncludesStat(groupSizeStatType))
                {
                    if (stats.IncludesStat(groupSizeVarianceStatType))
                    {
                        int groupSizeVariance = (int)stats.GetStat(groupSizeVarianceStatType);
                        int variance = UnityEngine.Random.Range(-groupSizeVariance, groupSizeVariance+1);
                        groupSize = (int)(stats.GetStat(groupSizeStatType) + variance);
                    }
                    else
                    {
                        groupSize = (int)stats.GetStat(groupSizeStatType);
                    }
                }
                if (stats.IncludesStat(groupSpawnRadiusStatType))
                {
                    groupSpawnRadius = stats.GetStat(groupSpawnRadiusStatType);
                }
                
            }

            if (groupSize > 1)
            {
                SpawnGroup(position, nextGameObject.name, groupSize, groupSpawnRadius);
                return;
            }

            Spawn(position, nextGameObject);
        }

        public virtual void Spawn(Vector2 position, string searchedName, float spawnRadius = 0f)
        {
            if (ObjectPooler is MMMultipleObjectPooler)
            {
                var multipleObjectPooler = ObjectPooler as MMMultipleObjectPooler;
                GameObject nextGameObject = multipleObjectPooler.GetPooledGameObjectOfType(searchedName);
                if (nextGameObject==null)
                {
                    Debug.LogError("No object with name "+searchedName+" was found in the object pooler.");
                    return;
                }
                Spawn(position, nextGameObject, spawnRadius);
            }
            else
            {
                Debug.LogError("Object pooler is not a MMMultipleObjectPooler.");
            }

        }

        protected override void Spawn()
        {
            Vector2 position = spawnPoints[UnityEngine.Random.Range(0, spawnPoints.Count)].position;
            Spawn(position);
        }

        public void SpawnRandomEnemy()
        {
            Spawn();
        }

        public virtual void SpawnGroup(Vector2 position, string searchedName, int groupSize, float groupSpawnRadius = 1f)
        {
            for (int i = 0; i < groupSize; i++)
            {
                Spawn(new Vector2(position.x + UnityEngine.Random.Range(-groupSpawnRadius, groupSpawnRadius), position.y + UnityEngine.Random.Range(-groupSpawnRadius, groupSpawnRadius)), searchedName);
            }
        }

        public void OnMMEvent(TopDownEngineEvent engineEvent)
        {
            switch (engineEvent.EventType)
            {
                case TopDownEngineEventTypes.PlayerDeath:
                    CanSpawn = false;
                    break;
                case TopDownEngineEventTypes.RespawnStarted:
                    CanSpawn = false;
                    break;
                case TopDownEngineEventTypes.RespawnComplete:
                    CanSpawn = true;
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
}