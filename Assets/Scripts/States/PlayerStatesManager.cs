using System.Collections.Generic;
using UnityEngine;

public sealed class PlayerStatesManager : StatesManager    
{

    #region Overrides

    protected override bool IdlingTrigger() => timer.Finished;
    protected override bool CrouchingTrigger() => Input.GetKey(KeyCode.S);
    protected override bool StandInputCheck() => !Input.GetKey(KeyCode.S);
    protected override bool CoveringTrigger() => Input.GetKey(KeyCode.E);
    protected override bool UncoveringTrigger() => !Input.GetKey(KeyCode.E);
    protected override bool LaunchingTrigger() => Input.GetKeyDown(KeyCode.W);
    protected override bool FallingTrigger() => timer.Finished;
    protected override bool LandingTrigger() => landed;
    protected override bool FlippingLeftTrigger() => Input.GetKeyDown(KeyCode.A);
    protected override bool FlippingRightTrigger() => Input.GetKeyDown(KeyCode.D);
    protected override bool RunningTrigger() => Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D);
    protected override bool AttackingTrigger() => Input.GetKeyDown(KeyCode.Q);
    protected override bool ThrowingTrigger() => Input.GetKeyDown(KeyCode.Space);

    #endregion

}
