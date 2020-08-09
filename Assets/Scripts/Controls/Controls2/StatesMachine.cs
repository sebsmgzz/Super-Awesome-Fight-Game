using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Component to be added to the gameobject as a controller
/// </summary>
public class StatesMachine : MonoBehaviour
{

    #region Fields

    // components
    private Animator animator;
    private Rigidbody2D rb2d;
    private Timer statesTimer;

    // config
    private float runForceMagnitude;
    private float jumpForceMagnitude;
    private float maxRunVelocity;

    // flags
    private bool landed = false;

    // states
    private State2 currentState = new State2();
    private Dictionary<StateName, State2> states = new Dictionary<StateName, State2>()
    {
        { StateName.Idling, new State2() },
        { StateName.Crouching, new State2() },
        { StateName.Standing, new State2() },
        { StateName.Covering, new State2() },
        { StateName.Uncovering, new State2() },
        { StateName.Launching, new State2() },
        { StateName.Falling, new State2() },
        { StateName.Landing, new State2() },
        { StateName.Attacking, new State2() },
        { StateName.FlippingLeft, new State2() },
        { StateName.FlippingRight, new State2() },
        { StateName.Running, new State2() },
        { StateName.Throwing, new State2() }
    };

    #endregion

    #region Properties

    #endregion

    #region Unity API

    private void Start()
    {
        // components
        animator = gameObject.GetComponent<Animator>();
        rb2d = gameObject.GetComponent<Rigidbody2D>();
        statesTimer = gameObject.AddComponent<Timer>();
        // config
        runForceMagnitude = ConfigurationManager.RunForceMagnitude;
        jumpForceMagnitude = ConfigurationManager.JumpForceMagnitude;
        maxRunVelocity = 2;
        // initialize
        currentState = states[StateName.Idling];
    }

