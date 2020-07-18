using UnityEngine;

public class ControlsManager : MonoBehaviour
{

    IdleControl idleControl;
    RunControl runControl;
    JumpControl jumpControl;
    CrouchControl crouchControl;
    AttackControl attackControl;
    DefendControl defendControl;
    ThrowControl throwControl;

    public bool Attacking
    {
        get { return attackControl.Attacking; }
    }

    public void Start()
    {
        // controls
        idleControl = gameObject.AddComponent<IdleControl>();
        runControl = gameObject.AddComponent<RunControl>();
        jumpControl = gameObject.AddComponent<JumpControl>();
        crouchControl = gameObject.AddComponent<CrouchControl>();
        attackControl = gameObject.AddComponent<AttackControl>();
        defendControl = gameObject.AddComponent<DefendControl>();
        throwControl = gameObject.AddComponent<ThrowControl>();
    }

}
