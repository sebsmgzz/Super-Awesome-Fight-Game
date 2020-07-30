using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class Enemy : Fighter
{

    #region Fields

    // player
    GameObject playerGameObject;
    Controller playerController;

    // control
    Action updateCheck;
    private Dictionary<State.Case, Action> updateChecks =
        new Dictionary<State.Case, Action>();
    private Dictionary<State.Case, Action> baseStatesTriggeredHandlers =
        new Dictionary<State.Case, Action>();
    private Dictionary<State.Case, bool> statesFlags = 
        new Dictionary<State.Case, bool>()
    {
        { State.Case.Idling, false },
        { State.Case.Crouching, false },
        { State.Case.Standing, false },
        { State.Case.Covering, false },
        { State.Case.Uncovering, false },
        { State.Case.Launching, false },
        { State.Case.Falling, false },
        { State.Case.Landing, false },
        { State.Case.FlippingLeft, false },
        { State.Case.FlippingRight, false },
        { State.Case.Running, false },
        { State.Case.Attacking, false },
        { State.Case.Throwing, false },
    };

    // config
    private float nearDistanceTriggerer = 2f;

    #endregion

    #region Properties

    public override Name FighterName => Name.Enemy;

    protected override float MaxVelocity => 2f;

    #endregion

    #region Unity API

    protected override void Awake()
    {
        // base call
        base.Awake();
        // events
        EventsManager.AddPlayerStartedStateListener(HandlePlayerStartedState);
    }

    protected override void Start()
    {
        // base call
        base.Start();
        // player
        playerGameObject = GameObject.FindGameObjectWithTag(GameConstants.PlayerTag);
        playerController = playerGameObject.GetComponent<Controller>();
        // initialize fields
        Initialize();
        updateCheck = updateChecks[State.Case.Idling];
    }

    protected override void Update()
    {
        // base call
        base.Update();
        // update check
        updateCheck?.Invoke();
    }

    #endregion

    #region Methods

    private void Initialize()
    {
        // base states
        baseStatesTriggeredHandlers.Add(State.Case.Idling, base.IdleOnEnter);
        baseStatesTriggeredHandlers.Add(State.Case.Crouching, base.CrouchingOnEnter);
        baseStatesTriggeredHandlers.Add(State.Case.Standing, base.StandOnEnter);
        baseStatesTriggeredHandlers.Add(State.Case.Covering, base.CoveringOnEnter);
        baseStatesTriggeredHandlers.Add(State.Case.Uncovering, base.UncoveringOnEnter);
        baseStatesTriggeredHandlers.Add(State.Case.Launching, base.LaunchingOnEnter);
        baseStatesTriggeredHandlers.Add(State.Case.Falling, base.FallingOnEnter);
        baseStatesTriggeredHandlers.Add(State.Case.Landing, base.LandingOnEnter);
        baseStatesTriggeredHandlers.Add(State.Case.Attacking, base.AttackingOnEnter);
        baseStatesTriggeredHandlers.Add(State.Case.FlippingLeft, base.FlippingLeftOnEnter);
        baseStatesTriggeredHandlers.Add(State.Case.FlippingRight, base.FlippingRightOnEnter);
        baseStatesTriggeredHandlers.Add(State.Case.Running, base.RunningOnEnter);
        baseStatesTriggeredHandlers.Add(State.Case.Throwing, base.ThrowingOnEnter);
        // update Checks
        updateChecks.Add(State.Case.Idling, WhileIdling);
        updateChecks.Add(State.Case.Crouching, WhileCrouching);
        updateChecks.Add(State.Case.Standing, WhileIdling);
        updateChecks.Add(State.Case.Covering, WhileCovering);
        updateChecks.Add(State.Case.Uncovering, WhileUncovering);
        updateChecks.Add(State.Case.Launching, WhileLaunching);
        updateChecks.Add(State.Case.Falling, WhileFalling);
        updateChecks.Add(State.Case.Landing, WhileLanding);
        updateChecks.Add(State.Case.Attacking, WhileAttacking);
        updateChecks.Add(State.Case.FlippingLeft, WhileFlippingLeft);
        updateChecks.Add(State.Case.FlippingRight, WhileFlippingRight);
        updateChecks.Add(State.Case.Running, WhileRunning);
        updateChecks.Add(State.Case.Throwing, WhileThrowing);
    }

    private void StateTriggered(State.Case stateName)
    {
        // base call
        baseStatesTriggeredHandlers[stateName].Invoke();
        // updateCheck update
        updateCheck = updateChecks[stateName];
    }

    private void StateOnEnter(State.Case stateName)
    {
        // base call
        baseStatesTriggeredHandlers[stateName].Invoke();
        // action
        Debug.Log(stateName);
        statesFlags[stateName] = false;
        updateCheck = updateChecks[stateName];
    }

    /// <summary>
    /// Changes all flags to the given condition
    /// </summary>
    /// <param name="flagCondition">The condition for all flags to have</param>
    private void SetAllFlags(bool flagCondition)
    {
        foreach(State.Case key in statesFlags.Keys)
        {
            statesFlags[key] = flagCondition;
        }
    }

    /// <summary>
    /// Determines if the player if near the enemy
    /// </summary>
    /// <returns>True when player is within nearDistanceTriggerer</returns>
    private bool PlayerIsNear()
    {
        Vector3 playerPosition = playerGameObject.transform.position;
        Vector3 enemyPosition = gameObject.transform.position;
        float distance = Mathf.Sqrt(
            Mathf.Pow(playerPosition.x - enemyPosition.x, 2) +
            Mathf.Pow(playerPosition.y - enemyPosition.y, 2));
        return distance < nearDistanceTriggerer;
    }

    #endregion

    #region Player Responses

    /// <summary>
    /// Handles a change in state on the player
    /// </summary>
    /// <param name="stateName">The state entered by the player</param>
    private void HandlePlayerStartedState(State.Case stateName)
    {
        //Debug.Log(stateName);
        //switch(stateName)
        //{
        //    case State.Case.Idling:
        //        RunToPlayer();
        //        break;
        //}
    }

    #endregion

    #region Controls

    protected override bool IdlingTrigger() => statesFlags[State.Case.Idling];
    protected override void IdleOnEnter() => StateOnEnter(State.Case.Idling);

    protected override bool CrouchingTrigger() => statesFlags[State.Case.Crouching];
    protected override void CrouchingOnEnter() => StateOnEnter(State.Case.Crouching);

    protected override bool StandInputCheck() => statesFlags[State.Case.Standing];
    protected override void StandOnEnter() => StateOnEnter(State.Case.Standing);

    protected override bool CoveringTrigger() => statesFlags[State.Case.Covering];
    protected override void CoveringOnEnter() => StateOnEnter(State.Case.Covering);

    protected override bool UncoveringTrigger() => statesFlags[State.Case.Uncovering];
    protected override void UncoveringOnEnter() => StateOnEnter(State.Case.Uncovering);

    protected override bool LaunchingTrigger() => statesFlags[State.Case.Launching];
    protected override void LaunchingOnEnter() => StateOnEnter(State.Case.Launching);

    protected override bool FallingTrigger() => statesFlags[State.Case.Falling];
    protected override void FallingOnEnter() => StateOnEnter(State.Case.Falling);

    protected override bool LandingTrigger() => statesFlags[State.Case.Landing];
    protected override void LandingOnEnter() => StateOnEnter(State.Case.Landing);

    protected override bool FlippingLeftTrigger() => statesFlags[State.Case.FlippingLeft];
    protected override void FlippingLeftOnEnter() => StateOnEnter(State.Case.FlippingLeft);

    protected override bool FlippingRightTrigger() => statesFlags[State.Case.FlippingRight];
    protected override void FlippingRightOnEnter() => StateOnEnter(State.Case.FlippingRight);

    protected override bool RunningTrigger() => statesFlags[State.Case.Running];
    protected override void RunningOnEnter() => StateOnEnter(State.Case.Running);

    protected override bool AttackingTrigger() => statesFlags[State.Case.Attacking];
    protected override void AttackingOnEnter() => StateOnEnter(State.Case.Attacking);

    protected override bool ThrowingTrigger() => statesFlags[State.Case.Throwing];
    protected override void ThrowingOnEnter() => StateOnEnter(State.Case.Throwing);


    #endregion

    #region WhileOnState

    private void WhileIdling()
    {
        // check run controls
        if (!playerController.Falling)
        {
            bool playerIsToRightOfEnemy =
                gameObject.transform.position.x < playerGameObject.transform.position.x;
            statesFlags[State.Case.FlippingLeft] = !playerIsToRightOfEnemy;
            statesFlags[State.Case.FlippingRight] = playerIsToRightOfEnemy;
            statesFlags[State.Case.Running] = true;
        }
        statesFlags[State.Case.Attacking] = PlayerIsNear();
    }
    private void WhileCrouching() { }
    private void WhileStanding() { }
    private void WhileCovering() { }
    private void WhileUncovering() { }
    private void WhileLaunching() { }
    private void WhileFalling() { }
    private void WhileLanding() { }
    private void WhileFlippingLeft() { }
    private void WhileFlippingRight() { }
    private void WhileRunning()
    {
        statesFlags[State.Case.Running] = true;
        if (PlayerIsNear())
        {
            statesFlags[State.Case.Running] = false;
            statesFlags[State.Case.Idling] = true;
        }
        else if (!playerController.Falling)
        {
            bool facingRight = gameObject.transform.localScale.x == -1;
            bool playerLocatedRight =
                gameObject.transform.position.x < playerGameObject.transform.position.x;
            statesFlags[State.Case.FlippingLeft] = facingRight && !playerLocatedRight;
            statesFlags[State.Case.FlippingRight] = !facingRight && playerLocatedRight;
        }
    }
    private void WhileAttacking()
    {
        statesFlags[State.Case.Idling] = statesTimer.Finished;
    }
    private void WhileThrowing() { }

    #endregion

}
