
public static class ConfigurationManager
{

    #region Fields

    private static ConfigurationData configData;

    public static DifficultyLevel DifficultyLevelSelected;

    #endregion

    #region Properties

    public static float RunForceMagnitude => configData.RunForceMagnitude;
    public static float JumpForceMagnitude => configData.JumpForceMagnitude;
    public static float AttackAnimationDuration => configData.AttackAnimationDuration;
    public static float DefendAnimationDuration => configData.DefendAnimationDuration;
    public static float JumpAnimationDuration => configData.JumpAnimationDuration;
    public static float ThrowAnimationDuration => configData.ThrowAnimationDuration;

    #endregion

    #region Methods

    public static void Initialize()
    {
        configData = new ConfigurationData();
    }

    #endregion

}