using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Tools;
using MoreMountains.TopDownEngine;
using UnityEngine.Pool;
using Sirenix.OdinInspector;

public class EnemyManager : MMSingleton<EnemyManager>, MMEventListener<TopDownEngineEvent>
{
	[SerializeField] private EnemySpawner enemySpawner;
    [SerializeField] private MMMultipleObjectPooler enemyPooler;

	[SerializeField] private MMSerializableDictionary<string, CharacterInformationSO> allEnemyInformations;
	[SerializeField] private MMSerializableDictionary<string, CharacterInformationSO> spawnableEnemyInformations;

	[ShowInInspector]
	private MMSerializableDictionary<string, MMMultipleObjectPoolerObject> enemyPoolerObjects = new MMSerializableDictionary<string, MMMultipleObjectPoolerObject>();

	private void Start()
	{
		SetPooler();
	}

	private void SetPooler()
	{
		foreach (var enemyInformation in allEnemyInformations.Values)
		{
			MMMultipleObjectPoolerObject poolerObject = new MMMultipleObjectPoolerObject();
			poolerObject.GameObjectToPool = enemyInformation.characterPrefab;
			poolerObject.PoolSize = enemyInformation.poolSize;
			poolerObject.PoolCanExpand = true;
			if (spawnableEnemyInformations.ContainsKey(enemyInformation.characterName))
			{
				poolerObject.Enabled = true;
			}
			else
			{
				poolerObject.Enabled = false;
			}
			enemyPooler.Pool.Add(poolerObject);
			enemyPoolerObjects.Add(enemyInformation.characterName, poolerObject);
		}
		enemyPooler.FillObjectPool();
	}

	public List<CharacterInformationSO> GetEnemyInformations()
	{
		return new List<CharacterInformationSO>(spawnableEnemyInformations.Values);
	}

	public void AddSpawnableEnemy(CharacterInformationSO enemyInformation)
	{
		if (enemyInformation == null) { return; }
		if (spawnableEnemyInformations.ContainsKey(enemyInformation.characterName)) { return; }
		spawnableEnemyInformations.Add(enemyInformation.characterName, enemyInformation);
		// enemyPoolerObjects[enemyInformation.name].Enabled = true;
	}

    public virtual void Reset()
    {
        foreach(var obj in FindObjectsOfType<MMPoolableObject>())
        {
            if (obj.gameObject.layer == LayerMask.NameToLayer("Enemies"))
				obj.Destroy();
        }
		foreach (var enemyPool in enemyPoolerObjects.Values)
		{
			enemyPool.Enabled = false;
		}
		foreach (var spawnableEnemy in spawnableEnemyInformations.Keys)
		{
			enemyPoolerObjects[spawnableEnemy].Enabled = true;
		}
    }

	public virtual void AddEnemyToPool(CharacterInformationSO enemyInformation)
	{
		if (enemyInformation == null) { return; }
		if (spawnableEnemyInformations.ContainsKey(enemyInformation.name)) { return; }
		spawnableEnemyInformations.Add(enemyInformation.name, enemyInformation);
	}

	public virtual void RemoveEnemyFromPool(CharacterInformationSO enemyInformation)
	{
		if (enemyInformation == null) { return; }
		if (!spawnableEnemyInformations.ContainsKey(enemyInformation.name)) { return; }
		spawnableEnemyInformations.Remove(enemyInformation.name);
	}

	public virtual float GetSpawnChance(CharacterInformationSO enemyInformation)
	{
		if (enemyInformation == null) { return 0f; }
		if (!spawnableEnemyInformations.ContainsKey(enemyInformation.name)) { return 0f; }
		int totalPoolSize = 0;
		foreach (var enemyPool in spawnableEnemyInformations.Values)
		{
			totalPoolSize += enemyPool.poolSize;
		}

		return (float)enemyInformation.poolSize / (float)totalPoolSize;
	}

	public virtual EnemySpawner GetEnemySpawner()
	{
		return enemySpawner;
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


	protected virtual void OnEnable()
	{
		this.MMEventStartListening<TopDownEngineEvent>();
	}

	protected virtual void OnDisable()
	{
		this.MMEventStopListening<TopDownEngineEvent>();
	}
}
