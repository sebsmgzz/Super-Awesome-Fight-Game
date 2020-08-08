using UnityEngine.Events;

public interface IPlayerStartedStateInvoker
{

    /// <summary>
    /// Adds a listener to the start of a state
    /// </summary>
    /// <param name="listener">The listener the the start of a state</param>
    void AddPlayerStartedStateListener(UnityAction<State.Case> listener);

}
