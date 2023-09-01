using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;


[CreateAssetMenu(fileName = "View", menuName = "CustomUI/ViewSO")]
public class CViewSO : SerializedScriptableObject
{
    public CThemeSO themeSO;
    public RectOffset padding;
    public float spacing;
}
