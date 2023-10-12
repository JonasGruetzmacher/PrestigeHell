using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace LeroGames.PrestigeHell
{
    [CreateAssetMenu(fileName = "View", menuName = "CustomUI/ViewSO")]
    public class CViewSO : SerializedScriptableObject
    {
        public RectOffset padding;
        public float spacing;
    }
}