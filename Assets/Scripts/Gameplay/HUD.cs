using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HUD : MonoBehaviour
{

    #region Fields

    // content
    Dictionary<Fighter, HealthBar> healthBars = 
        new Dictionary<Fighter, HealthBar>();

    // events
    EmptyHealth emptyHealthEvent = new EmptyHealth();

    #endregion

    #region Unity API

    void Start()
    {
        // health bars
        HealthBar[] bars = gameObject.GetComponentsInChildren<HealthBar>();
        healthBars.Add(Fighter.Enemy, bars[0]);
        healthBars.Add(Fighter.Player, bars[1]);
        // events
        EventManager.AddDamageMadeListener(HandleDamageMadeEvent);
        EventManager.AddEmptyHealthInvoker(this);
    }

    #endregion

    #region Methods

    /// <summary>
    /// Marks damage in health
    /// </summary>
    /// <param name="damaged"></param>
    private void HandleDamageMadeEvent(Fighter damaged)
    {
        HealthBar healthBar = healthBars[damaged];
        // take damage
        healthBar.TakeDamage();
        // if empty invoke event
        if(healthBar.Value <= 0)
        {
            emptyHealthEvent.Invoke(damaged);
        }
    }

    /// <summary>
    /// Adds a listener to when a healthbar becomes empty
    /// </summary>
    /// <param name="listener">The listener</param>
    public void AddEmptyHealthLister(UnityAction<Fighter> listener)
    {
        emptyHealthEvent.AddListener(listener);
    }

    #endregion

}
