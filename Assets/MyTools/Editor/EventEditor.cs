using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace LeroGames.Tools
{
    public class EventEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            GUI.enabled = Application.isPlaying;

            GameEvent e = target as GameEvent;
            if (GUILayout.Button("Raise"))
            {
                e.Raise(null, null);
            }
        }
    }
}
