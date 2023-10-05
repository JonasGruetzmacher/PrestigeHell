using UnityEngine;

namespace LeroGames.PrestigeHell
{
    [CreateAssetMenu(fileName = "UnlockEnemy", menuName = "Unlockables/UnlockEnemySO")]
    public class UnlockEnemySO : UnlockableSO
    {
        public CharacterInformationSO enemyInformation;

        public override void Unlock()
        {
            if (enemyInformation == null)
            {
                return;
            }
            if (unlocked)
            {
                return;
            }
            unlocked = true;
        
            EnemyManager.Instance.AddSpawnableEnemy(enemyInformation);
        }

        public override string GetTextDescription()
        {
            return "Unlock new enemy: " + enemyInformation.characterName;
        }
    }}