using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LeroGames.PrestigeHell
{
    public class TextViewModel : MonoBehaviour
    {
        public CText textView;

        public void OnTextChanged(Component sender, object data)
        {
            Debug.Log(sender + " " + data);
            if (data is string text)
            {
                textView.SetText(text);
            }
        }
    }
}