using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR;
using MoreMountains.Tools;
using MoreMountains.TopDownEngine;
using UnityEditor;

public class DebugController : MonoBehaviour
{
    public bool showConsole = false;
    public bool showHelp = false;

    private string input = "";
    private string lastInput;

    public static DebugCommand KILL_ALL;
    public static DebugCommand SPAWN_ENEMY;
    public static DebugCommand<int> SET_XP;
    public static DebugCommand HELP;
    public static DebugCommand<ResourceType, int> SET_RESOURCE;
    public static DebugCommand IRONWOOD;

    public List<object> commandList;

    private Vector2 scroll;


    public void OnToggleDebug(InputValue value)
    {
        showConsole = !showConsole;
    }

    public void OnReturn(InputValue value)
    {
        Debug.Log("Return");
        if (showConsole)
        {
            HandleInput();
            Debug.Log(input);
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

    private void Awake()
    {
        KILL_ALL = new DebugCommand("kill_all", "Kill all enemies", "kill_all", () => { EnemyManager.Instance.KillAllEnemies(); });
        SPAWN_ENEMY = new DebugCommand("spawn_enemy", "Spawn an enemy", "spawn_enemy", () => { EnemyManager.Instance.GetEnemySpawner().SpawnRandomEnemy(); });
        SET_XP = new DebugCommand<int>("set_xp", "Set player xp", "set_xp [xp]", (xp) => { ResourcesManager.Instance.SetResource(ResourceType.XP, xp); });
        HELP = new DebugCommand("help", "Show all commands", "help", () => { showHelp = !showHelp; });
        SET_RESOURCE = new DebugCommand<ResourceType, int>("set_resource", "Set resource", "set_resource [resource type] [value]", (resource, value) => { ResourcesManager.Instance.SetResource(resource, value); });
        IRONWOOD = new DebugCommand("ironwood", "Exalted forever number 1", "ironwood", () => 
        {
            LevelManager.Instance.Players[0].GetComponentInChildren<SpriteRenderer>().sprite = Resources.Load<Sprite>("Characters/Ironwood");
            LevelManager.Instance.Players[0].GetComponentInChildren<Animator>().enabled = false;
            Debug.Log("Exalted forever number 1");
            Debug.Log(Resources.Load<Sprite>("Characters/Ironwood"));
            Debug.Log(LevelManager.Instance.Players[0].GetComponentInChildren<SpriteRenderer>().sprite);
        });

        commandList = new List<object>
        {
            KILL_ALL,
            SPAWN_ENEMY,
            SET_XP,
            HELP,
            SET_RESOURCE,
            IRONWOOD,
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

    private void HandleInput()
    {
        string[] properties = input.Split(' ');

        for (int i = 0; i < commandList.Count; i++)
        {
            DebugCommandBase commandBase = commandList[i] as DebugCommandBase;

            if (input.Contains(commandBase.commandId))
            {
                if((commandList[i] as DebugCommand).commandId == commandBase.commandId)
                {
                    (commandList[i] as DebugCommand).Invoke();
                    return;
                }
                else if(commandList[i] as DebugCommand<int> != null)
                {
                    int value = int.Parse(properties[1]);
                    (commandList[i] as DebugCommand<int>).Invoke(value);
                    return;
                }
                else if(commandList[i] as DebugCommand<ResourceType, int> != null)
                {
                    ResourceType resource = (ResourceType)System.Enum.Parse(typeof(ResourceType), properties[1]);
                    int value = int.Parse(properties[2]);
                    (commandList[i] as DebugCommand<ResourceType, int>).Invoke(resource, value);
                    return;
                }
                else
                {
                    Debug.Log("Command not found");
                    return;
                }
            }
        }
    }
}
