using UnityEngine;

public class AttackControl : MonoBehaviour
{

    #region Fields

    // component
    private Animator animator;
    private Timer attackTimer;

    // animation
    private string defaultAnimationName = "Idling";
    private string attackAnimatorName = "Attack";
    private float attackAnimationDuration = ConfigurationManager.AttackAnimationDuration;

    // flags
    private bool attacking = false;

    #endregion

    #region Propesties

    /// <summary>
    /// True when attacking
    /// </summary>
    public bool Attacking
    {
        get { return attacking; }
    }

    #endregion

    #region Unity API

    private void Start()
    {
        // components
        animator = gameObject.GetComponent<Animator>();
        // timer
        attackTimer = gameObject.GetComponent<Timer>();
        attackTimer.Duration = attackAnimationDuration;
        attackTimer.AddTimerFinishedListener(HandleTimerFinished);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            animator.Play(attackAnimatorName);
            attackTimer.Run();
            attacking = true;
        }
    }

    #endregion

    #region Methods

    private void HandleTimerFinished()
    {
        animator.Play(defaultAnimationName);
        attacking = false;
    }

    #endregion

}