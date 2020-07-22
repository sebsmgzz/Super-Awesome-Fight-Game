using UnityEngine;

public class GameInitializer : MonoBehaviour
{

    private void Awake()
    {
        ScreenUtilities.Initialize();
        ConfigurationManager.Initialize();
    }

}
