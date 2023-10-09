using System.Collections;
using System.Collections.Generic;
// using LeroGames.StatSystem;
using LeroGames.Tools;
using Sirenix.OdinInspector;
using UnityEngine;

namespace LeroGames.PrestigeHell
{
    [CreateAssetMenu(fileName = "CharacterInformation", menuName = "Character/InformationSO")]
    public class CharacterInformationSO : SerializedScriptableObject, ITooltipInformation
    {
        public Stats stats;
        public string characterName;
        public Sprite characterSprite;

        public GameObject characterPrefab;
        public int poolSize = 20;

        public void GetTooltipInformation(out string infoLeft, out string infoRight)
        {
            infoRight = "";
            infoLeft = string.Format("~{0}`\n", characterName);
            
            stats.GetTooltipInformation(out string statsInfoLeft, out string statsInfoRight);
            infoLeft += statsInfoLeft;
            infoRight += statsInfoRight;
        }
    }
}