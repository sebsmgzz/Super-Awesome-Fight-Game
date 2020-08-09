using System;
using System.Collections.Generic;

public class State2
{

    #region Fields

    private Func<bool> triggerer = new Func<bool>(() => false);
    private Action action = null;
    private Action onEnter = null;
    private Action onExit = null;

    private bool active = false;
    private bool selfDeactivates = false;

    private List<State2> neighbors = new List<State2>();

    #endregion

    #region Property

    /// <summary>
    /// Determines if the state should start/continue to execute
    /// </summary>
    public Func<bool> Triggerer
    {
        get { return triggerer; }
        set
        {
            if(triggerer != null)
            {
                triggerer = value;
            }
        }
    }

    /// <summary>
    /// The action to be executed every update
    /// </summary>
    public Action Action
    {
        get { return action; }
        set { action = value; }
    }

    /// <summary>
    /// Executed once, when entering the state
    /// </summary>
    public Action OnEnter
    {
        get { return onEnter; }
        set { onEnter = value; }
    }

    /// <summary>
    /// Executed once, when exiting the state
    /// </summary>
    public Action OnExit
    {
        get { return onExit; }
        set { onExit = value; }
    }

    /// <summary>
    /// True when the state is currently executing
    /// </summary>
    public bool Active
    {
        get { return active; }
    }

    /// <summary>
    /// Determines if the state can self deactivate itself during an update,
    /// when no trigger is provided
    /// </summary>
    /// <returns>True to self deactivate, false to deactivate manually</returns>
    public bool SelfDeactivates
    {
        get { return selfDeactivates; }
        set { selfDeactivates = value; }
    }

    #endregion

    #region Constructor

    #endregion

    #region Methods

    /// <summary>
    /// Check if the states been triggered
    /// </summary>
    /// <returns>True if the states has been trigger, false otherwise</returns>
    public bool CheckTriggerer()
    {
        if(triggerer.Invoke())
        {
            Enter();
            return true;
        }
        return false;
    }

    /// <summary>
    /// Forces to enter the state
    /// </summary>
    public void Enter()
    {
        active = true;
        onEnter?.Invoke();
    }

    /// <summary>
    /// Executes the action of the active state
    /// </summary>
    public void Update()
    {
        if(active)
        {
            action?.Invoke();
            if(selfDeactivates && !triggerer.Invoke())
            {
                Exit();
            }
        }
    }

    /// <summary>
    /// Gets the next state triggered in the list of neighbors
    /// </summary>
    /// <returns>The next state triggered</returns>
    public State2 NextState()
    {
        foreach (State2 neighbor in neighbors)
        {
            if (neighbor.CheckTriggerer())
            {
                return neighbor;
            }
        }
        return null;
    }

    /// <summary>
    /// Forces to exit the state
    /// </summary>
    public void Exit()
    {
        if(active)
        {
            active = false;
            onExit?.Invoke();
        }
    }

    /// <summary>
    /// Adds the given state as a neighbor for this state
    /// </summary>
    /// <param name="neighbor">Neighbor to add</param>
    /// <returns>True if the neighbor was added, false otherwise</returns>
    public bool AddNeighbor(State2 neighbor)
    {
        if (neighbors.Contains(neighbor))
        {
            return false;
        }
        else
        {
            neighbors.Add(neighbor);
            return true;
        }
    }

    /// <summary>
    /// Removes the given state as a neighbor for this state
    /// </summary>
    /// <param name="neighbor">State to remove</param>
    /// <returns>True if the state was removed, false otherwise</returns>
    public bool RemoveNeighbor(State2 neighbor)
    {
        return neighbors.Remove(neighbor);
    }

    /// <summary>
    /// Removes all the neighbor states from this state
    /// </summary>
    /// <returns>True if the neighbors were removed, false otherwise</returns>
    public bool RemoveAllNeighbor()
    {
        for (int i = neighbors.Count - 1; i >= 0; i--)
        {
            neighbors.RemoveAt(i);
        }
        return true;
    }

    #endregion

}
