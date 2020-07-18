using UnityEngine;

public class JumpControl : MonoBehaviour
{

    #region Fields

    // components
    private Animator animator;
    private Rigidbody2D rb2d;
    private Timer timer;

    // animations names
    private string jumpAnimationName = "Jump";
    private string fallingAnimationName = "Falling";
    private string landingAnimationName = "Land";

    // flags
    bool isJumping = false;

    // configuration
    float jumpForceMagnitude = 7f;

    #endregion

    #region Unity API

    private void Start()
    {
        // components
        animator = gameObject.GetComponent<Animator>();
        rb2d = gameObject.GetComponent<Rigidbody2D>();
        // timer
        timer = gameObject.AddComponent<Timer>();
        timer.Duration = 0.1f;
        timer.AddTimerFinishedListener(HandleTimerFinished);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) && !isJumping)
        {
            // animate
            animator.Play(jumpAnimationName);
            // jump
            rb2d.AddForce(
                new Vector2(0, jumpForceMagnitude), 
                ForceMode2D.Impulse);
            // prepare to fall
            timer.Run();
            isJumping = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(isJumping)
        {
            // animate
            animator.Play(landingAnimationName);
            // prepare to jump
            isJumping = false;
        }
    }

    #endregion

    #region Methods

    /// <summary>
    /// Called when timer finishes
    /// </summary>
    private void HandleTimerFinished()
    {
        // animate
        animator.Play(fallingAnimationName);
    }

    #endregion

}
