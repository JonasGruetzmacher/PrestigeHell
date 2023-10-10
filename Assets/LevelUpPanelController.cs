using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LeroGames.Tools;
using System.Linq;
using UnityEngine.PlayerLoop;

namespace LeroGames.PrestigeHell
{
    public class LevelUpPanelController : MonoBehaviour
    {
        string windowId = "LevelUpPanel";
        [SerializeField] private SerializableDictionary<LevelUpgradeSO, ButtonViewModel> levelUpgradesInit;
        private Dictionary<LevelUpgrade, ButtonViewModel> levelUpgradeToVM = new Dictionary<LevelUpgrade, ButtonViewModel>();
        private Dictionary<ButtonViewModel, LevelUpgrade> vMToLevelUpgrade = new Dictionary<ButtonViewModel, LevelUpgrade>();

        private void Start()
        {
            Initialize();
            
        }

        private void Initialize()
        {
            foreach (var leveUpgradeSO in levelUpgradesInit.Keys)
            {
                LevelUpgrade levelUpgrade = new LevelUpgrade(leveUpgradeSO);
                levelUpgradeToVM.Add(levelUpgrade, levelUpgradesInit[leveUpgradeSO]);
                vMToLevelUpgrade.Add(levelUpgradesInit[leveUpgradeSO], levelUpgrade);
            }
        }

        private void BlockUpgrades(LevelUpgradeSO levelUpgradeSO)
        {
            foreach (var levelUpgrade in levelUpgradeToVM.Keys.ToList())
            {
                if (levelUpgrade.GetLevelUpgradeState() != LevelUpgrade.LevelUpgradeState.Pickable)
                {
                    continue;
                }
                if (levelUpgrade.levelUpgradeSO.getBlockedBy.Contains(levelUpgradeSO))
                {
                    levelUpgrade.BlockLevelUpgrade();
                }
            }
        }

        public void UpdateUI()
        {
            foreach (var levelUpgrade in levelUpgradeToVM.Keys.ToList())
            {
                levelUpgradeToVM[levelUpgrade].button.SetText(levelUpgrade.GetShortDescription());
                levelUpgradeToVM[levelUpgrade].button.SetButtonState(levelUpgrade.GetLevelUpgradeState().ToButtonState());
            }
        }

        public void OnApplyLevelUpgrade(Component sender, object data)
        {
            if (sender.gameObject == this.gameObject)
                return;
            if (sender is ButtonViewModel buttonViewModel)
            {
                vMToLevelUpgrade[buttonViewModel]?.SelectLevelUpgrade();
                BlockUpgrades(vMToLevelUpgrade[buttonViewModel].levelUpgradeSO);
                UpdateUI();
            }
        }

        public void OnLevelReset(Component sender, object data)
        {
            foreach (var levelUpgrade in levelUpgradeToVM.Keys.ToList())
            {
                levelUpgrade.ResetLevelUpgrade();
            }
        }

        public void OnUpdateUI(Component sender, object data)
        {
            if (data is string id)
            {
                if (id == windowId)
                {
                    UpdateUI();
                }
            }
        }
    }
}
