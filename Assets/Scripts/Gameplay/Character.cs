using UnityEngine;
using UnityEngine.Events;

public class Character : MonoBehaviour
{

    #region Fields
        
    // components
    Rigidbody2D rb2d;
    ControlsManager controls;

    // events
    DamageMade damageMadeEvent = new DamageMade();

    #endregion

    #region Unity API

    public void Start()
    {
        // components
        rb2d = GetComponent<Rigidbody2D>();
        controls = GetComponent<ControlsManager>();
        // events
        EventManager.AddDamageMadeInvoker(this);
        EventManager.AddEmptyHealthListener(HandleEmptyHealthEvent);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (controls == null)
            return;
        if(controls.Attacking)
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
