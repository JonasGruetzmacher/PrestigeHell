using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting;

public class TooltipInformation : MonoBehaviour, ITooltipInformation
{
    public Object tooltipInformationObject;

    private ITooltipInformation tooltipInformation;

    private void Awake()
    {
        try
        {
            tooltipInformation = tooltipInformationObject as ITooltipInformation;
        }
        catch (System.Exception)
        {
            Debug.LogError("TooltipInformationObject is not of type ITooltipInformation");
            throw;
        }
    }
    public void GetTooltipInformation(out string infoLeft, out string infoRight)
    {
        tooltipInformation.GetTooltipInformation(out infoLeft, out infoRight);
    }

}
