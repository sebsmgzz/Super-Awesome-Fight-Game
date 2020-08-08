using UnityEngine.Events;

public interface IDamageTakenInvoker
{

    /// <summary>
    /// Adds a listener to invoke when damage is taken
    /// </summary>
    /// <param name="listener">The method listenning for invoke</param>
    void AddDamageTakenListener(UnityAction<Fighter.Name> listener);

}
