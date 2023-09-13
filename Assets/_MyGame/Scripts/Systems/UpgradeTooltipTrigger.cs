using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.EventSystems;


public class UpgradeTooltipTrigger : TooltipTrigger
{
    private Upgrade upgrade;

    public override void OnPointerEnter(PointerEventData eventData)
    {
        GetUpgrade();
        header = upgrade.name;
        content = upgrade.description;
        base.OnPointerEnter(eventData);
    }

    private void GetUpgrade()
    {
        upgrade = GetComponent<IUpgrade>()?.upgrade;
    }
}
