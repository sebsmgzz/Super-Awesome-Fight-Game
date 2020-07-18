using UnityEngine;

public class IdleControl : MonoBehaviour
{

    #region Fields

    // components
    private Animator animator;

    // animations names
    private string idleAnimationName = "Idle";

    #endregion

    #region Unity API

    public void Start()
    {
        animator = gameObject.GetComponent<Animator>();
    }

    #endregion

    #region Methods

    /// <summary>
    /// Animates the idle
    /// </summary>
    public void Animate()
    {
        animator.Play(idleAnimationName);
    }

    #endregion

}