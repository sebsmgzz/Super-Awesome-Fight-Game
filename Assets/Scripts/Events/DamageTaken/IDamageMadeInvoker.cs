using UnityEngine.Events;

public interface IDamageMadeInvoker
{

    /// <summary>
    /// Adds a listener to invoke when damage is made
    /// </summary>
    /// <param name="listener">The method listenning for invoke</param>
    void AddDamageMadeListener(UnityAction<float, FighterType> listener);

}
