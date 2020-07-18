using UnityEngine;

public class RunControl : MonoBehaviour
{

    #region Fields

    // components
    private Animator animator;
    private Rigidbody2D rb2d;

    // animations names
    private string defaultAnimationName = "Idling";
    private string runAnimationName = "Running";

    // configuration
    private float runForceMagnitude = 0.7f;

    #endregion

    #region Unity API

    private void Start()
    {
        // components
        animator = gameObject.GetComponent<Animator>();
        rb2d = gameObject.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // left run
        if(Input.GetKeyDown(KeyCode.A))
        {
            animator.Play(runAnimationName);
            Flip(Direction.Left);
        }
        else if(Input.GetKey(KeyCode.A))
        {
            Run(Direction.Left);
        }
        else if(Input.GetKeyUp(KeyCode.A))
        {
            animator.Play(defaultAnimationName);
        }
        // right run
        else if(Input.GetKeyDown(KeyCode.D))
        {
            animator.Play(runAnimationName);
            Flip(Direction.Right);
        }
        else if(Input.GetKey(KeyCode.D))
        {
            Run(Direction.Right);
        }
        else if (Input.GetKeyUp(KeyCode.D))
        {
            animator.Play(defaultAnimationName);
        }
    }

    #endregion region

    #region Methods

    /// <summary>
    /// Flips the game object in the X axis
    /// </summary>
    /// <param name="direction">The direction to flip</param>
    private void Flip(Direction direction)
    {
        Vector3 characterScale = gameObject.transform.localScale;
        characterScale.x = (direction == Direction.Left)? 1 : -1;
        gameObject.transform.localScale = characterScale;
    }

    /// <summary>
    /// Applies force to run
    /// </summary>
    /// <param name="direction">The direction to run to</param>
    private void Run(Direction direction)
    {
        float dir = (direction == Direction.Left)? -1 : 1;
        rb2d.AddForce(
            new Vector2(dir * runForceMagnitude, 0),
            ForceMode2D.Impulse);
    }

    #endregion

}