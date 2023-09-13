using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterInformation", menuName = "Character/InformationSO")]
public class CharacterInformationSO : SerializedScriptableObject
{
    public Stats stats;
    public string characterName;
    public Sprite characterSprite;
}
