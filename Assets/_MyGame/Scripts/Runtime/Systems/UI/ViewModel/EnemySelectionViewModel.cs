using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Tools;
using Sirenix.OdinInspector;

namespace LeroGames.PrestigeHell
{
    public class EnemySelectionViewModel : MonoBehaviour
    {
        public Transform content;
        public CustomToggle togglePrefab;


        [ShowInInspector, ReadOnly]
        private MMSerializableDictionary<CharacterInformationSO, CustomToggle> toggles = new MMSerializableDictionary<CharacterInformationSO, CustomToggle>();

        [Header("Event")]
        public LeroGames.Tools.GameEvent onSelectEnemies;

        public void OnSelectEnemies()
        {
            List<CharacterInformationSO> characterInformations = new List<CharacterInformationSO>();
            foreach (var toggle in toggles)
            {
                if (toggle.Value.IsOn())
                {
                    characterInformations.Add(toggle.Key);
                }
            }
            onSelectEnemies.Raise(this, characterInformations);
        }

        public void OnAddEnemy(Component sender, object data)
        {
            if (data is CharacterInformationSO enemyInformation)
            {
                AddEnemy(enemyInformation);
            }
        }

        [Button]
        public void AddEnemy(CharacterInformationSO enemyInformation)
        {
            if (enemyInformation == null) { return; }
            if (!toggles.ContainsKey(enemyInformation))
            {
                CustomToggle toggle = Instantiate(togglePrefab, content);
                toggle.SetSprite(enemyInformation.characterSprite);
                toggles.Add(enemyInformation, toggle);
            }
        }

        private void OnEnable()
        {
            foreach (var enemyInformation in EnemyManager.Instance.GetSpawnableEnemyInformations())
            {
                AddEnemy(enemyInformation);
            }
        }
    }
}