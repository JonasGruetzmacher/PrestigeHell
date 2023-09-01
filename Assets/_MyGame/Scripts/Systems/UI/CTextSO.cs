using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;


[CreateAssetMenu(fileName = "Text", menuName = "CustomUI/TextSO")]
public class CTextSO : SerializedScriptableObject
{
    public CThemeSO theme;

    public TMP_FontAsset font;
    public float fontSize;
}
