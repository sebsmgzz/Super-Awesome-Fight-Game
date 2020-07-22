using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Th event manager
/// </summary>
public static class EventsManager
{

    #region DamageMade

    private static List<Character> damageMadeInvokers = 
        new List<Character>();
    private static List<UnityAction<Fighter>> damageMadeListeners = 
        new List<UnityAction<Fighter>>();

    public static void AddDamageMadeInvoker(Character invoker)
    {
        damageMadeInvokers.Add(invoker);
        foreach (UnityAction<Fighter> listener in damageMadeListeners)
        {
            invoker.AddDamageMadeListener(listener);
        }
    }

    public static void AddDamageMadeListener(UnityAction<Fighter> listener)
    {
        damageMadeListeners.Add(listener);
        foreach (Character invoker in damageMadeInvokers)
        {
            invoker.AddDamageMadeListener(listener);
        }
    }

    #endregion

    #region EmptyHealth


    private static List<HUD> emptyHealthInvokers =
        new List<HUD>();
    private static List<UnityAction<Fighter>> emptyHealthListeners =
        new List<UnityAction<Fighter>>();

    public static void AddEmptyHealthInvoker(HUD invoker)
    {
        emptyHealthInvokers.Add(invoker);
        foreach (UnityAction<Fighter> listener in emptyHealthListeners)
        {
            invoker.AddEmptyHealthLister(listener);
        }
    }

    public static void AddEmptyHealthListener(UnityAction<Fighter> listener)
    {
        emptyHealthListeners.Add(listener);
        foreach (HUD invoker in emptyHealthInvokers)
        {
            invoker.AddEmptyHealthLister(listener);
        }
    }

    #endregion

}
