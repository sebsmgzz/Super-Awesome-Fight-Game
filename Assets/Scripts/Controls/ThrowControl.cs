﻿using UnityEngine;

public class ThrowControl : MonoBehaviour
{

    #region Fields

    // components
    private Animator animator;
    private Timer timer;

    // animations name
    private string defaultAnimationName = "Idling";
    private string throwAnimationName = "Throw";

    #endregion

    #region Unity API

    private void Start()
    {
        // components
        animator = gameObject.GetComponent<Animator>();
        // timer
        timer = gameObject.AddComponent<Timer>();
        timer.Duration = 1f;
        timer.AddTimerFinishedListener(HandleTimerFinished);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            animator.Play(throwAnimationName);
            timer.Run();
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