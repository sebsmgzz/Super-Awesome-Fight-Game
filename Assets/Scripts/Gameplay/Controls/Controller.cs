using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class Controller : MonoBehaviour
{

    #region Fields

    // components
    private Animator animator;
    private Rigidbody2D rb2d;
    protected Timer statesTimer;

    // state machine
    private StatesMachine<StateName> statesMachine;
    private Dictionary<StateName, State<StateName>> states =
        new Dictionary<StateName, State<StateName>>();

    // config
    private float runForceMagnitude;
    private float jumpForceMagnitude;
    private float maxRunVelocity;

    // flags
    private bool landed = false;

    #endregion

    #region Properties

    public float RunForceMagnitude
    {
        get { return runForceMagnitude; }
        set { runForceMagnitude = value; }
    }

    public float JumpForceMagnitude
    {
        get { return jumpForceMagnitude; }
        set { jumpForceMagnitude = value; }
    }

    public float MaxRunVelocity
    {
        get { return maxRunVelocity; }
        set { maxRunVelocity = value; }
    }

    public StateName CurrentState
    {
        get { return statesMachine.CurrentState.Value; }
    }

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
        // states
        BuildStatesMachine();
    }

    private void Update()
    {
        statesMachine.Update();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(GameConstants.PlataformTag)
        || collision.gameObject.CompareTag(GameConstants.EnemyTag)
        || collision.gameObject.CompareTag(GameConstants.PlayerTag))
        {
            landed = true;
        }
    }

    #endregion

    #region Methods

    /// <summary>
    /// Builds and configures the default states in the states machine
    /// </summary>
    private void BuildStatesMachine()
    {
        // add states
        statesMachine = new StatesMachine<StateName>(StateName.Idling);
        List<StateName> stateNames = Enum.GetValues(typeof(StateName)).OfType<StateName>().ToList<StateName>();
        stateNames.Remove(StateName.Idling);
        states.Add(StateName.Idling, statesMachine.Find(StateName.Idling));
        foreach (StateName state in stateNames)
        {
            statesMachine.AddState(state);
            states.Add(state, statesMachine.Find(state));
        }
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
        states[StateName.Crouching].Triggerer = CrouchingTriggerer;
        states[StateName.Crouching].OnEnter = CrouchingOnEnter;
        states[StateName.Crouching].AddNeighbor(states[StateName.Standing]);
        // StateName.Standing
        states[StateName.Standing].Triggerer = StandingTriggerer;
        states[StateName.Standing].OnEnter = StandingOnEnter;
        states[StateName.Standing].AddNeighbor(states[StateName.Idling]);
        // StateName.Covering
        states[StateName.Covering].Triggerer = CoveringTriggerer;
        states[StateName.Covering].OnEnter = CoveringOnEnter;
        states[StateName.Covering].AddNeighbor(states[StateName.Uncovering]);
        // StateName.Uncovering
        states[StateName.Uncovering].Triggerer = UncoveringTriggerer;
        states[StateName.Uncovering].OnEnter = UncoveringOnEnter;
        states[StateName.Uncovering].AddNeighbor(states[StateName.Idling]);
        // StateName.Launching
        states[StateName.Launching].Triggerer = LaunchingTriggerer;
        states[StateName.Launching].OnEnter = LaunchingOnEnter;
        states[StateName.Launching].AddNeighbor(states[StateName.Falling]);
        // StateName.Falling
        states[StateName.Falling].Triggerer = FallingTriggerer;
        states[StateName.Falling].OnEnter = FallingOnEnter;
        states[StateName.Falling].AddNeighbor(states[StateName.Landing]);
        // StateName.Landing
        states[StateName.Landing].Triggerer = LandingTriggerer;
        states[StateName.Landing].OnEnter = LandingOnEnter;
        states[StateName.Landing].AddNeighbor(states[StateName.Idling]);
        states[StateName.Landing].AddNeighbor(states[StateName.FlippingLeft]);
        states[StateName.Landing].AddNeighbor(states[StateName.FlippingRight]);
        // StateName.Attacking
        states[StateName.Attacking].Triggerer = AttackingTriggerer;
        states[StateName.Attacking].OnEnter = AttackingOnEnter;
        states[StateName.Attacking].AddNeighbor(states[StateName.Idling]);
        // StateName.FlippingLeft
        states[StateName.FlippingLeft].Triggerer = FlippingLeftTriggerer;
        states[StateName.FlippingLeft].OnEnter = FlippingLeftOnEnter;
        states[StateName.FlippingLeft].AddNeighbor(states[StateName.Running]);
        // StateName.FlippingRight
        states[StateName.FlippingRight].Triggerer = FlippingRightTrigger;
        states[StateName.FlippingRight].OnEnter = FlippingRightOnEnter;
        states[StateName.FlippingRight].AddNeighbor(states[StateName.Running]);
        // StateName.Running
        states[StateName.Running].Triggerer = RunningTriggerer;
        states[StateName.Running].OnEnter = RunningOnEnter;
        states[StateName.Running].Action = RunningAction;
        states[StateName.Running].SelfDeactivates = true;
        states[StateName.Running].AddNeighbor(states[StateName.FlippingLeft]);
        states[StateName.Running].AddNeighbor(states[StateName.FlippingRight]);
        states[StateName.Running].AddNeighbor(states[StateName.Running]);
        states[StateName.Running].AddNeighbor(states[StateName.Idling]);
        states[StateName.Running].AddNeighbor(states[StateName.Launching]);
        // StateName.Throwing
        states[StateName.Throwing].Triggerer = ThrowingTriggerer;
        states[StateName.Throwing].OnEnter = ThrowingOnEnter;
        states[StateName.Throwing].AddNeighbor(states[StateName.Idling]);
        // Event
        statesMachine.AddStateChangedListener(HandleStateChangedEvent);
    }

    #endregion

    #region Events

    protected abstract void HandleStateChangedEvent(StateName stateName);

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

    #region States Controls

    protected bool IdlingTriggerer() => statesTimer.Finished;
    private void IdlingOnEnter()
    {
        animator.Play(GameConstants.Idling.animatorName);
    }

    protected abstract bool CrouchingTriggerer();
    private void CrouchingOnEnter()
    {
        animator.Play(GameConstants.Crouching.animatorName);
        rb2d.constraints =
            RigidbodyConstraints2D.FreezeRotation |
            RigidbodyConstraints2D.FreezePositionX;
    }

    protected abstract bool StandingTriggerer();
    private void StandingOnEnter()
    {
        animator.Play(GameConstants.Standing.animatorName);
        rb2d.constraints = RigidbodyConstraints2D.FreezeRotation;
        statesTimer.Duration = GameConstants.Standing.Length;
        statesTimer.Run();
    }

    protected abstract bool CoveringTriggerer();
    private void CoveringOnEnter()
    {
        animator.Play(GameConstants.Covering.AnimatorName);
    }

    protected abstract bool UncoveringTriggerer();
    private void UncoveringOnEnter()
    {
        animator.Play(GameConstants.Uncovering.AnimatorName);
        statesTimer.Duration = GameConstants.Uncovering.Length;
        statesTimer.Run();
    }

    protected abstract bool LaunchingTriggerer();
    private void LaunchingOnEnter()
    {
        animator.Play(GameConstants.Launching.animatorName);
        rb2d.AddForce(
            new Vector2(0, jumpForceMagnitude),
            ForceMode2D.Impulse);
        statesTimer.Duration = GameConstants.Launching.Length;
        statesTimer.Run();
    }

    private bool FallingTriggerer() => statesTimer.Finished;
    private void FallingOnEnter()
    {
        animator.Play(GameConstants.Falling.AnimatorName);
        landed = false;
    }

    private bool LandingTriggerer() => landed;
    private bool LandingTriggererer()
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

    protected abstract bool FlippingLeftTriggerer();
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

    protected abstract bool FlippingRightTrigger();
    private void FlippingRightOnEnter()
    {
        float localScaleX = gameObject.transform.localScale.x;
        if (localScaleX > 0)
        {
            localScaleX *= -1;
        }
        gameObject.transform.localScale = new Vector3(
            localScaleX,
            gameObject.transform.localScale.y,
            gameObject.transform.localScale.z);
    }

    protected abstract bool RunningTriggerer();
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

    protected abstract bool AttackingTriggerer();
    private void AttackingOnEnter()
    {
        animator.Play(GameConstants.Attacking.AnimatorName);
        statesTimer.Duration = GameConstants.Attacking.Length;
        statesTimer.Run();
    }

    protected abstract bool ThrowingTriggerer();
    private void ThrowingOnEnter()
    {
        animator.Play(GameConstants.Throwing.AnimatorName);
        statesTimer.Duration = GameConstants.Throwing.Length;
        statesTimer.Run();
    }

    #endregion

}
