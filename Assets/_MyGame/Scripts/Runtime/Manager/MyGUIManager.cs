using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.TopDownEngine;
using MoreMountains.Tools;
using TMPro;
using System.Resources;
using UnityEngine.EventSystems;

namespace LeroGames.PrestigeHell
{
	public class MyGUIManager : GUIManager, MMEventListener<GameEvent>
	{
		[Header("Panels")]
		public GameObject levelUpPanel;
		public GameObject statisticPanel;
		public GameObject enemySelectionPanel;

		[Header("Buttons")]
		public GameObject levelUpButton;
		public GameObject enemySelectionButton;

		[Header("Events")]
		[SerializeField] private Tools.GameEvent onUpdateUI;
		public MMProgressBar XPBar;
		public MMProgressBar dangerBar;
		[SerializeField] private MMSerializableDictionary<ResourceType, MMProgressBar> resourceBars = new MMSerializableDictionary<ResourceType, MMProgressBar>();
		[SerializeField] private MMSerializableDictionary<ResourceType, TextMeshProUGUI> resourceTexts = new MMSerializableDictionary<ResourceType, TextMeshProUGUI>();

		protected override void Awake()
		{
			base.Awake();
		}

		protected override void Start()
		{
			base.Start();
			StartCoroutine(UpdateResourceBarCoroutine(ResourceType.Danger));
			SetLevelUpPanel(false);
			SetStatisticPanel(false);
			SetEnemySelectionPanel(false);
			levelUpButton.SetActive(false);
		}

		public virtual void UpdateBar(MMProgressBar bar, float currentXP,float minXP,float maxXP,string playerID)
		{
			if (bar == null) { return; }

			if (bar.PlayerID == playerID)
			{
				bar.UpdateBar(currentXP,minXP,maxXP);
			}
		}

		public virtual void UpdateText(TextMeshProUGUI text, string textToDisplay)
		{
			if (text == null) { return; }
			text.text = textToDisplay;
		}

		public virtual void UpdateText(ResourceType resourceType, string textToDisplay)
		{
			if (!resourceTexts.ContainsKey(resourceType)){ return; }
			UpdateText(resourceTexts[resourceType], textToDisplay);
		}

		public virtual void UpdateResourceBar(ResourceType resourceType)
		{
			switch(resourceType)
			{
				case ResourceType.Danger:
					UpdateBar(dangerBar, DangerManager.Instance.GetDangerProgress(),0,100,"Player1");
					break;
				case ResourceType.LevelPoints:
					UpdateBar(XPBar, ResourcesManager.Instance.GetResourceAmount(resourceType),0,ResourcesManager.Instance.GetNextLevelRequirement(),"Player1");
					break;
			}
		}

		public virtual void UpdateResourceText(ResourceType resourceType)
		{
			switch(resourceType)
			{
				case ResourceType.Danger:
					UpdateText(resourceType, "Danger: " + ResourcesManager.Instance.GetResourceAmountInt(resourceType));
					break;
				case ResourceType.XP:
					UpdateText(resourceType, "XP: " + ResourcesManager.Instance.GetResourceAmountInt(resourceType));
					break;
				case ResourceType.Level:
					UpdateText(resourceType, "Level: " + ResourcesManager.Instance.GetResourceAmountInt(resourceType));
					break;
			}
		}

		private IEnumerator UpdateResourceBarCoroutine(ResourceType resourceType)
		{
			while(true)
			{
				UpdateResourceBar(resourceType);
				yield return new WaitForSeconds(0.1f);
			}
		}

		public virtual void SetLevelUpPanel(bool state)
		{
			if (levelUpPanel != null)
				{
					levelUpPanel.SetActive(state);
					EventSystem.current.sendNavigationEvents = state;
					onUpdateUI.Raise(this, "LevelUpPanel");
				}
		}

		public virtual void SetStatisticPanel(bool state)
		{
			if (statisticPanel != null)
			{
				statisticPanel.SetActive(state);
				EventSystem.current.sendNavigationEvents = state;
			}
		}

		public virtual void SetEnemySelectionPanel(bool state)
		{
			if (enemySelectionPanel != null)
			{
				enemySelectionPanel.SetActive(state);
				EventSystem.current.sendNavigationEvents = state;
			}
		}

		public virtual void SetLevelUpButton(bool state)
		{
			if (levelUpButton != null)
			{
				levelUpButton.SetActive(state);
			}
		}

		public virtual void SetEnemySelectionButton(bool state)
		{
			if (enemySelectionButton != null)
			{
				enemySelectionButton.SetActive(state);
			}
		}

		public virtual void OnMMEvent(GameEvent eventType)
		{
			if (eventType.eventName == Eventname.LevelUp)
			{
				SetLevelUpButton(true);
			}
		}

		private void OnEnable()
		{
			this.MMEventStartListening<GameEvent>();
		}

		private void OnDisable()
		{
			this.MMEventStopListening<GameEvent>();
		}
			
	}
}