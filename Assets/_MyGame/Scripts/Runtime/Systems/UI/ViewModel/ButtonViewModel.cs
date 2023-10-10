using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LeroGames.PrestigeHell
{
    public class ButtonViewModel : MonoBehaviour
    {
        public CButton button;

        [Header("Event")]
        public Tools.GameEvent onClick;

        public void OnButtonStateChanged(Component sender, object data)
        {
            if (data is CButton.ButtonState buttonState)
            {
                button.SetButtonState(buttonState);
            }
        }

        public void OnTextUpdate(Component sender, object data)
        {
            if (data is string text)
            {
                button.SetText(text);
            }
        }

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