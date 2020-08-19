using UnityEngine;

public class Initializer : MonoBehaviour
{
    
    private void Awake()
    {
        ScreenUtilities.Initialize();
        ConfigurationManager.Initialize();
        Pools.Initialize();
    }

}
