using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.TopDownEngine;
using MoreMountains.Tools;

namespace LeroGames.PrestigeHell
{
    public class MyGameManager : GameManager
    {
        public bool panelActive = false;
        public string activePanelName = "";

        public override void Pause(PauseMethods method = PauseMethods.PauseMenu, bool pause = true)
        {
            if (panelActive)
            {
                return;
            }
            base.Pause(method, pause);
            
        }

        public virtual void OpenStatistics()
        {
            if (_pauseMenuOpen || panelActive)
            {
                return;
            }

            Pause(PauseMethods.NoPauseMenu, false);
            if (GUIManager.HasInstance)
            {
                (GUIManager.Instance as MyGUIManager).SetStatisticPanel(true);
                panelActive = true;
                activePanelName = "Statistics";
            }
        }

        public virtual void CloseStatistics()
        {
            UnPause(PauseMethods.NoPauseMenu);
            if (GUIManager.HasInstance)
            {
                (GUIManager.Instance as MyGUIManager).SetStatisticPanel(false);
                panelActive = false;
                activePanelName = "";
            }
        }

        public virtual void OpenLevelUpgrades()
        {
            if (panelActive || _pauseMenuOpen)
            { 
                return; 
            }

            Pause(PauseMethods.NoPauseMenu, false);
            if (GUIManager.HasInstance)
            {
                (GUIManager.Instance  as MyGUIManager).SetLevelUpPanel(true);
                panelActive = true;
                activePanelName = "LevelUpgrades";
            }
        }

        public virtual void CloseLevelUpgrades()
        {
            UnPause(PauseMethods.NoPauseMenu);
            if (GUIManager.HasInstance)
            {
                (GUIManager.Instance as MyGUIManager).SetLevelUpPanel(false);
                panelActive = false;
                activePanelName = "";
            }
        }

        public virtual void OpenEnemySelection()
        {
            if (panelActive || _pauseMenuOpen)
            {
                return;
            }

            Pause(PauseMethods.NoPauseMenu, false);
            if (GUIManager.HasInstance)
            {
                (GUIManager.Instance as MyGUIManager).SetEnemySelectionPanel(true);
                panelActive = true;
                activePanelName = "EnemySelection";
            }
        }

        public virtual void CloseEnemySelection()
        {
            UnPause(PauseMethods.NoPauseMenu);
            if (GUIManager.HasInstance)
            {
                (GUIManager.Instance as MyGUIManager).SetEnemySelectionPanel(false);
                panelActive = false;
                activePanelName = "";
            }
        }

        public virtual void KillPlayer()
        {
            LevelManager.Instance.Players[0].CharacterHealth.Kill();
        }

        public override void OnMMEvent(MMGameEvent gameEvent)
        {
            base.OnMMEvent(gameEvent);
            if (gameEvent.EventName == "ToggleLevelUpgrades")
            {
                if (panelActive && activePanelName == "LevelUpgrades") 
                {
                    MMGameEvent.Trigger("LevelUpgradesClosed");
                }
                else
                {
                    MMGameEvent.Trigger("LevelUpgradesOpened");
                }
            }
            if (gameEvent.EventName == "LevelUpgradesOpened")
            {
                OpenLevelUpgrades();
            }

            if (gameEvent.EventName == "LevelUpgradesClosed")
            {
                CloseLevelUpgrades();
            }

            if (gameEvent.EventName == "ToggleStatistics")
            {
                if (panelActive && activePanelName == "Statistics")
                {
                    MMGameEvent.Trigger("StatisticsClosed");
                }
                else
                {
                    MMGameEvent.Trigger("StatisticsOpened");
                }
            }
            if (gameEvent.EventName == "StatisticsOpened")
            {
                OpenStatistics();
            }
            if (gameEvent.EventName == "StatisticsClosed")
            {
                CloseStatistics();
            }

            if (gameEvent.EventName == "ToggleEnemySelection")
            {
                if (panelActive && activePanelName == "EnemySelection")
                {
                    MMGameEvent.Trigger("EnemySelectionClosed");
                }
                else
                {
                    MMGameEvent.Trigger("EnemySelectionOpened");
                }
            }
            if (gameEvent.EventName == "EnemySelectionOpened")
            {
                OpenEnemySelection();
            }
            if (gameEvent.EventName == "EnemySelectionClosed")
            {
                CloseEnemySelection();
            }
        }
    }
}