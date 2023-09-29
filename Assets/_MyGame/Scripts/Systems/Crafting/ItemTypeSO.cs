using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemType", menuName = "Crafting/Item/ItemTypeSO")]
public class ItemTypeSO : ScriptableObject
{
    public string typeName;
    public string typeDescription;
    public Sprite typeSprite;
}
