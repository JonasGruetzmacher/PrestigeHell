using System.Collections;
using System.Collections.Generic;
using MoreMountains.Tools;
using UnityEngine;

public class TooltipManager : MMSingleton<TooltipManager>
{
    public Tooltip tooltip;

    void Start()
    {
        tooltip.gameObject.SetActive(false);
    }

    public static void Show(string content, string header = "")
    {
        Instance.tooltip.SetText(content, header);
        Instance.tooltip.gameObject.SetActive(true);
    }

    public static void Hide()
    {
        Instance.tooltip.gameObject.SetActive(false);
    }
}
