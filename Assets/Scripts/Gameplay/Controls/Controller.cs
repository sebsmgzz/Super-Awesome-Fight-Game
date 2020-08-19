using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public abstract class Controller : MonoBehaviour
{

    #region Fields

    // events
    private ControllerChangedState controllerChangedStateEvent =
        new ControllerChangedState();

    // components
    private Animator animator;
    private Rigidbody2D rb2d;
    protected Timer statesTimer;

    // state machine
    protected StatesMachine<FighterState> statesMachine;
    protected Dictionary<FighterState, State<FighterState>> states =
        new Dictionary<FighterState, State<FighterState>>();

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

    public FighterState CurrentState
    {
        get { return statesMachine.CurrentState.Value; }
    }

    #endregion

    #region Unity API

    protected virtual void Start()
    {
        // components
        animator = gameObject.GetComponent<Animator>();
        rb2d = gameObject.GetComponent<Rigidbody2D>();
        statesTimer = gameObject.AddComponent<Timer>();
        statesTimer.AddTimerFinishedListener(HandleTimerFinishedEvent);
        // config
        runForceMagnitude = ConfigurationManager.RunForceMagnitude;
        jumpForceMagnitude = ConfigurationManager.JumpForceMagnitude;
        maxRunVelocity = 2;
        // initialize states machine
        BuildStates();
    }

    protected virtual void Update()
    {
        statesMachine.Update();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(Game.Constants.GameObjectsTags[Tag.Plataform])
        || collision.gameObject.CompareTag(Game.Constants.GameObjectsTags[Tag.Enemy])
        || collision.gameObject.CompareTag(Game.Constants.GameObjectsTags[Tag.Player]) )
        {
            landed = true;
        }
    }

    #endregion

    #region Methods

    /// <summary>
    /// Builds and configures the default states in the states machine
    /// </summary>
    private void BuildStates()
    {
        // add states
        statesMachine = new StatesMachine<FighterState>(FighterState.Idling);
        List<FighterState> stateNames = Enum.GetValues(typeof(FighterState)).OfType<FighterState>().ToList<FighterState>();
        stateNames.Remove(FighterState.Idling);
        states.Add(FighterState.Idling, statesMachine.Find(FighterState.Idling));
        foreach (FighterState state in stateNames)
        {
            statesMachine.AddState(state);
            states.Add(state, statesMachine.Find(state));
        }
        // StateName.Idling
        states[FighterState.Idling].Triggerer = IdlingTriggerer;
        states[FighterState.Idling].OnEnter = IdlingOnEnter;
        states[FighterState.Idling].AddNeighbor(states[FighterState.Crouching]);
        states[FighterState.Idling].AddNeighbor(states[FighterState.Covering]);
        states[FighterState.Idling].AddNeighbor(states[FighterState.Launching]);
        states[FighterState.Idling].AddNeighbor(states[FighterState.Attacking]);
        states[FighterState.Idling].AddNeighbor(states[FighterState.FlippingLeft]);
        states[FighterState.Idling].AddNeighbor(states[FighterState.FlippingRight]);
        states[FighterState.Idling].AddNeighbor(states[FighterState.Throwing]);
        // StateName.Crouching
        states[FighterState.Crouching].Triggerer = CrouchingTriggerer;
        states[FighterState.Crouching].OnEnter = CrouchingOnEnter;
        states[FighterState.Crouching].AddNeighbor(states[FighterState.Standing]);
        // StateName.Standing
        states[FighterState.Standing].Triggerer = StandingTriggerer;
        states[FighterState.Standing].OnEnter = StandingOnEnter;
        states[FighterState.Standing].AddNeighbor(states[FighterState.Idling]);
        // StateName.Covering
        states[FighterState.Covering].Triggerer = CoveringTriggerer;
        states[FighterState.Covering].OnEnter = CoveringOnEnter;
        states[FighterState.Covering].AddNeighbor(states[FighterState.Uncovering]);
        // StateName.Uncovering
        states[FighterState.Uncovering].Triggerer = UncoveringTriggerer;
        states[FighterState.Uncovering].OnEnter = UncoveringOnEnter;
        states[FighterState.Uncovering].AddNeighbor(states[FighterState.Idling]);
        // StateName.Launching
        states[FighterState.Launching].Triggerer = LaunchingTriggerer;
        states[FighterState.Launching].OnEnter = LaunchingOnEnter;
        states[FighterState.Launching].AddNeighbor(states[FighterState.Falling]);
        // StateName.Falling
        states[FighterState.Falling].Triggerer = FallingTriggerer;
        states[FighterState.Falling].OnEnter = FallingOnEnter;
        states[FighterState.Falling].AddNeighbor(states[FighterState.Landing]);
        // StateName.Landing
        states[FighterState.Landing].Triggerer = LandingTriggerer;
        states[FighterState.Landing].OnEnter = LandingOnEnter;
        states[FighterState.Landing].AddNeighbor(states[FighterState.Idling]);
        states[FighterState.Landing].AddNeighbor(states[FighterState.FlippingLeft]);
        states[FighterState.Landing].AddNeighbor(states[FighterState.FlippingRight]);
        // StateName.Attacking
        states[FighterState.Attacking].Triggerer = AttackingTriggerer;
        states[FighterState.Attacking].OnEnter = AttackingOnEnter;
        states[FighterState.Attacking].AddNeighbor(states[FighterState.Idling]);
        // StateName.FlippingLeft
        states[FighterState.FlippingLeft].Triggerer = FlippingLeftTriggerer;
        states[FighterState.FlippingLeft].OnEnter = FlippingLeftOnEnter;
        states[FighterState.FlippingLeft].AddNeighbor(states[FighterState.Running]);
        // StateName.FlippingRight
        states[FighterState.FlippingRight].Triggerer = FlippingRightTrigger;
        states[FighterState.FlippingRight].OnEnter = FlippingRightOnEnter;
        states[FighterState.FlippingRight].AddNeighbor(states[FighterState.Running]);
        // StateName.Running
        states[FighterState.Running].Triggerer = RunningTriggerer;
        states[FighterState.Running].OnEnter = RunningOnEnter;
        states[FighterState.Running].Action = RunningAction;
        states[FighterState.Running].SelfDeactivates = true;
        states[FighterState.Running].AddNeighbor(states[FighterState.FlippingLeft]);
        states[FighterState.Running].AddNeighbor(states[FighterState.FlippingRight]);
        states[FighterState.Running].AddNeighbor(states[FighterState.Running]);
        states[FighterState.Running].AddNeighbor(states[FighterState.Idling]);
        states[FighterState.Running].AddNeighbor(states[FighterState.Launching]);
        states[FighterState.Running].AddNeighbor(states[FighterState.Attacking]);
        states[FighterState.Running].AddNeighbor(states[FighterState.Throwing]);
        // StateName.Throwing
        states[FighterState.Throwing].Triggerer = ThrowingTriggerer;
        states[FighterState.Throwing].OnEnter = ThrowingOnEnter;
        states[FighterState.Throwing].AddNeighbor(states[FighterState.Idling]);
        // Event
        statesMachine.AddStateChangedListener(HandleStateChangedEvent);
    }

    #endregion

    #region Events

    public void AddControllerChangedStateListener(UnityAction<FighterState, FighterState> listener)
    {
        controllerChangedStateEvent.AddListener(listener);
    }

    private void HandleStateChangedEvent(FighterState previousState, FighterState nextState)
    {
        controllerChangedStateEvent.Invoke(previousState, nextState);
    }

    protected abstract void HandleTimerFinishedEvent();

    #endregion

    #region States Controls

    protected virtual bool IdlingTriggerer() => statesTimer.Finished;
    protected virtual void IdlingOnEnter()
    {
        animator.Play(Game.AnimationsInfo.Idling.animatorName);
    }

    protected abstract bool CrouchingTriggerer();
    protected virtual void CrouchingOnEnter()
    {
        animator.Play(Game.AnimationsInfo.Crouching.animatorName);
        rb2d.constraints =
            RigidbodyConstraints2D.FreezeRotation |
            RigidbodyConstraints2D.FreezePositionX;
    }

    protected abstract bool StandingTriggerer();
    protected virtual void StandingOnEnter()
    {
        animator.Play(Game.AnimationsInfo.Standing.animatorName);
        rb2d.constraints = RigidbodyConstraints2D.FreezeRotation;
        statesTimer.Stop();
        statesTimer.Duration = Game.AnimationsInfo.Standing.Length;
        statesTimer.Run();
    }

    protected abstract bool CoveringTriggerer();
    protected virtual void CoveringOnEnter()
    {
        animator.Play(Game.AnimationsInfo.Covering.AnimatorName);
    }

    protected abstract bool UncoveringTriggerer();
    protected virtual void UncoveringOnEnter()
    {
        animator.Play(Game.AnimationsInfo.Uncovering.AnimatorName);
        statesTimer.Stop();
        statesTimer.Duration = Game.AnimationsInfo.Uncovering.Length;
        statesTimer.Run();
    }

    protected abstract bool LaunchingTriggerer();
    protected virtual void LaunchingOnEnter()
    {
        animator.Play(Game.AnimationsInfo.Launching.animatorName);
        float xForceMagnitude = (gameObject.transform.localScale.x > 0) ? -1 : 1;
        rb2d.AddForce(
            new Vector2(xForceMagnitude, jumpForceMagnitude),
            ForceMode2D.Impulse);
        statesTimer.Stop();
        statesTimer.Duration = Game.AnimationsInfo.Launching.Length;
        statesTimer.Run();
    }

    protected virtual bool FallingTriggerer() => statesTimer.Finished;
    protected virtual void FallingOnEnter()
    {
        animator.Play(Game.AnimationsInfo.Falling.AnimatorName);
        landed = false;
    }

    protected virtual bool LandingTriggerer() => landed;
    protected virtual bool LandingTriggererer()
    {
        return landed;
    }
    protected virtual void LandingOnEnter()
    {
        animator.Play(Game.AnimationsInfo.Landing.AnimatorName);
        landed = false;
        statesTimer.Stop();
        statesTimer.Duration = Game.AnimationsInfo.Landing.Length;
        statesTimer.Run();
    }

    protected abstract bool FlippingLeftTriggerer();
    protected virtual void FlippingLeftOnEnter()
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
    protected virtual void FlippingRightOnEnter()
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
    protected virtual void RunningOnEnter()
    {
        animator.Play(Game.AnimationsInfo.Running.AnimatorName);
    }
    protected virtual void RunningAction()
    {
        if (Mathf.Abs(rb2d.velocity.x) < maxRunVelocity)
        {
            rb2d.AddForce(
                new Vector2(-gameObject.transform.localScale.x * runForceMagnitude, 0),
                ForceMode2D.Impulse);
        }
    }

    protected abstract bool AttackingTriggerer();
    protected virtual void AttackingOnEnter()
    {
        animator.Play(Game.AnimationsInfo.Attacking.AnimatorName);
        statesTimer.Stop();
        statesTimer.Duration = Game.AnimationsInfo.Attacking.Length;
        statesTimer.Run();
    }

    protected abstract bool ThrowingTriggerer();
    protected virtual void ThrowingOnEnter()
    {
        animator.Play(Game.AnimationsInfo.Throwing.AnimatorName);
        statesTimer.Stop();
        statesTimer.Duration = Game.AnimationsInfo.Throwing.Length;
        statesTimer.Run();
    }

    #endregion

}
