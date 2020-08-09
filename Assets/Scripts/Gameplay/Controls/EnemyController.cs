using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public sealed class EnemyController : Controller
{

    #region Fields

    private GameObject playerGameObject;
    private float nearDistanceTriggerer = 2.2f;

    #endregion

    #region Unity API

    private void Awake()
    {
        EventsManager.AddPlayerStartedStateListener(HandlePlayerStartedStateEvent);
    }

    private void Start()
    {
        playerGameObject = GameObject.FindGameObjectWithTag(GameConstants.PlayerTag);
    }

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

    #region States Actions

    private void IdlingAction()
    {
        // check run controls
        //if (!PlayerIsNear())
        //{
        //    bool playerIsToRightOfEnemy =
        //        gameObject.transform.position.x < playerGameObject.transform.position.x;
        //    statesFlags[State.Case.FlippingLeft] = !playerIsToRightOfEnemy;
        //    statesFlags[State.Case.FlippingRight] = playerIsToRightOfEnemy;
        //    statesFlags[State.Case.Running] = true;
        //}
        //else
        //{
        //    statesFlags[State.Case.Attacking] = true;
        //}
    }

    private void RunningAction()
    {
        //statesFlags[State.Case.Running] = true;
        //if (PlayerIsNear())
        //{
        //    statesFlags[State.Case.Running] = false;
        //    statesFlags[State.Case.Idling] = true;
        //}
        //else
        //{
        //    bool facingRight = gameObject.transform.localScale.x == -1;
        //    bool playerLocatedRight =
        //        gameObject.transform.position.x < playerGameObject.transform.position.x;
        //    statesFlags[State.Case.FlippingLeft] = facingRight && !playerLocatedRight;
        //    statesFlags[State.Case.FlippingRight] = !facingRight && playerLocatedRight;
        //}
    }

    private void AttackingAction()
    {
        //statesFlags[State.Case.Idling] = statesTimer.Finished;
    }

    #endregion

    #region Event Handlers

    private void HandlePlayerStartedStateEvent(Controller.StateName stateName)
    {
    }

    protected override void HandleStateChangedEvent(StateName stateName)
    {
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

    #endregion

}
