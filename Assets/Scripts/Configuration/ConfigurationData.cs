using System.Collections.Generic;
using System.Globalization;
using System.IO;
using UnityEngine;

public class ConfigurationData
{

    #region Fields

    // file name
    private const string ConfigurationDataFileName = "ConfigurationData.csv";

    // animations magnitudes
    private static float runForceMagnitude = 2f;
    private static float jumpForceMagnitude = 7f;
    private static float nearDistanceTriggerer = 2.2f;
    private static float defaultDamageForce = 5f;

    #endregion

    #region Properties

    public float RunForceMagnitude => runForceMagnitude;
    public float JumpForceMagnitude => jumpForceMagnitude;
    public float NearDistanceTriggerer => nearDistanceTriggerer;
    public float DefaultDamageForce => defaultDamageForce;

    #endregion

    #region Constructor

    /// <summary>
    /// Reads from ConfigurationDataFileName
    /// </summary>
    public ConfigurationData()
    {
        StreamReader configReader = null;
        try
        {
            Dictionary<string, string> CSVvalues = new Dictionary<string, string>();
            string configDataFileFullPath = Path.Combine(Application.streamingAssetsPath, ConfigurationDataFileName);
            configReader = File.OpenText(configDataFileFullPath);
            string line = configReader.ReadLine();
            while (line != null)
            {
                string[] lines = line.Split(',');
                CSVvalues[lines[0]] = lines[1];
                line = configReader.ReadLine();
            }
            SetConfigurationDataFields(CSVvalues);
        }
        catch
        {
            Debug.Log("Error loading " + ConfigurationDataFileName);
        }
        finally
        {
            if (configReader != null)
            {
                configReader.Close();
            }
        }
    }

    #endregion

    #region Methods

    /// <summary>
    /// Sets the fields from the given dictionary
    /// </summary>
    /// <param name="values">The dictionary containing the values</param>
    private static void SetConfigurationDataFields(Dictionary<string, string> values)
    {
        runForceMagnitude = float.Parse(values["runForceMagnitude"], CultureInfo.InvariantCulture);
        jumpForceMagnitude = float.Parse(values["jumpForceMagnitude"], CultureInfo.InvariantCulture);
        nearDistanceTriggerer = float.Parse(values["nearDistanceTriggerer"], CultureInfo.InvariantCulture);
        defaultDamageForce = float.Parse(values["defaultDamageForce"], CultureInfo.InvariantCulture);
    }

    #endregion

}
