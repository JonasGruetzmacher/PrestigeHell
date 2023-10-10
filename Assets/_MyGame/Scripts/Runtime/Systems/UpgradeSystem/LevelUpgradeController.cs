using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using LeroGames.Tools;

namespace LeroGames.PrestigeHell
{
    public class LevelUpgradeController : MonoBehaviour, ITooltipInformation
    {
        public LevelUpgradeSO levelUpgradeSO;

        public Tools.GameEvent onLevelUpgradeStateChanged;

        [SerializeField] private ButtonViewModel buttonViewModel;
        private LevelUpgrade levelUpgrade;
        private LevelUpgrade.LevelUpgradeState levelUpgradeState = LevelUpgrade.LevelUpgradeState.Unassigned;

        private void Initilize()
        {
            levelUpgrade = new LevelUpgrade(levelUpgradeSO);

        }

        public void SendState()
        {
            onLevelUpgradeStateChanged.Raise(this, levelUpgradeState);
            GetComponent<ButtonViewModel>().button.SetText(levelUpgrade.GetShortDescription());
            GetComponent<ButtonViewModel>().button.SetButtonState(levelUpgradeState.ToButtonState());
        }

        public void OnApplyLevelUpgrade(Component sender, object data)
        {
            if (sender.gameObject != this.gameObject)
                return;
            if (levelUpgrade.GetLevelUpgradeState() == LevelUpgrade.LevelUpgradeState.Pickable)
            {
                levelUpgrade.SelectLevelUpgrade();
                levelUpgradeState = levelUpgrade.GetLevelUpgradeState();
                SendState();
            }
        }

        public void OnLevelUpgradeStateChanged(Component sender, object data)
        {
            if (sender == this)
                return;
            if (data is LevelUpgrade.LevelUpgradeState otherLevelUpgradeState)
            {
                if (otherLevelUpgradeState != LevelUpgrade.LevelUpgradeState.Selected)
                {
                    return;
                }
                LevelUpgradeController otherLevelUpgradeController = sender as LevelUpgradeController;
                if (levelUpgradeSO.getBlockedBy.Contains(otherLevelUpgradeController.levelUpgradeSO))
                {
                    levelUpgrade.BlockLevelUpgrade();
                    levelUpgradeState = levelUpgrade.GetLevelUpgradeState();
                    SendState();

                }
            }
        }

        public void GetTooltipInformation(out string infoLeft, out string infoRight)
        {
            levelUpgrade.GetTooltipInformation(out infoLeft, out infoRight);
        }


        public void OnLevelReset(Component sender, object data)
        {
            Debug.Log("OnLevelReset");
            levelUpgrade.ResetLevelUpgrade();
            levelUpgradeState = levelUpgrade.GetLevelUpgradeState();
            SendState();
        }

        public void OnEnable()
        {
            if (levelUpgrade == null)
                Initilize();
            if (levelUpgrade.GetLevelUpgradeState() == levelUpgradeState)
                return;
            levelUpgradeState = levelUpgrade.GetLevelUpgradeState();
            SendState();
        }
    }
}