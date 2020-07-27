
public static class GameConstants
{

    #region Animations

    public static Animation Crouching => 
        new Animation(Animation.Name.Crouching, "Crouching", 0.2f, false);
    public static Animation Standing => 
        new Animation(Animation.Name.Standing, "Standing", 0.3f, false);
    public static Animation Covering => 
        new Animation(Animation.Name.Covering, "Covering", 0.333f, false);
    public static Animation Uncovering => 
        new Animation(Animation.Name.Uncovering, "Uncovering", 0.333f, false);
    public static Animation Falling => 
        new Animation(Animation.Name.Falling, "Falling", 0.5f, true);
    public static Animation Landing => 
        new Animation(Animation.Name.Landing, "Landing", 0.5f, false);
    public static Animation Launching => 
        new Animation(Animation.Name.Launching, "Launching", 0.5f, false);
    public static Animation Attacking => 
        new Animation(Animation.Name.Attacking, "Attacking", 0.667f, false);
    public static Animation Idling => 
        new Animation(Animation.Name.Idling, "Idling", 0.583f, true);
    public static Animation Running => 
        new Animation(Animation.Name.Running, "Running", 0.5f, true);
    public static Animation Throwing => 
        new Animation(Animation.Name.Throwing, "Throwing", 0.667f, false);

    #endregion

}
