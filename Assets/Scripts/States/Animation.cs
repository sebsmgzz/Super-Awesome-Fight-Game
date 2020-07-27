
public struct Animation
{

    #region Fields

    private Name nameVal;
    public string animatorName;
    private float length;
    private bool loops;

    #endregion

    #region Properties

    /// <summary>
    /// The name of the animation
    /// </summary>
    public Name NameVal
    {
        get { return nameVal; }
        set { nameVal = value; }
    }

    /// <summary>
    /// The name to play animation in animator
    /// </summary>
    public string AnimatorName
    {
        get { return animatorName; }
        set { animatorName = value; }
    }

    /// <summary>
    /// The duration of 1 cycle of the animation
    /// </summary>
    public float Length
    {
        get { return length; }
        set { length = value; }
    }

    /// <summary>
    /// True if the animation is supposed to loop over and over
    /// </summary>
    public bool Loops
    {
        get { return loops; }
        set { loops = value; }
    }

    #endregion

    #region Constructor

    public Animation(Name name, string animatorName, float length, bool loops)
    {
        this.nameVal = name;
        this.animatorName = animatorName;
        this.length = length;
        this.loops = loops;
    }

    #endregion

    #region Enum

    public enum Name
    {
        Crouching,
        Standing,
        Covering,
        Uncovering,
        Falling,
        Landing,
        Launching,
        Attacking,
        Idling,
        Running,
        Throwing
    }

    #endregion

}