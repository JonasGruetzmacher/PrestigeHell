using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using LeroGames.Tools;

namespace LeroGames.PrestigeHell
{
    public class EnemyStatsVisual : CUIComponent, ICharacterInformation, ITooltipInformation
    {
        private Image image;

        [field: SerializeField] public CharacterInformationSO characterInformation {get; private set;}


        public override void Setup()
        {
            image = GetComponent<Image>();   
        }

        public override void Configure()
        {
            if (characterInformation == null)
            {
                return;
            }
            image.sprite = characterInformation.characterSprite;
        }

        public void SetCharacterInformation(CharacterInformationSO characterInformation)
        {
            this.characterInformation = characterInformation;
            Init();
        }

        public void GetTooltipInformation(out string infoLeft, out string infoRight)
        {
            characterInformation.GetTooltipInformation(out infoLeft, out infoRight);
            infoLeft += string.Format("!Spawn Chance`: {0}\n", EnemyManager.Instance.GetSpawnChance(characterInformation).ToString("0.00"));
            infoRight += string.Format("Danger: {0}", ResourcesManager.Instance.GetResourceAmount(ResourceType.Danger));
        }
    }
}