using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public interface ITooltipInformation
{
    public void GetTooltipInformation(out string infoLeft, out string infoRight);
}
