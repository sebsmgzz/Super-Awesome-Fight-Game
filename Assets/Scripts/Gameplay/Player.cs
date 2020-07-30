using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : Fighter
{

    #region Fields

    // states triggered handlers
    private Dictionary<State.Case, Action> baseStatesTriggeredHandlers =
        new Dictionary<State.Case, Action>();

    // events
    private PlayerStartedState playerStartedStateEvent = new PlayerStartedState();

    #endregion

    #region Properties

    public override Name FighterName => Name.Player;

    protected override float MaxVelocity => 5f;

    #endregion

    #region Unity API

    protected override void Awake()
    {
        // base call
        base.Awake();
        // events
        EventsManager.AddPlayerStartedStateInvoker(this);
    }

    protected override void Start()
    {
        // base call
        base.Start();
        // initialize fields
        baseStatesTriggeredHandlers.Add(State.Case.Idling, base.IdleOnEnter);
        baseStatesTriggeredHandlers.Add(State.Case.Crouching, base.CrouchingOnEnter);
        baseStatesTriggeredHandlers.Add(State.Case.Standing, base.StandOnEnter);
        baseStatesTriggeredHandlers.Add(State.Case.Covering, base.CoveringOnEnter);
        baseStatesTriggeredHandlers.Add(State.Case.Uncovering, base.UncoveringOnEnter);
        baseStatesTriggeredHandlers.Add(State.Case.Launching, base.LaunchingOnEnter);
        baseStatesTriggeredHandlers.Add(State.Case.Falling, base.FallingOnEnter);
        baseStatesTriggeredHandlers.Add(State.Case.Landing, base.LandingOnEnter);
        baseStatesTriggeredHandlers.Add(State.Case.FlippingLeft, base.FlippingLeftOnEnter);
        baseStatesTriggeredHandlers.Add(State.Case.FlippingRight, base.FlippingRightOnEnter);
        baseStatesTriggeredHandlers.Add(State.Case.Running, base.RunningOnEnter);
        baseStatesTriggeredHandlers.Add(State.Case.Attacking, base.AttackingOnEnter);
        baseStatesTriggeredHandlers.Add(State.Case.Throwing, base.ThrowingOnEnter);
    }

    protected override void Update()
    {
        // base call
        base.Update();
    }

    #endregion

    #region Methods

    /// <summary>
    /// Handles the trigger of a state start
    /// </summary>
    /// <param name="stateName">The state triggered</param>
    private void StateTriggered(State.Case stateName)
    {
        // base call
        baseStatesTriggeredHandlers[stateName].Invoke();
        // events
        playerStartedStateEvent.Invoke(stateName);
    }

    /// <summary>
    /// Adds a listener to the start of a state
    /// </summary>
    /// <param name="listener">The listener the the start of a state</param>
    public void AddPlayerStartedStateListener(UnityAction<State.Case> listener)
    {
        playerStartedStateEvent.AddListener(listener);
    }

    #endregion

    #region Idle States

    protected override bool IdlingTrigger() => statesTimer.Finished;
    protected override void IdleOnEnter() => StateTriggered(State.Case.Idling);

    #endregion

    #region Crouch States

    protected override bool CrouchingTrigger() => Input.GetKey(KeyCode.S);
    protected override void CrouchingOnEnter() => StateTriggered(State.Case.Crouching);

    protected override bool StandInputCheck() => !Input.GetKey(KeyCode.S);
    protected override void StandOnEnter() => StateTriggered(State.Case.Standing);

    #endregion

    #region Defend States

    protected override bool CoveringTrigger() => Input.GetKey(KeyCode.E);
    protected override void CoveringOnEnter() => StateTriggered(State.Case.Covering);

    protected override bool UncoveringTrigger() => !Input.GetKey(KeyCode.E);
    protected override void UncoveringOnEnter() => StateTriggered(State.Case.Uncovering);

    #endregion

    #region Jump States

    protected override bool LaunchingTrigger() => Input.GetKeyDown(KeyCode.W);
    protected override void LaunchingOnEnter() => StateTriggered(State.Case.Launching);

    protected override bool FallingTrigger() => statesTimer.Finished;
    protected override void FallingOnEnter() => StateTriggered(State.Case.Falling);

    protected override bool LandingTrigger() => landed;
    protected override void LandingOnEnter() => StateTriggered(State.Case.Landing);

    #endregion

    #region Run States

    protected override bool FlippingLeftTrigger() => Input.GetKeyDown(KeyCode.A);
    protected override void FlippingLeftOnEnter() => StateTriggered(State.Case.FlippingLeft);

    protected override bool FlippingRightTrigger() => Input.GetKeyDown(KeyCode.D);
    protected override void FlippingRightOnEnter() => StateTriggered(State.Case.FlippingRight);

    protected override bool RunningTrigger() => Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D);
    protected override void RunningOnEnter() => StateTriggered(State.Case.Running);

    #endregion

    #region Attack States

    protected override bool AttackingTrigger() => Input.GetKeyDown(KeyCode.Q);
    protected override void AttackingOnEnter() => StateTriggered(State.Case.Attacking);

    #endregion

    #region Throwing States

    protected override bool ThrowingTrigger() => Input.GetKeyDown(KeyCode.Space);
    protected override void ThrowingOnEnter() => StateTriggered(State.Case.Throwing);

    #endregion

}
