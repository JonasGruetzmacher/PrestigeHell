using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.TopDownEngine;
using MoreMountains.Tools;

public class MyGUIManager : GUIManager
{
    /// the health bars to update
	[Tooltip("the XP bars to update")]
	public MMProgressBar[] XPBars;

	protected override void Awake()
	{
		base.Awake();
	}

    public virtual void UpdateXPBar(float currentXP,float minXP,float maxXP,string playerID)
	{
		if (XPBars == null) { return; }
		if (XPBars.Length <= 0)	{ return; }

		foreach (MMProgressBar xpBar in XPBars)
		{
			if (xpBar == null) { continue; }
			if (xpBar.PlayerID == playerID)
			{
				xpBar.UpdateBar(currentXP,minXP,maxXP);
			}
		}
	}

	public virtual void UpdateResourceBar(ResourceType resourceType)
	{
		
		if (resourceType == ResourceType.XP)
		{
			Debug.Log(ResourcesManager.Instance.GetResourceAmount(resourceType));
			UpdateXPBar(ResourcesManager.Instance.GetResourceAmount(resourceType),0,100,"Player1");
		}
	}

	// protected void OnEnable()
	// {
	// 	this.MMEventStartListening<ResourceEvent>();
	// }
		
}
