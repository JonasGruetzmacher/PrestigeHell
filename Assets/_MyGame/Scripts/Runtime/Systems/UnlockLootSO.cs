using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.TopDownEngine;
using MoreMountains.Tools;

namespace LeroGames.PrestigeHell
{
    [CreateAssetMenu(fileName = "UnlockLoot", menuName = "Unlockables/UnlockLootSO")]
    public class UnlockLootSO : UnlockableSO
    {
        public List<MMLootTableGameObjectSO> lootTables;
        public GameObject lootDropPrefab; 
        public int weight;

        public override void Unlock()
        {
            if (lootTables == null)
                return;
            if (lootDropPrefab == null)
                return;
            foreach (var lootTable in lootTables)
            {
                lootTable.SetLootWeight(lootDropPrefab, weight);
            }
            unlocked = true;
        }

        public override void Reset()
        {
            base.Reset();
            if (lootTables == null)
                return;
            if (lootDropPrefab == null)
                return;
            foreach (var lootTable in lootTables)
            {
                lootTable.RemoveLoot(lootDropPrefab);
            }
        }
    }
}