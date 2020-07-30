using UnityEngine;
using UnityEngine.Events;

public abstract class Fighter : Controller
{

    #region Fields

    // events
    protected DamageTaken damageTakenEvent = new DamageTaken();

    // colliders
    protected Collider2D body;
    protected Collider2D head;
    protected Collider2D shield;
    protected Collider2D sword;
    protected Collider2D leftLeg;
    protected Collider2D rightLeg;

    #endregion

    #region Properties

    /// <summary>
    /// The name of the fighter
    /// </summary>
    public abstract Name FighterName { get; }

    #endregion

    #region Unity API

    protected virtual void Awake()
    {
        // events
        EventsManager.AddDamageTakenInvoker(this);
        EventsManager.AddHealthEmptiedListener(HandleHealthEmptiedEvent);
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        // call base
        base.OnCollisionEnter2D(collision);
        // call events
        if (collision.collider.gameObject.CompareTag("Sword"))
        {
            damageTakenEvent.Invoke(FighterName);
        }
    }

    #endregion

    #region Methods

    /// <summary>
    /// Initialize colliders fields
    /// </summary>
    protected void InitializeColliders()
    {
        // colliders
        body = gameObject.GetComponentInChildren<CapsuleCollider2D>();
        head = gameObject.GetComponentInChildren<CircleCollider2D>();
        shield = gameObject.GetComponentsInChildren<PolygonCollider2D>()[0];
        sword = gameObject.GetComponentsInChildren<PolygonCollider2D>()[1];
        leftLeg = gameObject.GetComponentsInChildren<EdgeCollider2D>()[0];
        rightLeg = gameObject.GetComponentsInChildren<EdgeCollider2D>()[1];
    }

    /// <summary>
    /// Adds a listener to call when damage is taken
    /// </summary>
    /// <param name="listener">The method listenning for invoke</param>
    public void AddDamageTakenListener(UnityAction<Name> listener)
    {
        damageTakenEvent.AddListener(listener);
    }

    /// <summary>
    /// Handles when a healthbar is emptied
    /// </summary>
    /// <param name="fighterEmptied">The fighter whose health bar was emptied</param>
    public void HandleHealthEmptiedEvent(Name fighterEmptied)
    {
        if (fighterEmptied == FighterName)
        {
            Destroy(gameObject);
        }
    }

    #endregion

    #region Enums

    public enum Name
    {
        Enemy,
        Player
    }

    #endregion

}