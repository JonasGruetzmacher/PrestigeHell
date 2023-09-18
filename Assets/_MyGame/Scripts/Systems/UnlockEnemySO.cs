using UnityEngine;


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
}