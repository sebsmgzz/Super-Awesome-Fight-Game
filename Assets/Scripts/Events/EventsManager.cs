using System.Collections.Generic;
using UnityEngine.Events;

/// <summary>
/// The event manager
/// </summary>
public static class EventsManager
{

    #region Fields

    private static List<IDamageMadeInvoker> damageMadeInvokers = 
        new List<IDamageMadeInvoker>();
    private static List<UnityAction<float, FighterType>> damageMadeListeners = 
        new List<UnityAction<float, FighterType>>();

    private static List<IHealthEmptiedInvoker> healthEmptiedInvokers =
        new List<IHealthEmptiedInvoker>();
    private static List<UnityAction<FighterType>> healthEmptiedListeners =
        new List<UnityAction<FighterType>>();

    #endregion

    #region AddInvoker

    public static void AddInvoker(IHealthEmptiedInvoker invoker)
    {
        healthEmptiedInvokers.Add(invoker);
        foreach (UnityAction<FighterType> listener in healthEmptiedListeners)
        {
            invoker.AddHealthEmptiedLister(listener);
        }
    }

    public static void AddInvoker(IDamageMadeInvoker invoker)
    {
        damageMadeInvokers.Add(invoker);
        foreach (UnityAction<float, FighterType> listener in damageMadeListeners)
        {
            invoker.AddDamageMadeListener(listener);
        }
    }

    #endregion

    #region AddListener

    public static void AddDamageMadeListener(UnityAction<float, FighterType> listener)
    {
        damageMadeListeners.Add(listener);
        foreach (IDamageMadeInvoker invoker in damageMadeInvokers)
        {
            invoker.AddDamageMadeListener(listener);
        }
    }

    public static void AddHealthEmptiedListener(UnityAction<FighterType> listener)
    {
        healthEmptiedListeners.Add(listener);
        foreach (IHealthEmptiedInvoker invoker in healthEmptiedInvokers)
        {
            invoker.AddHealthEmptiedLister(listener);
        }
    }

    #endregion

}
