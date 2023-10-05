using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Tools;
using MoreMountains.TopDownEngine;
using UnityEngine.Pool;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using System.Linq;

namespace LeroGames.PrestigeHell
{
	public class EnemyManager : MMSingleton<EnemyManager>, MMEventListener<TopDownEngineEvent>
	{
		[Header("Events")]
		[SerializeField] private LeroGames.Tools.GameEvent enemiesUnlockedEvent;
		[SerializeField] private EnemySpawner enemySpawner;
		[SerializeField] private MMMultipleObjectPooler enemyPooler;
		[SerializeField] private List<CharacterInformationSO> startEnemiesList;
		[SerializeField] private List<CharacterInformationSO> allEnemiesList;

		[ShowInInspector, ReadOnly]
		private MMSerializableDictionary<CharacterInformationSO, bool> currentEnemiesInformation = new MMSerializableDictionary<CharacterInformationSO, bool>();

		[ShowInInspector, ReadOnly]
		private List<CharacterInformationSO> nextRoundEnemiesInformation = new List<CharacterInformationSO>();



		[ShowInInspector, ReadOnly]
		private MMSerializableDictionary<string, MMMultipleObjectPoolerObject> enemyPoolerObjects = new MMSerializableDictionary<string, MMMultipleObjectPoolerObject>();

		protected override void Awake()
		{
			base.Awake();
			Initialize();
		}

		private void Initialize()
		{
			SetPoolerObjects();
			foreach (var enemyInformation in startEnemiesList)
			{
				currentEnemiesInformation.Add(enemyInformation, true);
				nextRoundEnemiesInformation.Add(enemyInformation);
				enemyPoolerObjects[enemyInformation.characterName].Enabled = true;

				enemiesUnlockedEvent.Raise(this, enemyInformation);
			}
		}

		private void SetPoolerObjects()
		{
			foreach (var enemyInformation in allEnemiesList)
			{
				MMMultipleObjectPoolerObject poolerObject = new MMMultipleObjectPoolerObject();
				poolerObject.GameObjectToPool = enemyInformation.characterPrefab;
				poolerObject.PoolSize = enemyInformation.poolSize;
				poolerObject.PoolCanExpand = true;

				poolerObject.Enabled = false;

				enemyPooler.Pool.Add(poolerObject);
				enemyPoolerObjects.Add(enemyInformation.characterName, poolerObject);
			}

			enemyPooler.FillObjectPool();
		}

		public List<CharacterInformationSO> GetSpawnableEnemyInformations()
		{
			List<CharacterInformationSO> spawnableEnemyInformations = new List<CharacterInformationSO>();
			foreach (var enemyInformation in currentEnemiesInformation)
			{
				spawnableEnemyInformations.Add(enemyInformation.Key);
			}
			return spawnableEnemyInformations;
		}

		public List<CharacterInformationSO> GetSpawningEnemyInformations()
		{
			List<CharacterInformationSO> spawningEnemyInformations = new List<CharacterInformationSO>();
			foreach (var enemyInformation in currentEnemiesInformation)
			{
				if (enemyInformation.Value)
				{
					spawningEnemyInformations.Add(enemyInformation.Key);
				}
			}
			return spawningEnemyInformations;
		}

		public void AddSpawnableEnemy(CharacterInformationSO enemyInformation)
		{
			if (enemyInformation == null) 
			{ 
				return; 
			}
			if (!currentEnemiesInformation.ContainsKey(enemyInformation)) 
			{
				nextRoundEnemiesInformation.Add(enemyInformation);
				enemiesUnlockedEvent.Raise(this, enemyInformation);	
			}
		}

		public virtual void Reset()
		{
			foreach(var obj in FindObjectsOfType<MMPoolableObject>())
			{
				if (obj.gameObject.layer == LayerMask.NameToLayer("Enemies"))
					obj.Destroy();
			}
			foreach (var enemyInformation in currentEnemiesInformation.ToArray())
			{
				currentEnemiesInformation[enemyInformation.Key] = false;
			}

			foreach (var enemyInformation in nextRoundEnemiesInformation)
			{
				Debug.Log(enemyInformation.characterName);
				currentEnemiesInformation[enemyInformation] = true;
			}

			foreach (var enemyInformation in currentEnemiesInformation)
			{
				enemyPoolerObjects[enemyInformation.Key.characterName].Enabled = enemyInformation.Value;
			}
		}

		public virtual float GetSpawnChance(CharacterInformationSO enemyInformation)
		{
			if (enemyInformation == null) 
			{ 
				return 0f; 
			}
			if (!currentEnemiesInformation.ContainsKey(enemyInformation) || !currentEnemiesInformation[enemyInformation])
			{ 
				return 0f; 
			}
			int totalPoolSize = 0;
			foreach (var enemy in currentEnemiesInformation)
			{
				if (enemy.Value)
				{
					totalPoolSize += enemy.Key.poolSize;
				}
			}

			return (float)enemyInformation.poolSize / (float)totalPoolSize;
		}

		public virtual void SetSpawnableEnemies(List<CharacterInformationSO> spawnableEnemiesInformation)
		{
			Debug.Log("SetSpawnableEnemies");
			Debug.Log(spawnableEnemiesInformation);
			if (spawnableEnemiesInformation == null || spawnableEnemiesInformation.Count < 3) 
			{ 
				throw new System.Exception("Select at least 3 enemies");
			}
			nextRoundEnemiesInformation = spawnableEnemiesInformation;

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

		public virtual void KillAllEnemies()
		{
			foreach(var obj in FindObjectsOfType<MMPoolableObject>())
			{
				if (obj.gameObject.layer == LayerMask.NameToLayer("Enemies"))
					obj.Destroy();
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
}