using System.Collections.Generic;
using UnityEngine;

public abstract class Controller : MonoBehaviour
{

    #region Fields

    // components
    private Animator animator;
    private Rigidbody2D rb2d;
    protected Timer statesTimer;

    // tools
    private Graph<State> graph;
    private GraphNode<State> currentNode;

    // config
    private float runForceMagnitude;
    private float jumpForceMagnitude;

    // flags
    protected bool landed = false;

    // states
    private State idling;
    private State crouching;
    private State standing;
    private State covering;
    private State uncovering;
    private State launching;
    private State falling;
    private State landing;
    private State attacking;
    private State flippingLeft;
    private State flippingRight;
    private State running;
    private State throwing;

    #endregion

    #region Properties

    protected abstract float MaxVelocity { get; }

    public bool Idling => currentNode.Value == idling;
    public bool Crouching => currentNode.Value == crouching;
    public bool Standing => currentNode.Value == standing;
    public bool Covering => currentNode.Value == covering;
    public bool Uncovering => currentNode.Value == uncovering;
    public bool Launching => currentNode.Value == launching;
    public bool Falling => currentNode.Value == falling;
    public bool Attacking => currentNode.Value == attacking;
    public bool Flipping => currentNode.Value == flippingLeft || currentNode.Value == flippingRight;
    public bool Running => currentNode.Value == running;
    public bool Throwing => currentNode.Value == throwing;

    #endregion

    #region Unity API

    protected virtual void Start()
    {
        // components
        animator = gameObject.GetComponent<Animator>();
        rb2d = gameObject.GetComponent<Rigidbody2D>();
        statesTimer = gameObject.AddComponent<Timer>();
        // config
        runForceMagnitude = ConfigurationManager.RunForceMagnitude;
        jumpForceMagnitude = ConfigurationManager.JumpForceMagnitude;
        // builders
        BuildStates();
        graph = new Graph<State>();
        BuildGraphNodes();
        BuildGraphEdges();
        // default state
        currentNode = graph.Find(idling);
    }

    protected virtual void Update()
    {
        IList<GraphNode<State>> neighbors = currentNode.Neighbors;
        foreach(GraphNode<State> neighbor in neighbors)
        {
            if(neighbor.Value.Triggered)
            {
                currentNode = neighbor;
                neighbor.Value.Invoke();
                break;
            }
        }
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Plataform"))
        {
            landed = true;
        }
    }

    #endregion

    #region Methods

    /// <summary>
    /// Flips the game object in the X axis
    /// </summary>
    /// <param name="direction">True to the left</param>
    private void Flip(bool toLeft)
    {
        Vector3 characterScale = gameObject.transform.localScale;
        characterScale.x = (toLeft) ? 1 : -1;
        gameObject.transform.localScale = characterScale;
    }

    /// <summary>
    /// Applies force to run
    /// </summary>
    /// <param name="direction">The direction to run to</param>
    private void Run()
    {
        float dir = -gameObject.transform.localScale.x;
        if (Mathf.Abs(rb2d.velocity.x) < MaxVelocity)
        {
            rb2d.AddForce(
                new Vector2(dir * runForceMagnitude, 0),
                ForceMode2D.Impulse);
        }
    }

    #endregion

    #region Builders

    /// <summary>
    /// Builds the states fields
    /// </summary>
    private void BuildStates()
    {
        // idle state
        idling = new State(IdlingTrigger, IdleOnEnter);
        // crouch state
        crouching = new State(CrouchingTrigger, CrouchingOnEnter);
        standing = new State(StandInputCheck, StandOnEnter);
        // defend state
        covering = new State(CoveringTrigger, CoveringOnEnter);
        uncovering = new State(UncoveringTrigger, UncoveringOnEnter);
        // jump state
        launching = new State(LaunchingTrigger, LaunchingOnEnter);
        falling = new State(FallingTrigger, FallingOnEnter);
        landing = new State(LandingTrigger, LandingOnEnter);
        // attack state
        attacking = new State(AttackingTrigger, AttackingOnEnter);
        // run state
        flippingLeft = new State(FlippingLeftTrigger, FlippingLeftOnEnter);
        flippingRight = new State(FlippingRightTrigger, FlippingRightOnEnter);
        running = new State(RunningTrigger, RunningOnEnter);
        // throw state
        throwing = new State(ThrowingTrigger, ThrowingOnEnter);
    }

    /// <summary>
    /// Adds the nodes to the graph
    /// </summary>
    private void BuildGraphNodes()
    {
        graph.AddNode(idling);
        graph.AddNode(crouching);
        graph.AddNode(standing);
        graph.AddNode(covering);
        graph.AddNode(uncovering);
        graph.AddNode(launching);
        graph.AddNode(falling);
        graph.AddNode(landing);
        graph.AddNode(attacking);
        graph.AddNode(flippingLeft);
        graph.AddNode(flippingRight);
        graph.AddNode(running);
        graph.AddNode(throwing);
    }

