using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LeroGames.PrestigeHell
{
    [CreateAssetMenu(fileName = "StatType", menuName = "PrestigeHell/StatType")]
    public class StatType : ScriptableObject
    {
        public string markedString;
        public string ToMarkedString()
        {
            if (!string.IsNullOrEmpty(markedString))
            {
                return string.Format("{0}`", markedString);
            }
            return base.ToString();
        }
        public override string ToString()
        {
            return name;
        }
    }
}