    private void Update()
    {
        foreach(KeyValuePair<StateName,State2> pair in states)
        {
            if(pair.Value == currentState)
            {
                Debug.Log(pair.Key);
            }
        }
        currentState.Update();
        State2 nextState = currentState.NextState();
        if (nextState != null)
        {
            currentState = nextState;
        }
        else if(!currentState.Active)
        {
            currentState = states[StateName.Idling];
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if( collision.gameObject.CompareTag(GameConstants.PlataformTag)
        ||  collision.gameObject.CompareTag(GameConstants.EnemyTag)
        ||  collision.gameObject.CompareTag(GameConstants.PlayerTag) )
        {
            landed = true;
        }
    }

    #endregion

    #region Methods

    /// <summary>
    /// Adds a triggerer to the given state
    /// </summary>
    /// <param name="stateName">The state to which give a trigger</param>
    /// <param name="triggerer">The triggerer</param>
    public void AddTriggerer(StateName stateName, Func<bool> triggerer)
    {
        states[stateName].Triggerer = triggerer;
    }

    /// <summary>
    /// Builds the states fields
    /// </summary>
    public void BuildStates()
    {
        // StateName.Idling
        states[StateName.Idling].Triggerer = IdlingTriggerer;
        states[StateName.Idling].OnEnter = IdlingOnEnter;
        states[StateName.Idling].AddNeighbor(states[StateName.Crouching]);
        states[StateName.Idling].AddNeighbor(states[StateName.Covering]);
        states[StateName.Idling].AddNeighbor(states[StateName.Launching]);
        states[StateName.Idling].AddNeighbor(states[StateName.Attacking]);
        states[StateName.Idling].AddNeighbor(states[StateName.FlippingLeft]);
        states[StateName.Idling].AddNeighbor(states[StateName.FlippingRight]);
        states[StateName.Idling].AddNeighbor(states[StateName.Throwing]);
        // StateName.Crouching
        states[StateName.Crouching].OnEnter = CrouchingOnEnter;
        states[StateName.Crouching].AddNeighbor(states[StateName.Standing]);
        // StateName.Standing
        states[StateName.Standing].OnEnter = StandingOnEnter;
        states[StateName.Standing].AddNeighbor(states[StateName.Idling]);
        // StateName.Covering
        states[StateName.Covering].OnEnter = CoveringOnEnter;
        states[StateName.Covering].AddNeighbor(states[StateName.Uncovering]);
        // StateName.Uncovering
        states[StateName.Uncovering].OnEnter = UncoveringOnEnter;
        states[StateName.Uncovering].AddNeighbor(states[StateName.Idling]);
        // StateName.Launching
        states[StateName.Launching].OnEnter = LaunchingOnEnter;
        states[StateName.Launching].AddNeighbor(states[StateName.Falling]);
        // StateName.Falling
        states[StateName.Falling].OnEnter = FallingOnEnter;
        states[StateName.Falling].AddNeighbor(states[StateName.Landing]);
        // StateName.Landing
        states[StateName.Landing].Triggerer = LandingTriggerer;
        states[StateName.Landing].OnEnter = LandingOnEnter;
        states[StateName.Landing].AddNeighbor(states[StateName.Idling]);
        states[StateName.Landing].AddNeighbor(states[StateName.FlippingLeft]);
        states[StateName.Landing].AddNeighbor(states[StateName.FlippingRight]);
        // StateName.Attacking
        states[StateName.Attacking].OnEnter = AttackingOnEnter;
        states[StateName.Attacking].AddNeighbor(states[StateName.Idling]);
        // StateName.FlippingLeft
        states[StateName.FlippingLeft].AddNeighbor(states[StateName.Running]);
        states[StateName.FlippingLeft].OnEnter = FlippingLeftOnEnter;
        // StateName.FlippingRight
        states[StateName.FlippingRight].AddNeighbor(states[StateName.Running]);
        states[StateName.FlippingRight].OnEnter = FlippingRightOnEnter;
        // StateName.Running
        states[StateName.Running].AddNeighbor(states[StateName.FlippingLeft]);
        states[StateName.Running].AddNeighbor(states[StateName.FlippingRight]);
        states[StateName.Running].AddNeighbor(states[StateName.Running]);
        states[StateName.Running].AddNeighbor(states[StateName.Idling]);
        states[StateName.Running].AddNeighbor(states[StateName.Launching]);
        states[StateName.Running].OnEnter = RunningOnEnter;
        states[StateName.Running].Action = RunningAction;
        states[StateName.Running].SelfDeactivates = true;
        // StateName.Throwing
        states[StateName.Throwing].AddNeighbor(states[StateName.Idling]);
        states[StateName.Throwing].OnEnter = ThrowingOnEnter;
    }

    #endregion

    #region States

    private bool IdlingTriggerer()
    {
        return statesTimer.Finished;
    }
    private void IdlingOnEnter()
    {
        animator.Play(GameConstants.Idling.animatorName);
    }

    private void CrouchingOnEnter()
    {
        animator.Play(GameConstants.Crouching.animatorName);
        rb2d.constraints =
            RigidbodyConstraints2D.FreezeRotation |
            RigidbodyConstraints2D.FreezePositionX;
    }

    private void StandingOnEnter()
    {
        animator.Play(GameConstants.Standing.animatorName);
        rb2d.constraints = RigidbodyConstraints2D.FreezeRotation;
        statesTimer.Duration = GameConstants.Standing.Length;
        statesTimer.Run();
        Debug.Log("Running");
    }
    private void StandingOnExit()
    {

    }

    private void CoveringOnEnter()
    {
        animator.Play(GameConstants.Covering.AnimatorName);
    }

    private void  UncoveringOnEnter()
    {
        animator.Play(GameConstants.Uncovering.AnimatorName);
        statesTimer.Duration = GameConstants.Uncovering.Length;
        statesTimer.Run();
    }

    private void LaunchingOnEnter()
    {
        animator.Play(GameConstants.Launching.animatorName);
        rb2d.AddForce(
            new Vector2(0, jumpForceMagnitude),
            ForceMode2D.Impulse);
        statesTimer.Duration = GameConstants.Launching.Length;
        statesTimer.Run();
    }

    private void FallingOnEnter()
    {
        animator.Play(GameConstants.Falling.AnimatorName);
        landed = false;
    }

    private bool LandingTriggerer()
    {
        return landed;
    }
    private void LandingOnEnter()
    {
        animator.Play(GameConstants.Landing.AnimatorName);
        landed = false;
        statesTimer.Duration = GameConstants.Landing.Length;
        statesTimer.Run();
    }

    private void FlippingLeftOnEnter()
    {
        float localScaleX = gameObject.transform.localScale.x;
        if (localScaleX < 0)
        {
            localScaleX *= -1;
        }
        gameObject.transform.localScale = new Vector3(
            localScaleX,
            gameObject.transform.localScale.y,
            gameObject.transform.localScale.z);
    }

    private void FlippingRightOnEnter()
    {
        float localScaleX = gameObject.transform.localScale.x;
        if(localScaleX > 0)
        {
            localScaleX *= -1;
        }
        gameObject.transform.localScale = new Vector3(
            localScaleX,
            gameObject.transform.localScale.y,
            gameObject.transform.localScale.z);
    }

    private void RunningOnEnter()
    {
        animator.Play(GameConstants.Running.AnimatorName);
    }
    private void RunningAction()
    {
        if (Mathf.Abs(rb2d.velocity.x) < maxRunVelocity)
        {
            rb2d.AddForce(
                new Vector2(-gameObject.transform.localScale.x * runForceMagnitude, 0),
                ForceMode2D.Impulse);
        }
        statesTimer.Duration = GameConstants.Running.Length / 2f;
        statesTimer.Run();
    }

    private void AttackingOnEnter()
    {
        animator.Play(GameConstants.Attacking.AnimatorName);
        statesTimer.Duration = GameConstants.Attacking.Length;
        statesTimer.Run();
    }

    private void ThrowingOnEnter()
    {
        animator.Play(GameConstants.Throwing.AnimatorName);
        statesTimer.Duration = GameConstants.Throwing.Length;
        statesTimer.Run();
    }

    #endregion

    #region Enum

    public enum StateName
    {
        Idling,
        Crouching,
        Defending,
        Standing,
        Covering,
        Uncovering,
        Launching,
        Falling,
        Landing,
        Attacking,
        FlippingLeft,
        FlippingRight,
        Running,
        Throwing
    }

    #endregion

}
