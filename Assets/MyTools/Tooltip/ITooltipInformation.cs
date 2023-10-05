using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace LeroGames.Tools
{
    public interface ITooltipInformation
    {
        public void GetTooltipInformation(out string infoLeft, out string infoRight);
    }
}