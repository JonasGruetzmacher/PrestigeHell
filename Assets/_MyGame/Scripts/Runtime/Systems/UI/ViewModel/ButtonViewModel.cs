using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LeroGames.PrestigeHell
{
    public class ButtonViewModel : MonoBehaviour
    {
        public CButton button;

        [Header("Event")]
        public LeroGames.Tools.GameEvent onClick;

        private void OnEnable()
        {
            button.onClick.AddListener(OnClick);
        }

        private void OnDisable()
        {
            button.onClick.RemoveListener(OnClick);
        }

        public void OnClick()
        {
            onClick.Raise(this, "Button clicked");
        }
    }
}