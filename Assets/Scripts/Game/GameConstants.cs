
public static class GameConstants
{

    #region Tags

    public static string PlayerTag => "Player";

    #endregion

    #region Animations

    public static Animation Crouching => 
        new Animation(Animation.Case.Crouching, "Crouching", 0.2f, false);
    public static Animation Standing => 
        new Animation(Animation.Case.Standing, "Standing", 0.3f, false);
    public static Animation Covering => 
        new Animation(Animation.Case.Covering, "Covering", 0.333f, false);
    public static Animation Uncovering => 
        new Animation(Animation.Case.Uncovering, "Uncovering", 0.333f, false);
    public static Animation Falling => 
        new Animation(Animation.Case.Falling, "Falling", 0.5f, true);
    public static Animation Landing => 
        new Animation(Animation.Case.Landing, "Landing", 0.5f, false);
    public static Animation Launching => 
        new Animation(Animation.Case.Launching, "Launching", 0.5f, false);
    public static Animation Attacking => 
        new Animation(Animation.Case.Attacking, "Attacking", 0.667f, false);
    public static Animation Idling => 
        new Animation(Animation.Case.Idling, "Idling", 0.583f, true);
    public static Animation Running => 
        new Animation(Animation.Case.Running, "Running", 0.5f, true);
    public static Animation Throwing => 
        new Animation(Animation.Case.Throwing, "Throwing", 0.667f, false);

    #endregion

}
