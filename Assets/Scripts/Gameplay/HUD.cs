using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HUD : MonoBehaviour
{

    #region Fields

    // content
    Dictionary<Fighter.Name, HealthBar> healthBars = 
        new Dictionary<Fighter.Name, HealthBar>();

    // events
    HealthEmptied healthEmptiedEvent = new HealthEmptied();

    #endregion

    #region Unity API

    void Start()
    {
        // health bars
        HealthBar[] bars = gameObject.GetComponentsInChildren<HealthBar>();
        healthBars.Add(Fighter.Name.Enemy, bars[0]);
        healthBars.Add(Fighter.Name.Player, bars[1]);
        // name
        CharacterName characterName = (CharacterName)PlayerPrefs.GetInt("CharacterName");
        bars[1].Text = characterName.ToString();
        // debug
        DifficultyLevel difficultyLevel = (DifficultyLevel)PlayerPrefs.GetInt("DifficultyLevel");
        // events
        EventsManager.AddDamageTakenListener(HandleDamageMadeEvent);
        EventsManager.AddHealthEmptiedInvoker(this);
    }

    #endregion

    #region Methods

    /// <summary>
    /// Marks damage in player health
    /// </summary>
    private void HandleDamageMadeEvent(Fighter.Name fighterDamaged)
    {
        HealthBar healthBar = healthBars[fighterDamaged];
        // take damage
        healthBar.TakeDamage();
        // if empty invoke event
        if(healthBar.Value <= 0)
        {
            healthEmptiedEvent.Invoke(fighterDamaged);
        }
    }

    public void AddHealthEmptiedLister(UnityAction<Fighter.Name> listener)
    {
        healthEmptiedEvent.AddListener(listener);
    }

    #endregion

}
