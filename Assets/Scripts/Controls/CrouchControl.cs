using UnityEngine;

public class CrouchControl : MonoBehaviour
{

    #region Fields

    // components
    Animator animator;
    Rigidbody2D rb2d;

    // animations names
    string crouchAnimationName = "Crouch";
    string standupAnimationName = "Standup";

    // flag
    bool crouched = false;

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
        if(Input.GetKey(KeyCode.S))
        {
            if(!crouched)
            {
                animator.Play(crouchAnimationName);
            }
            rb2d.constraints = 
                RigidbodyConstraints2D.FreezeRotation | 
                RigidbodyConstraints2D.FreezePositionX;
            crouched = true;
        }
        else if(crouched || Input.GetKeyUp(KeyCode.S))
        {
            animator.Play(standupAnimationName);
            rb2d.constraints = RigidbodyConstraints2D.FreezeRotation;
            crouched = false;
        }
    }

    #endregion

}
