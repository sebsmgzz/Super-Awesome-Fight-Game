using UnityEngine;

public class DefendControl : MonoBehaviour
{

    #region Fields

    // components
    private Animator animator;
    private Timer timer;

    // flags
    bool defending = false;

    // animations names
    string defaultAnimationName = "Idling";
    string defendAnimationName = "Defend";
    string defendReversedAnimationName = "Defend_Reversed";

    #endregion

    #region Unity API

    private void Start()
    {
        // components
        animator = gameObject.GetComponent<Animator>();
        // timers
        timer = gameObject.GetComponent<Timer>();
        timer.Duration = 1f;
        timer.AddTimerFinishedListener(HandleTimerFinished);
    }

    private void Update()
    {
        if(Input.GetKey(KeyCode.E))
        {
            if(!defending)
            {
                animator.Play(defendAnimationName);
            }
            defending = true;
        }
        else if(defending || Input.GetKeyUp(KeyCode.E))
        {
            animator.Play(defendReversedAnimationName);
            timer.Run();
            defending = false;
        }
    }

    #endregion

    #region Methods

    /// <summary>
    /// Handles the timer finished
    /// </summary>
    private void HandleTimerFinished()
    {
        animator.Play(defaultAnimationName);
    }

    #endregion

}