using UnityEngine;


[CreateAssetMenu(fileName = "UnlockEnemy", menuName = "Unlockables/UnlockEnemySO")]
public class UnlockEnemySO : UnlockableSO
{
    // public EnemySO enemySO;

    public override void Unlock()
    {
        unlocked = true;
        Debug.Log("Unlock Enemy");
    }
}