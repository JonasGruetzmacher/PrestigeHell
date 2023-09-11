using System.Collections;
using System.Collections.Generic;
using MoreMountains.Tools;
using MoreMountains.TopDownEngine;
using UnityEngine;

public class MyInputManager : InputManager
{
    public MMInput.IMButton LevelUpgradesButton;

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }

    protected override void InitializeButtons()
    {
        base.InitializeButtons();
        ButtonList.Add(LevelUpgradesButton = new MMInput.IMButton(PlayerID, "LevelUpgrades", LevelUpgradesButtonDown, LevelUpgradesButtonPressed, LevelUpgradesButtonUp));
    }

    public virtual void LevelUpgradesButtonDown() {LevelUpgradesButton.State.ChangeState(MMInput.ButtonStates.ButtonDown);}
    public virtual void LevelUpgradesButtonPressed() {LevelUpgradesButton.State.ChangeState(MMInput.ButtonStates.ButtonPressed);}
    public virtual void LevelUpgradesButtonUp() {LevelUpgradesButton.State.ChangeState(MMInput.ButtonStates.ButtonUp);}

}
