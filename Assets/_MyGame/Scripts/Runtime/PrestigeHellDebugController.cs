using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LeroGames.Tools;
using MoreMountains.TopDownEngine;
using MoreMountains.Tools;
using MoreMountains.InventoryEngine;
using System.Linq;

namespace LeroGames.PrestigeHell
{
    public class PrestigeHellDebugController : DebugController
    {
        [SerializeField] Inventory mainInventory;
        public static DebugCommand KILL_ALL;
        public static DebugCommand SPAWN_ENEMY;
        public static DebugCommand<int> SET_XP;
        public static DebugCommand HELP;
        public static DebugCommand<ResourceType, int> SET_RESOURCE;
        public static DebugCommand IRONWOOD;
        public static DebugCommand<string, int> ADD_ITEM;
        public static DebugCommand<string> UNLOCK;
        public static DebugCommand<string, int> SET_STATISTIC;
        public static DebugCommand HARD_RESET;

        protected override void Awake()
        {
            HARD_RESET = new DebugCommand("hard_reset", "Hard reset game", "hard_reset", () => { DataPersistenceManager.instance.NewGame(); });
            KILL_ALL = new DebugCommand("kill_all", "Kill all enemies", "kill_all", () => { EnemyManager.Instance.KillAllEnemies(); });
            SPAWN_ENEMY = new DebugCommand("spawn_enemy", "Spawn random enemy", "spawn_enemy", () => { EnemyManager.Instance.GetEnemySpawner().SpawnRandomEnemy(); });
            SET_XP = new DebugCommand<int>("set_xp", "Set player xp", "set_xp [xp]", (xp) => { ResourcesManager.Instance.SetResource(ResourceType.XP, xp); });
            HELP = new DebugCommand("help", "Show all commands", "help", () => { showHelp = !showHelp; });
            SET_RESOURCE = new DebugCommand<ResourceType, int>("set_resource", "Set resource", "set_resource [resource type] [value]", (resource, value) => { ResourcesManager.Instance.SetResource(resource, value); });
            IRONWOOD = new DebugCommand("ironwood", "Exalted forever number 1", "ironwood", () => 
            {
                LevelManager.Instance.Players[0].GetComponentInChildren<SpriteRenderer>().sprite = Resources.Load<Sprite>("Characters/Ironwood");
                LevelManager.Instance.Players[0].GetComponentInChildren<Animator>().enabled = false;
                Debug.Log("Exalted forever number 1");
            });
            ADD_ITEM = new DebugCommand<string, int>("add_item", "Add item to inventory", "add_item [item name] [quantity]", (itemID, quantity) => 
            {
                List<InventoryItem> items = Resources.LoadAll<InventoryItem>("Items/").ToList();
                foreach (InventoryItem item in items)
                {
                    if (item.ItemID == itemID)
                    {
                        mainInventory.AddItem(item, quantity);
                        return;
                    }
                }
                Debug.Log("Item not found");
            });
            UNLOCK = new DebugCommand<string>("unlock", "Unlock item", "unlock [unlock name]", (unlockID) =>
            {
                List<UnlockableSO> unlocks = Resources.LoadAll<UnlockableSO>("Unlockables/").ToList();
                foreach (UnlockableSO unlock in unlocks)
                {
                    if (unlock.unlockID == unlockID)
                    {
                        unlock.Unlock();
                        return;
                    }
                }
                Debug.Log("Unlock not found");
            });

            SET_STATISTIC = new DebugCommand<string, int>("set_statistic", "Set statistic", "set_statistic [statistic id] [value]", (id, value) =>
            {
                List<StatisticSO> statistics = Resources.LoadAll<StatisticSO>("Statistics/").ToList();
                foreach (StatisticSO statistic in statistics)
                {
                    if (statistic.id == id)
                    {
                        statistic.SetValue(value);
                        return;
                    }
                }
                Debug.Log("Statistic not found");
            });

            commandList = new List<object>
            {
                KILL_ALL,
                SPAWN_ENEMY,
                SET_XP,
                HELP,
                SET_RESOURCE,
                IRONWOOD,
                ADD_ITEM,
                UNLOCK,
                SET_STATISTIC,
                HARD_RESET,
            };
        }
    }
}
