using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Tools;

public class AIActionSpawnEnemy : AIAction
{
    public CharacterInformationSO enemy;
    protected EnemySpawner enemySpawner;

    public override void Initialization()
    {
        if(!ShouldInitialize) return;
        base.Initialization();
        enemySpawner = EnemyManager.Instance.GetEnemySpawner();
    }

    public override void PerformAction()
    {
        Debug.Log(enemySpawner);
        if (enemySpawner == null)
        {
            return;
        }
        enemySpawner.Spawn(this.transform.position, enemy.characterName, 2f);
    }
}
