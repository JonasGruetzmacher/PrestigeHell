using System.Collections;
using System.Collections.Generic;
using LeroGames.Tools;
using UnityEngine;
using UnityEngine.Scripting;

namespace LeroGames.Tools
{
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
}