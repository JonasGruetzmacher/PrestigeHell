using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Tools;
using MoreMountains.TopDownEngine;
namespace LeroGames.PrestigeHell
{
    public class EnemiesDisplay : MonoBehaviour, MMEventListener<TopDownEngineEvent>
    {
        public GameObject enemyDisplayContainer;
        public GameObject enemyDisplayPrefab;

        private void Start()
        {
            DisplayEnemies(EnemyManager.Instance.GetSpawningEnemyInformations());
        }
        
        public void DisplayEnemies(List<CharacterInformationSO> characterInformations)
        {
            foreach (Transform child in enemyDisplayContainer.transform)
            {
                Destroy(child.gameObject);
            }

            foreach (var characterInformation in characterInformations)
            {
                GameObject enemyDisplay = Instantiate(enemyDisplayPrefab, enemyDisplayContainer.transform);
                enemyDisplay.GetComponent<EnemyStatsVisual>().SetCharacterInformation(characterInformation);
            }
        }

        public void OnMMEvent(TopDownEngineEvent eventType)
        {
            if (eventType.EventType == TopDownEngineEventTypes.RespawnComplete)
            {
                DisplayEnemies(EnemyManager.Instance.GetSpawningEnemyInformations());
            }
        }

        public void OnEnable()
        {
            this.MMEventStartListening<TopDownEngineEvent>();
        }

        public void OnDisable()
        {
            this.MMEventStopListening<TopDownEngineEvent>();
        }
    }
}