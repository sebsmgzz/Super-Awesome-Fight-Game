
public struct Animation
{

    #region Fields

    private Case name;
    public string animatorName;
    private float length;
    private bool loops;

    #endregion

    #region Properties

    /// <summary>
    /// The name of the animation
    /// </summary>
    public Case Name
    {
        get { return name; }
        set { name = value; }
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

    public Animation(Case name, string animatorName, float length, bool loops)
    {
        this.name = name;
        this.animatorName = animatorName;
        this.length = length;
        this.loops = loops;
    }

    #endregion

    #region Enum

    public enum Case
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