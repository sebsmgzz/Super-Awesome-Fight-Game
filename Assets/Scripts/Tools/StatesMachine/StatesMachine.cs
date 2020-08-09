using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages states as a machine
/// </summary>
public class StatesMachine<T>
{

    #region Fields

    private State<T> defaultState;
    private State<T> currentState;
    private List<State<T>> states;
    private List<Action<T>> stateChangedListeners;

    #endregion

    #region Properties

    public int Count
    {
        get { return states.Count; }
    }

    public State<T> CurrentState
    {
        get { return currentState; }
    }

    public IList<State<T>> States
    {
        get { return states.AsReadOnly(); }
    }

    #endregion

    #region Constructor

    public StatesMachine(T defaultValue)
    {
        this.defaultState = new State<T>(defaultValue);
        this.currentState = this.defaultState;
        this.currentState.Enter();
        this.states = new List<State<T>> { defaultState };
        this.stateChangedListeners = new List<Action<T>>();
    }

    #endregion

    #region Collection Methods

    /// <summary>
    /// Finds the given state in the states machine
    /// </summary>
    /// <param name="value">The value contained by the state</param>
    /// <returns>The state itselfs</returns>
    public State<T> Find(T value)
    {
        foreach (State<T> state in states)
        {
            if (state.Value.Equals(value))
            {
                return state;
            }
        }
        return null;
    }

    /// <summary>
    /// Clears all states from the machine
    /// </summary>
    public void Clear()
    {
        foreach (State<T> state in states)
        {
            state.RemoveAllNeighbors();
        }
        for (int i = states.Count - 1; i >= 0; i--)
        {
            states.RemoveAt(i);
        }
    }

    /// <summary>
    /// Adds a new state to the machine
    /// </summary>
    /// <param name="value">The value to be hold by the state</param>
    /// <returns>True when added, false otherwise</returns>
    public bool AddState(T value)
    {
        if (Find(value) != null)
        {
            return false;
        }
        else
        {
            states.Add(new State<T>(value));
            return true;
        }
    }

    /// <summary>
    /// Removes a state from the machine
    /// </summary>
    /// <param name="value">The value holded by the state to be removed</param>
    /// <returns>True when removed, false otherwise</returns>
    public bool RemoveState(T value)
    {
        State<T> removeState = Find(value);
        if (removeState == null)
        {
            return false;
        }
        else
        {
            states.Remove(removeState);
            foreach (State<T> state in states)
            {
                state.RemoveNeighbor(removeState);
            }
            return true;
        }
    }

    /// <summary>
    /// Adds a directed transition from one state to another
    /// </summary>
    /// <param name="value1">The value of the main state</param>
    /// <param name="value2">The value of the neighbor state</param>
    /// <returns>True when transition added, false otherwise</returns>
    public bool AddTransition(T value1, T value2)
    {
        State<T> state1 = Find(value1);
        State<T> state2 = Find(value2);
        if (state1 == null || state2 == null)
        {
            return false;
        }
        else if (state1.Neighbors.Contains(state2))
        {
            return false;
        }
        else
        {
            state1.AddNeighbor(state2);
            return true;
        }
    }

    /// <summary>
    /// Removes a transition from one state to another
    /// </summary>
    /// <param name="value1">The value of the mains state</param>
    /// <param name="value2">The value of the second state</param>
    /// <returns></returns>
    public bool RemoveTransition(T value1, T value2)
    {
        State<T> state1 = Find(value1);
        State<T> state2 = Find(value2);
        if (state1 == null || state2 == null)
        {
            return false;
        }
        else if (!state1.Neighbors.Contains(state2))
        {
            return false;
        }
        else
        {
            state1.RemoveNeighbor(state2);
            return true;
        }
    }

    #endregion

    #region Methods

    /// <summary>
    /// Adds a listener to call when a change in state is made
    /// </summary>
    /// <param name="listener">The listener</param>
    public void AddStateChangedListener(Action<T> listener)
    {
        stateChangedListeners.Add(listener);
    }

    /// <summary>
    /// Updates the current state of the machine
    /// </summary>
    public void Update()
    {
        currentState.Update();
        State<T> nextState = currentState.NextState();
        if (nextState != null)
        {
            Debug.Log("Boop");
            GoTo(nextState.Value);
        }
        else if (!currentState.Active)
        {
            Debug.Log("Beep");
            GoTo(defaultState.Value);
        }
    }

    /// <summary>
    /// Forces to jump to the given state if contained in the machine
    /// </summary>
    /// <param name="value">The value of the state to jump to</param>
    /// <returns>True when moved to the givens state</returns>
    public bool GoTo(T value)
    {
        State<T> state = Find(value);
        if (state != null)
        {
            currentState = state;
            if (currentState == defaultState)
            {
                currentState.Enter();
            }
            foreach (Action<T> listener in stateChangedListeners)
            {
                listener.Invoke(currentState.Value);
            }
            return true;
        }
        return false;
    }

    #endregion

}
