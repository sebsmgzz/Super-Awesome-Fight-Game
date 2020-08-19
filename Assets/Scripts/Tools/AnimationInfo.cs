
public struct AnimationInfo
{

    #region Fields

    private Animation animation;
    public string animatorName;
    private float length;
    private bool loops;

    #endregion

    #region Properties

    /// <summary>
    /// The name of the animation
    /// </summary>
    public Animation Animation
    {
        get { return animation; }
        set { animation = value; }
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

    public AnimationInfo(Animation name)
    {
        this.animation = name;
        this.animatorName = name.ToString();
        this.length = 0;
        this.loops = false;
    }

    #endregion

}