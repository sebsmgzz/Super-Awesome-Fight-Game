using UnityEngine;

public class AttackControl : MonoBehaviour
{

    #region Fields

    // component
    private Animator animator;
    private Timer attackTimer;

    // animation name
    private string defaultAnimationName = "Idling";
    private string attackAnimatorName = "Attack";

    #endregion

    #region Unity API
    
    private void Start()
    {
        // components
        animator = gameObject.GetComponent<Animator>();
        // timer
        attackTimer = gameObject.GetComponent<Timer>();
        attackTimer.Duration = 1f;
        attackTimer.AddTimerFinishedListener(HandleTimerFinished);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            animator.Play(attackAnimatorName);
            attackTimer.Run();
        }
    }

    #endregion

    #region Methods

    private void HandleTimerFinished()
    {
        animator.Play(defaultAnimationName);
    }

    #endregion

}