    /// <summary>
    /// Adds the edges to the graph
    /// </summary>
    private void BuildGraphEdges()
    {
        // idle action edges
        graph.AddEdge(idling, crouching);
        graph.AddEdge(idling, covering);
        graph.AddEdge(idling, launching);
        graph.AddEdge(idling, attacking);
        graph.AddEdge(idling, flippingLeft);
        graph.AddEdge(idling, flippingRight);
        graph.AddEdge(idling, throwing);
        // crouch action edges
        graph.AddEdge(crouching, standing);
        graph.AddEdge(standing, idling);
        // defend action edges
        graph.AddEdge(covering, uncovering);
        graph.AddEdge(uncovering, idling);
        // jump action edges
        graph.AddEdge(launching, falling);
        graph.AddEdge(falling, landing);
        graph.AddEdge(landing, idling);
        graph.AddEdge(landing, flippingLeft);
        graph.AddEdge(landing, flippingRight);
        // attack action edges
        graph.AddEdge(attacking, idling);
        // run action edges
        graph.AddEdge(flippingLeft, running);
        graph.AddEdge(flippingRight, running);
        graph.AddEdge(running, flippingLeft);
        graph.AddEdge(running, flippingRight);
        graph.AddEdge(running, running);
        graph.AddEdge(running, idling);
        graph.AddEdge(running, launching);
        // throw action edges
        graph.AddEdge(throwing, idling);
    }

    #endregion

    #region Idle States

    protected abstract bool IdlingTrigger();
    protected virtual void IdleOnEnter()
    {
        // animator
        animator.Play(GameConstants.Idling.animatorName);
    }

    #endregion

    #region Crouch States

    protected abstract bool CrouchingTrigger();
    protected virtual void CrouchingOnEnter()
    {
        // animator
        animator.Play(GameConstants.Crouching.animatorName);
        // action
        rb2d.constraints =
            RigidbodyConstraints2D.FreezeRotation |
            RigidbodyConstraints2D.FreezePositionX;
    }

    protected abstract bool StandInputCheck();
    protected virtual void StandOnEnter()
    {
        // animator
        animator.Play(GameConstants.Standing.animatorName);
        // action
        rb2d.constraints = RigidbodyConstraints2D.FreezeRotation;
        // timer
        statesTimer.Duration = GameConstants.Standing.Length;
        statesTimer.Run();
    }

    #endregion

    #region Defend States

    protected abstract bool CoveringTrigger();
    protected virtual void CoveringOnEnter()
    {
        // animator
        animator.Play(GameConstants.Covering.AnimatorName);
    }

    protected abstract bool UncoveringTrigger();
    protected virtual void UncoveringOnEnter()
    {
        // animator
        animator.Play(GameConstants.Uncovering.AnimatorName);
        // timer
        statesTimer.Duration = GameConstants.Uncovering.Length;
        statesTimer.Run();
    }

    #endregion

    #region Jump States

    protected abstract bool LaunchingTrigger() ;
    protected virtual void LaunchingOnEnter()
    {
        // animator
        animator.Play(GameConstants.Launching.animatorName);
        // action
        rb2d.AddForce(
            new Vector2(0, jumpForceMagnitude),
            ForceMode2D.Impulse);
        // timer
        statesTimer.Duration = GameConstants.Launching.Length;
        statesTimer.Run();
    }

    protected abstract bool FallingTrigger();
    protected virtual void FallingOnEnter()
    {
        // animator
        animator.Play(GameConstants.Falling.AnimatorName);
        // action
        landed = false;
    }

    protected abstract bool LandingTrigger();
    protected virtual void LandingOnEnter()
    {
        // animator
        animator.Play(GameConstants.Landing.AnimatorName);
        // action
        landed = false;
        // timer
        statesTimer.Duration = GameConstants.Landing.Length;
        statesTimer.Run();
    }

    #endregion

    #region Run States

    protected abstract bool FlippingLeftTrigger();
    protected virtual void FlippingLeftOnEnter()
    {
        // action
        Flip(true);
        // animator
        animator.Play(GameConstants.Running.AnimatorName);
    }

    protected abstract bool FlippingRightTrigger();
    protected virtual void FlippingRightOnEnter()
    {
        // action
        Flip(false);
        // animator
        animator.Play(GameConstants.Running.AnimatorName);
    }

    protected abstract bool RunningTrigger();
    protected virtual void RunningOnEnter()
    {
        // action
        Run();
        // timer
        statesTimer.Duration = GameConstants.Running.Length / 2f;
        statesTimer.Run();
    }

    #endregion

    #region Attack States

    protected abstract bool AttackingTrigger();
    protected virtual void AttackingOnEnter()
    {
        // animator
        animator.Play(GameConstants.Attacking.AnimatorName);
        // timer
        statesTimer.Duration = GameConstants.Attacking.Length;
        statesTimer.Run();
    }

    #endregion

    #region Throw States

    protected abstract bool ThrowingTrigger();
    protected virtual void ThrowingOnEnter()
    {
        // animator
        animator.Play(GameConstants.Throwing.AnimatorName);
        // timer
        statesTimer.Duration = GameConstants.Throwing.Length;
        statesTimer.Run();
    }

    #endregion

}
