using UnityEngine.Events;

public interface IHealthEmptiedInvoker
{

    /// <summary>
    /// Adds a listener to invoke when a healthbar is emptied
    /// </summary>
    /// <param name="listener">The method listenning for invoke</param>
    void AddHealthEmptiedLister(UnityAction<Fighter.Name> listener);

}
