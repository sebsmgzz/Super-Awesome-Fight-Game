using UnityEngine;

public sealed class PlayerController : Controller
{

    #region Fields

    #endregion

    #region UnityAPI

    #endregion

    #region States Triggerers

    protected override bool CrouchingTriggerer() => Input.GetKey(KeyCode.S);
    protected override bool StandingTriggerer() => !Input.GetKey(KeyCode.S);
    protected override bool CoveringTriggerer() => Input.GetKey(KeyCode.E);
    protected override bool UncoveringTriggerer() => !Input.GetKey(KeyCode.E);
    protected override bool LaunchingTriggerer() => Input.GetKeyDown(KeyCode.W);
    protected override bool FlippingLeftTriggerer() => Input.GetKeyDown(KeyCode.A);
    protected override bool FlippingRightTrigger() => Input.GetKeyDown(KeyCode.D);
    protected override bool RunningTriggerer() => Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D);
    protected override bool AttackingTriggerer() => Input.GetKeyDown(KeyCode.Q);
    protected override bool ThrowingTriggerer() => Input.GetKeyDown(KeyCode.Space);

    #endregion

    #region Events Handlers

    protected override void HandleTimerFinishedEvent()
    {
        
    }

    #endregion

}
