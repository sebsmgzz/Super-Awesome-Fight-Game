using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{

    #region Fields
        
    // components
    Rigidbody2D rb2d;
    PlayerStatesManager psm;

    // events
    DamageMade damageMadeEvent = new DamageMade();

    #endregion

    #region Unity API

    public void Start()
    {
        // components
        rb2d = GetComponent<Rigidbody2D>();
        psm = GetComponent<PlayerStatesManager>();
        // events
        EventsManager.AddDamageMadeInvoker(this);
        EventsManager.AddEmptyHealthListener(HandleEmptyHealthEvent);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (psm == null)
            return;
        if(psm.Attacking)
        {
            switch (collision.gameObject.tag)
            {
                case "Enemy":
                    damageMadeEvent.Invoke(Fighter.Enemy);
                    break;
                case "Player":
                    damageMadeEvent.Invoke(Fighter.Player);
                    break;
            }
        }
    }

    #endregion

    #region Methods

    /// <summary>
    /// Adds a listener to call when damage is taken
    /// </summary>
    /// <param name="listener"></param>
    public void AddDamageMadeListener(UnityAction<Fighter> listener)
    {
        damageMadeEvent.AddListener(listener);
    }

    /// <summary>
    /// Handles when a healthbar is emptied
    /// </summary>
    /// <param name="fighterEmptied">The fighter whose health bar was emptied</param>
    public void HandleEmptyHealthEvent(Fighter fighterEmptied)
    {
        string fighterEmptiedTag;
        switch (fighterEmptied)
        {
            case Fighter.Enemy:
                fighterEmptiedTag = "Enemy";
                break;
            case Fighter.Player:
                fighterEmptiedTag = "Player";
                break;
            default:
                return;
        }
        if(gameObject.CompareTag(fighterEmptiedTag))
        {
            Destroy(gameObject);
        }
    }

    #endregion

}
