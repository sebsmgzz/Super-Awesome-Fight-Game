using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ConfigurationData
{

    #region Fields

    // file name
    private const string ConfigurationDataFileName = "ConfigurationData.csv";

    // animations magnitudes
    private static float runForceMagnitude = 0.7f;
    private static float jumpForceMagnitude = 7f;

    // animations durations
    private static float attackAnimationDuration = 0.8f;
    private static float defendAnimationDuration = 1f;
    private static float jumpAnimationDuration = 0.1f;
    private static float throwAnimationDuration = 1f;

    #endregion

    #region Properties

    public float RunForceMagnitude => runForceMagnitude;
    public float JumpForceMagnitude => jumpForceMagnitude;
    public float AttackAnimationDuration => attackAnimationDuration;
    public float DefendAnimationDuration => defendAnimationDuration;
    public float JumpAnimationDuration => jumpAnimationDuration;
    public float ThrowAnimationDuration => throwAnimationDuration;

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
            }
            SetConfigurationDataFields(CSVvalues);
        }
        catch { }
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
    static void SetConfigurationDataFields(Dictionary<string, string> values)
    {
        runForceMagnitude = float.Parse(values["runForceMagnitude"]);
        jumpForceMagnitude = float.Parse(values["jumpForceMagnitude"]);
        attackAnimationDuration = float.Parse(values["attackAnimationDuration"]);
        defendAnimationDuration = float.Parse(values["defendAnimationDuration"]);
        jumpAnimationDuration = float.Parse(values["jumpAnimationDuration"]);
    }

    #endregion

}
