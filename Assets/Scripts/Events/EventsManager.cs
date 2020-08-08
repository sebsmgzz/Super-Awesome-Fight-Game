using System.Collections.Generic;
using UnityEngine.Events;

/// <summary>
/// The event manager
/// </summary>
public static class EventsManager
{

    #region Fields

    private static List<IDamageTakenInvoker> damageTakenInvokers = 
        new List<IDamageTakenInvoker>();
    private static List<UnityAction<Fighter.Name>> damageTakenListeners = 
        new List<UnityAction<Fighter.Name>>();

    private static List<IHealthEmptiedInvoker> healthEmptiedInvokers =
        new List<IHealthEmptiedInvoker>();
    private static List<UnityAction<Fighter.Name>> healthEmptiedListeners =
        new List<UnityAction<Fighter.Name>>();

    private static List<IPlayerStartedStateInvoker> playerStartedStateInvokers =
        new List<IPlayerStartedStateInvoker>();
    private static List<UnityAction<State.Case>> playerStartedStateListeners =
        new List<UnityAction<State.Case>>();

    #endregion

    #region AddInvoker

    public static void AddInvoker(IHealthEmptiedInvoker invoker)
    {
        healthEmptiedInvokers.Add(invoker);
        foreach (UnityAction<Fighter.Name> listener in healthEmptiedListeners)
        {
            invoker.AddHealthEmptiedLister(listener);
        }
    }

    public static void AddInvoker(IDamageTakenInvoker invoker)
    {
        damageTakenInvokers.Add(invoker);
        foreach (UnityAction<Fighter.Name> listener in damageTakenListeners)
        {
            invoker.AddDamageTakenListener(listener);
        }
    }

    public static void AddInvoker(IPlayerStartedStateInvoker invoker)
    {
        playerStartedStateInvokers.Add(invoker);
        foreach (UnityAction<State.Case> listener in playerStartedStateListeners)
        {
            invoker.AddPlayerStartedStateListener(listener);
        }
    }

    #endregion

    #region AddListener

    public static void AddDamageTakenListener(UnityAction<Fighter.Name> listener)
    {
        damageTakenListeners.Add(listener);
        foreach (IDamageTakenInvoker invoker in damageTakenInvokers)
        {
            invoker.AddDamageTakenListener(listener);
        }
    }

    public static void AddHealthEmptiedListener(UnityAction<Fighter.Name> listener)
    {
        healthEmptiedListeners.Add(listener);
        foreach (IHealthEmptiedInvoker invoker in healthEmptiedInvokers)
        {
            invoker.AddHealthEmptiedLister(listener);
        }
    }

    public static void AddPlayerStartedStateListener(UnityAction<State.Case> listener)
    {
        playerStartedStateListeners.Add(listener);
        foreach (IPlayerStartedStateInvoker invoker in playerStartedStateInvokers)
        {
            invoker.AddPlayerStartedStateListener(listener);
        }
    }

    #endregion



}
