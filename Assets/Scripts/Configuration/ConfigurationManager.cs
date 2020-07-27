
public static class ConfigurationManager
{

    #region Fields

    private static ConfigurationData configData;

    #endregion

    #region Properties

    public static float RunForceMagnitude => configData.RunForceMagnitude;
    public static float JumpForceMagnitude => configData.JumpForceMagnitude;

    #endregion

    #region Methods

    public static void Initialize()
    {
        configData = new ConfigurationData();
    }

    #endregion

}