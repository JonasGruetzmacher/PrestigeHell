using System.Collections;
using System.Collections.Generic;
using MoreMountains.TopDownEngine;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;


public class TooltipTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public string header;

    [Multiline]
    public string content;



    public virtual void OnPointerEnter(PointerEventData eventData)
    {
        TooltipManager.Show(content, header);
        Debug.Log("OnPointerEnter");
    }

    public virtual void OnPointerExit(PointerEventData eventData)
    {
        TooltipManager.Hide();
        Debug.Log("OnPointerExit");
    }
}
