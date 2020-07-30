using System;

public class State
{

    #region Fields

    private Func<bool> triggerer;
    private Action action;

    #endregion

    #region Property

    public bool Triggered => triggerer.Invoke();

    public Func<bool> Triggerer
    {
        get { return triggerer; }
        set { triggerer = value; }
    }

    #endregion

    #region Constructor

    public State(Func<bool> inputCheck, Action action)
    {
        this.triggerer = inputCheck;
        this.action = action;
    }

    #endregion

    #region Methods

    public void Invoke() => action.Invoke();

    #endregion

    #region Enums

    public enum Case
    {
        Idling,
        Crouching,
        Defending,
        Standing,
        Covering,
        Uncovering,
        Launching,
        Falling,
        Landing,
        Attacking,
        FlippingLeft,
        FlippingRight,
        Running,
        Throwing
    }

    #endregion

}
