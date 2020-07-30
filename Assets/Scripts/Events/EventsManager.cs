using System.Collections.Generic;
using UnityEngine.Events;

/// <summary>
/// The event manager
/// </summary>
public static class EventsManager
{

    #region DamageTaken

    private static List<Fighter> damageTakenInvokers = 
        new List<Fighter>();
    private static List<UnityAction<Fighter.Name>> damageTakenListeners = 
        new List<UnityAction<Fighter.Name>>();

    public static void AddDamageTakenInvoker(Fighter invoker)
    {
        damageTakenInvokers.Add(invoker);
        foreach (UnityAction<Fighter.Name> listener in damageTakenListeners)
        {
            invoker.AddDamageTakenListener(listener);
        }
    }

    public static void AddDamageTakenListener(UnityAction<Fighter.Name> listener)
    {
        damageTakenListeners.Add(listener);
        foreach (Fighter invoker in damageTakenInvokers)
        {
            invoker.AddDamageTakenListener(listener);
        }
    }

    #endregion

    #region HealthEmptied

    private static List<HUD> healthEmptiedInvokers =
        new List<HUD>();
    private static List<UnityAction<Fighter.Name>> healthEmptiedListeners =
        new List<UnityAction<Fighter.Name>>();

    public static void AddHealthEmptiedInvoker(HUD invoker)
    {
        healthEmptiedInvokers.Add(invoker);
        foreach (UnityAction<Fighter.Name> listener in healthEmptiedListeners)
        {
            invoker.AddHealthEmptiedLister(listener);
        }
    }

    public static void AddHealthEmptiedListener(UnityAction<Fighter.Name> listener)
    {
        healthEmptiedListeners.Add(listener);
        foreach (HUD invoker in healthEmptiedInvokers)
        {
            invoker.AddHealthEmptiedLister(listener);
        }
    }

    #endregion

    #region PlayerStartedState

    private static List<Player> playerStartedStateInvokers =
        new List<Player>();
    private static List<UnityAction<State.Case>> playerStartedStateListeners =
        new List<UnityAction<State.Case>>();

    public static void AddPlayerStartedStateInvoker(Player invoker)
    {
        playerStartedStateInvokers.Add(invoker);
        foreach (UnityAction<State.Case> listener in playerStartedStateListeners)
        {
            invoker.AddPlayerStartedStateListener(listener);
        }
    }

    public static void AddPlayerStartedStateListener(UnityAction<State.Case> listener)
    {
        playerStartedStateListeners.Add(listener);
        foreach (Player invoker in playerStartedStateInvokers)
        {
            invoker.AddPlayerStartedStateListener(listener);
        }
    }

    #endregion

}
