using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public sealed class PlayerController : Controller, IPlayerStartedStateInvoker
{

    #region Fields

    private PlayerStartedState playerStateChangedEvent = new PlayerStartedState();

    #endregion

    #region UnityAPI

    private void Awake()
    {
        EventsManager.AddInvoker((IPlayerStartedStateInvoker)this);
    }

    #endregion

    #region States Controls

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

    #region Events

    protected override void HandleStateChangedEvent(StateName stateName)
    {
        playerStateChangedEvent.Invoke(stateName);
    }

    #endregion

    #region IPlayerStartedStateInvoker

    public void AddPlayerStartedStateListener(UnityAction<StateName> listener)
    {
        playerStateChangedEvent.AddListener(listener);
    }

    #endregion

}
