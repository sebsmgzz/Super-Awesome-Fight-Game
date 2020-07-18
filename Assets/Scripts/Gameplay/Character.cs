using UnityEngine;

public class Character : MonoBehaviour
{

    #region Fields
        
    // components
    Rigidbody2D rb2d;

    #endregion

    #region Unity API

    void Start()
    {
        // controls
        gameObject.AddComponent<IdleControl>();
        gameObject.AddComponent<RunControl>();
        gameObject.AddComponent<JumpControl>();
        gameObject.AddComponent<CrouchControl>();
        gameObject.AddComponent<AttackControl>();
        gameObject.AddComponent<DefendControl>();
        gameObject.AddComponent<ThrowControl>();
        // components
        rb2d = GetComponent<Rigidbody2D>();
    }

    #endregion

}
