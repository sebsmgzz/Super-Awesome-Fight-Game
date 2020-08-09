using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class Fighter : MonoBehaviour, IDamageTakenInvoker 
{

    #region Fields

    // events
    private DamageTaken damageTakenEvent = new DamageTaken();

    // colliders
    protected GameObject body;
    protected GameObject head;
    protected GameObject shield;
    protected GameObject sword;
    protected GameObject leftLeg;
    protected GameObject rightLeg;

    // heads
    private Dictionary<CharacterName, string> heads =
        new Dictionary<CharacterName, string>()
        {
            { CharacterName.Sebas, "Heads/Sebastian" },
            { CharacterName.Camacho, "Heads/Camacho" },
            { CharacterName.Chavarria, "Heads/Chavarria" },
            { CharacterName.Ramon, "Heads/Ramon" },
            { CharacterName.Joaquin, "Heads/Joaquin" },
            { CharacterName.Maritza, "Heads/Maritza" },
            { CharacterName.Majo, "Heads/Majo" }
        };

    #endregion

    #region Properties

    /// <summary>
    /// The name of the fighter
    /// </summary>
    public abstract Name FighterName { get; }

    /// <summary>
    /// Determinse if the fighter can take damage
    /// </summary>
    public abstract bool CanTakeDamage { get; }

    #endregion

    #region Unity API

    protected virtual void Awake()
    {
        // events
        EventsManager.AddInvoker(this);
        EventsManager.AddHealthEmptiedListener(HandleHealthEmptiedEvent);
    }

    protected virtual void Start()
    {
        // body parts
        body = gameObject.transform.Find("Body").gameObject;
        head = gameObject.transform.Find("Body/Head").gameObject;
        shield = gameObject.transform.Find("Body/Shield").gameObject;
        sword = gameObject.transform.Find("Body/Sword").gameObject;
        leftLeg = gameObject.transform.Find("LeftLeg").gameObject;
        rightLeg = gameObject.transform.Find("RightLeg").gameObject;
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.CompareTag(GameConstants.SwordTag) && CanTakeDamage)
        {
            damageTakenEvent.Invoke(FighterName);
        }
    }

    #endregion

    #region Methods

    /// <summary>
    /// Sets the head sprite of the fighter
    /// </summary>
    /// <param name="characterName">The character name of the head</param>
    protected void SetHead(CharacterName characterName)
    {
        Sprite characterHead = Resources.Load<Sprite>(heads[characterName]);
        if (characterHead != null && head != null)
        {
            head.GetComponent<SpriteRenderer>().sprite = characterHead;
        }
    }

    #endregion

    #region IDamageTakenInvoker

    public void AddDamageTakenListener(UnityAction<Name> listener)
    {
        damageTakenEvent.AddListener(listener);
    }

    #endregion

    #region Event Handlers

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