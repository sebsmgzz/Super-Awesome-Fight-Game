using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public sealed class EnemyController : Controller
{

    #region Fields

    private GameObject playerGameObject;
    private Player player;
    private float nearDistanceTriggerer;
    private Dictionary<FighterState, bool> flags;

    #endregion

    #region Unity API

    private void Awake()
    {
        AddControllerChangedStateListener(HandleControllerChangedState);
    }

    protected override void Start()
    {
        // base call to construct default states machine
        base.Start();
        // config
        nearDistanceTriggerer = ConfigurationManager.NearDistanceTriggerer;
        // find player
        playerGameObject = GameObject.FindGameObjectWithTag(Game.Constants.GameObjectsTags[Tag.Player]);
        player = playerGameObject.GetComponent<Player>();
        // setup flags
        flags = new Dictionary<FighterState, bool>();
        foreach(FighterState state in Enum.GetValues(typeof(FighterState)))
        {
            flags.Add(state, false);
        }
        // add enemy implementation
        states[FighterState.Idling].Action = IdlingAction;
        states[FighterState.Running].SelfDeactivates = false;
        states[FighterState.Running].Action = RunningAction;
    }

    #endregion

    #region States Triggerers

    protected override bool IdlingTriggerer() => flags[FighterState.Idling];
    protected override bool CrouchingTriggerer() => flags[FighterState.Crouching];
    protected override bool StandingTriggerer() => flags[FighterState.Standing];
    protected override bool CoveringTriggerer() => flags[FighterState.Covering];
    protected override bool UncoveringTriggerer() => flags[FighterState.Uncovering];
    protected override bool LaunchingTriggerer() => flags[FighterState.Launching];
    protected override bool FlippingLeftTriggerer() => flags[FighterState.FlippingLeft];
    protected override bool FlippingRightTrigger() => flags[FighterState.FlippingRight];
    protected override bool RunningTriggerer() => flags[FighterState.Running];
    protected override bool AttackingTriggerer() => flags[FighterState.Attacking];
    protected override bool ThrowingTriggerer() => flags[FighterState.Throwing];

    #endregion

    #region States Actions

    private void IdlingAction()
    {
        // check run controls
        if (PlayerIsNear())
        {
            flags[FighterState.Attacking] = true;
        }
        else if (PlayerIsRunningAway())
        {
            flags[FighterState.Throwing] = true;
        }
        else
        {
            bool playerIsToRightOfEnemy =
                gameObject.transform.position.x < playerGameObject.transform.position.x;
            flags[FighterState.FlippingLeft] = !playerIsToRightOfEnemy;
            flags[FighterState.FlippingRight] = playerIsToRightOfEnemy;
            flags[FighterState.Running] = true;
        }
    }

    protected override void RunningAction()
    {
        base.RunningAction();
        if (PlayerIsNear())
        {
            flags[FighterState.Attacking] = true;
        }
        else
        {
            bool facingRight = gameObject.transform.localScale.x == -1;
            bool playerLocatedRight =
                gameObject.transform.position.x < playerGameObject.transform.position.x;
            bool facingPlayer = (facingRight && playerLocatedRight) ||
                                (!facingRight && !playerLocatedRight);
            if(!facingPlayer)
            {
                flags[FighterState.FlippingLeft] = facingRight && !playerLocatedRight;
                flags[FighterState.FlippingRight] = !facingRight && playerLocatedRight;
                flags[FighterState.Running] = true;
            }
        }
    }

    #endregion

    #region Event Handlers

    private void HandleControllerChangedState(FighterState previousState, FighterState nextState)
    {
        flags[previousState] = false;
        flags[nextState] = false;
    }

    protected override void HandleTimerFinishedEvent()
    {
        switch(statesMachine.CurrentState.Value)
        {
            case FighterState.Attacking:
            case FighterState.Throwing:
                flags[FighterState.Idling] = true;
                break;
        }
    }

    #endregion

    #region Methods

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

    /// <summary>
    /// Determines if the player is within a chicken
    /// </summary>
    /// <returns>True when player is chicken, false otherwise</returns>
    private bool PlayerIsRunningAway()
    {
        if(player.CurrentState == FighterState.Running)
        {
            bool playerFacingRight = playerGameObject.transform.localScale.x < 0;
            float playerPosition = playerGameObject.transform.localPosition.x;
            float enemyPosition = gameObject.transform.localPosition.x;
            bool playerIsToRight = playerPosition > enemyPosition;
            if( (playerFacingRight && playerIsToRight) ||
                (!playerFacingRight && !playerIsToRight) )
            {
                return true;
            }

        }
        return false;
    }

    #endregion

}
