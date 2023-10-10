
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEditor;

namespace LeroGames.PrestigeHell
{
    public class CButton : CUIComponent
    {
        public Style style;
        public UnityEvent onClick;

        protected Button button;
        protected TextMeshProUGUI buttonText;

        public override void Setup()
        {
            button = GetComponentInChildren<Button>();
            buttonText = button.GetComponentInChildren<TextMeshProUGUI>();
        }

        public override void Configure()
        {
            CThemeSO theme = GetThemeSO();
            if (theme == null) { return; }

            ColorBlock colorBlock = button.colors;
            colorBlock.normalColor = theme.GetBackgroundColor(style);
            button.colors = colorBlock;

            buttonText.color = theme.GetTextColor(style);
        }

        public virtual void SetText(string text)
        {
            buttonText.text = text;
        }

        public virtual void SetButtonState(ButtonState buttonState)
        {
            switch (buttonState)
            {
                case ButtonState.Hidden:
                    Hide();
                    break;
                case ButtonState.Activated:
                    Activate();
                    break;
                case ButtonState.Selected:
                    Selected();
                    break;
                case ButtonState.Blocked:
                    Block();
                    break;
                case ButtonState.Disabled:
                    Dissable();
                    break;
            }
        }

        public virtual void Hide()
        {
            button.gameObject.SetActive(false);
        }

        public virtual void Activate()
        {
            button.gameObject.SetActive(true);
            button.interactable = true;
        }

        public virtual void Block()
        {
            button.interactable = false;
            button.gameObject.SetActive(true);
        }

        public virtual void Dissable()
        {
            button.interactable = false;
        }

        public virtual void Enable()
        {
            button.interactable = true;
        }

        public virtual void Selected()
        {
            button.interactable = false;
        }

        public virtual void OnClick()
        {
            onClick.Invoke();
        }

        public enum ButtonState
        {
            Hidden = 0,
            Activated = 1,
            Selected = 2,
            Blocked = 3,
            Disabled = 4,
        }
        
    }
}