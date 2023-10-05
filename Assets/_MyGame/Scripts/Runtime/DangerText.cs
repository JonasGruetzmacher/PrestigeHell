using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace LeroGames.PrestigeHell
{
    public class DangerText : MonoBehaviour
    {
        [SerializeField] private TextMeshPro text;

        public void SetDangerText()
        {
            text.text = ResourcesManager.Instance.GetResourceAmount(ResourceType.Danger).ToString();
        }
    }
}