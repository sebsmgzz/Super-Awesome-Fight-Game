
public static class ConfigurationManager
{

    #region Fields

    public static ConfigurationData configData;

    #endregion

    #region Properties

    public static float RunForceMagnitude => configData.RunForceMagnitude;
    public static float JumpForceMagnitude => configData.JumpForceMagnitude;
    public static float NearDistanceTriggerer => configData.NearDistanceTriggerer;
    public static float DefaultDamageForce => configData.DefaultDamageForce;

    #endregion

    #region Methods

    public static void Initialize()
    {
        configData = new ConfigurationData();
    }

    #endregion

}