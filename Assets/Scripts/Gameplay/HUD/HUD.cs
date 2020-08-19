using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HUD : MonoBehaviour, IHealthEmptiedInvoker
{

    #region Fields

    // events
    private HealthEmptied healthEmptiedEvent = new HealthEmptied();

    // content
    private Dictionary<FighterType, HealthBar> healthBars;

    #endregion

    #region Unity API

    private void Awake()
    {
        EventsManager.AddDamageMadeListener(HandleDamageMadeEvent);
        EventsManager.AddInvoker(this);
    }

    private void Start()
    {
        // health bars
        HealthBar[] bars = gameObject.GetComponentsInChildren<HealthBar>();
        healthBars = new Dictionary<FighterType, HealthBar>
        {
            { FighterType.Enemy, bars[0] },
            { FighterType.Player, bars[1] }
        };
        // name
        string preferenceKey = Game.Constants.PreferencesKeys[Preference.Character];
        CharacterName characterName = (CharacterName)PlayerPrefs.GetInt(preferenceKey);
        bars[1].Text = characterName.ToString();
    }

    #endregion

    #region IHealthEmptiedInvoker

    public void AddHealthEmptiedLister(UnityAction<FighterType> listener)
    {
        healthEmptiedEvent.AddListener(listener);
    }

    #endregion

    #region Events Handlers

    /// <summary>
    /// Marks damage in player health
    /// </summary>
    private void HandleDamageMadeEvent(float damageAmount, FighterType fighterDamaged)
    {
        // take damage
        HealthBar healthBar = healthBars[fighterDamaged];
        healthBar.TakeDamage(damageAmount);
        // if empty invoke event
        if(healthBar.Value <= 0)
        {
            healthEmptiedEvent.Invoke(fighterDamaged);
        }
    }

    #endregion

}
