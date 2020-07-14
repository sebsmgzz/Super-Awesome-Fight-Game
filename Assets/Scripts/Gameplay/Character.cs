using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{

    #region Fields

    // Animations
    Animator animator;
    bool isJumping;

    // Components
    Rigidbody2D rb2d;

    // Configuration
    float runningMagnitude = 0.1f;
    float jumpingMagnitude = 10f;

    // Timers
    Timer actionTimer;

    #endregion

    #region Unity API

    void Start()
    {
        // components
        animator = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        // timers
        actionTimer = gameObject.AddComponent<Timer>();
    }

    void Update()
    {
        // run input
        float horizontalInput = Input.GetAxis("Horizontal");
        if(horizontalInput != 0)
        {
            StartRunning(horizontalInput);
        }
        else
        {
            StopRunning();
        }
        // jump/crouch input
        float verticalInput = Input.GetAxis("Vertical");
        if (verticalInput > 0)
        {
            StartJumping();
        }
        else if(verticalInput < 0)
        {
            print("Crouch");
        }
        // other inputs
        if(Input.GetKeyDown(KeyCode.Space))
        {
            print("Throw");
        }
        else if(Input.GetKeyDown(KeyCode.E))
        {
            print("Attack");
        }
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            print("Defend");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Plataform"))
        {
            StopJumping();
        }
    }

    #endregion

    #region Actions

    void StartRunning(float horizontalInput)
    {
        animator.SetBool("isRunning", true);
        horizontalInput /= Mathf.Abs(horizontalInput);
        rb2d.AddForce(
            new Vector2(horizontalInput * runningMagnitude, 0),
            ForceMode2D.Impulse);
    }

    void StopRunning()
    {
        animator.SetBool("isRunning", false);
        if (rb2d.velocity.x != 0)
        {
            rb2d.velocity = new Vector2(rb2d.velocity.x / 2, rb2d.velocity.y);
        }
    }

    void StartJumping()
    {
        if(!isJumping)
        {
            animator.SetBool("isJumping", true);
            isJumping = true;
            rb2d.AddForce(
                new Vector2(0, jumpingMagnitude),
                ForceMode2D.Impulse);
        }
    }

    void StopJumping()
    {
        animator.SetBool("isJumping", false);
        isJumping = false;
    }

    #endregion

}
