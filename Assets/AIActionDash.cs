using System.Collections;
using System.Collections.Generic;
using MoreMountains.Tools;
using MoreMountains.TopDownEngine;
using UnityEngine;

public class AIActionDash : AIAction
{
    protected CharacterDamageDash2D _characterDamageDash2D;
    public override void Initialization()
    {
        if(!ShouldInitialize) return;
        base.Initialization();
        _characterDamageDash2D = this.gameObject.GetComponentInParent<Character>()?.FindAbility<CharacterDamageDash2D>();
    }

    public override void PerformAction()
    {
        if (_characterDamageDash2D == null)
        {
            return;
        }
        _characterDamageDash2D.DashStart();
    }
}
