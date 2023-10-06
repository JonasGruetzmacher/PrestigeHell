using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
// using MoreMountains.TopDownEngine;
using UnityEditor;
// using MoreMountains.InventoryEngine;
using System.Linq;

namespace LeroGames.Tools
{
    [RequireComponent(typeof(PlayerInput))]
    public class DebugController : MonoBehaviour
    {

        public bool showConsole = false;
        public bool showHelp = false;

        private string input = "";
        private string lastInput;


        public List<object> commandList;

        
        private Vector2 scroll;


        public void OnToggleDebug(InputValue value)
        {
            showConsole = !showConsole;
            input = "";
        }

        public void OnReturn(InputValue value)
        {
            if (showConsole)
            {
                HandleInput();
                lastInput = input;
                input = "";
            }
        }

        public void OnArrowUp(InputValue value)
        {
            if (showConsole)
            {
                input = lastInput;
            }
        }

        public void AddCommand(DebugCommandBase command)
        {
            commandList.Add(command);
        }

        protected virtual void Awake()
        {



            commandList = new List<object>
            {
                // KILL_ALL,
                // SPAWN_ENEMY,
                // SET_XP,
                // HELP,
                // SET_RESOURCE,
                // IRONWOOD,
                // ADD_ITEM,
                // UNLOCK,
                // SET_STATISTIC,
                // HARD_RESET,
            };
        }

        private void OnGUI()
        {
            if (!showConsole) { return;}

            float y = 0f;

            if (showHelp)
            {
                GUI.Box(new Rect(0, y, Screen.width, 100), "");

                Rect viewport = new Rect(0, 0, Screen.width - 30, 20 * commandList.Count);

                scroll = GUI.BeginScrollView(new Rect(0, y + 5, Screen.width, 90), scroll, viewport);

                for (int i = 0; i < commandList.Count; i++)
                {
                    DebugCommandBase commandBase = commandList[i] as DebugCommandBase;

                    string label = $"{commandBase.commandId} - {commandBase.commandDescription}";
                    Rect labelRect = new Rect(5, 20 * i, viewport.width-100, 20);
                    GUI.Label(labelRect, label);
                }

                GUI.EndScrollView();

                y += 100;
            }

            GUI.Box(new Rect(0, y, Screen.width, 30), "");
            GUI.backgroundColor = new Color(0, 0, 0, 0);
            GUI.SetNextControlName("Console");
            input = GUI.TextField(new Rect(10f, y + 5, Screen.width - 20, 20f), input);
            GUI.FocusControl("Console");
        }

        protected virtual void HandleInput()
        {
            string[] properties = input.Split(' ');

            for (int i = 0; i < commandList.Count; i++)
            {
                DebugCommandBase commandBase = commandList[i] as DebugCommandBase;

                if (input.Contains(commandBase.commandId))
                {
                    if((commandList[i] as DebugCommand) != null)
                    {
                        (commandList[i] as DebugCommand).Invoke();
                        return;
                    }
                    else if(commandList[i] as DebugCommand<int> != null)
                    {
                        if (properties.Length < 2) { Debug.Log("Invalid command format"); return; }
                        int value = int.Parse(properties[1]);
                        (commandList[i] as DebugCommand<int>).Invoke(value);
                        return;
                    }
                    else if(commandList[i] as DebugCommand<string> != null)
                    {
                        if (properties.Length < 2) { Debug.Log("Invalid command format"); return; }
                        string value = properties[1];
                        (commandList[i] as DebugCommand<string>).Invoke(value);
                        return;
                    }
                    // else if(commandList[i] as DebugCommand<ResourceType, int> != null)
                    // {
                    //     if (properties.Length < 3) { Debug.Log("Invalid command format"); return; }
                    //     ResourceType resource = (ResourceType)System.Enum.Parse(typeof(ResourceType), properties[1]);
                    //     int value = int.Parse(properties[2]);
                    //     (commandList[i] as DebugCommand<ResourceType, int>).Invoke(resource, value);
                    //     return;
                    // }
                    else if(commandList[i] as DebugCommand<string, int> != null)
                    {
                        if (properties.Length < 3) { Debug.Log("Invalid command format"); return; }
                        string itemName = properties[1];
                        int quantity = int.Parse(properties[2]);
                        (commandList[i] as DebugCommand<string, int>).Invoke(itemName, quantity);
                        return;
                    }
                }
            }
            Debug.Log("Command not found");
            return;
        }
    }
}
