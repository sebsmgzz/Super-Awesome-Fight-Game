using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class Fighter : Controller, IDamageTakenInvoker 
{

    #region Fields

    // events
    protected DamageTaken damageTakenEvent = new DamageTaken();

    // colliders
    protected GameObject body;
    protected GameObject head;
    protected GameObject shield;
    protected GameObject sword;
    protected GameObject leftLeg;
    protected GameObject rightLeg;

    // heads
    private Dictionary<CharacterName, Sprite> heads =
        new Dictionary<CharacterName, Sprite>();
    [SerializeField] private Sprite sebasHead;
    [SerializeField] private Sprite camachoHead;
    [SerializeField] private Sprite chavarriaHead;
    [SerializeField] private Sprite ramonHead;
    [SerializeField] private Sprite joaquinHead;
    [SerializeField] private Sprite maritzaHead;
    [SerializeField] private Sprite majoHead;

    #endregion

    #region Properties

    /// <summary>
    /// The name of the fighter
    /// </summary>
    public abstract Name FighterName { get; }

    #endregion

    #region IDamageTakenInvoker

    public void AddDamageTakenListener(UnityAction<Name> listener)
    {
        damageTakenEvent.AddListener(listener);
    }

    #endregion

    #region Unity API

    protected virtual void Awake()
    {
        // events
        EventsManager.AddInvoker(this);
        EventsManager.AddHealthEmptiedListener(HandleHealthEmptiedEvent);
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        // call base
        base.OnCollisionEnter2D(collision);
        // call events
        if (collision.collider.gameObject.CompareTag(GameConstants.SwordTag) && !Covering)
        {
            damageTakenEvent.Invoke(FighterName);
        }
    }

    #endregion

    #region Methods

    /// <summary>
    /// Initializes fields of fighter
    /// </summary>
    protected void Initialize()
    {
        // body parts
        body = gameObject.transform.Find("Body").gameObject;
        head = gameObject.transform.Find("Body/Head").gameObject;
        shield = gameObject.transform.Find("Body/Shield").gameObject;
        sword = gameObject.transform.Find("Body/Sword").gameObject;
        leftLeg = gameObject.transform.Find("LeftLeg").gameObject;
        rightLeg = gameObject.transform.Find("RightLeg").gameObject;
        // heads
        heads.Add(CharacterName.Sebas, sebasHead);
        heads.Add(CharacterName.Camacho, camachoHead);
        heads.Add(CharacterName.Chavarria, chavarriaHead);
        heads.Add(CharacterName.Ramon, ramonHead);
        heads.Add(CharacterName.Joaquin, joaquinHead);
        heads.Add(CharacterName.Maritza, maritzaHead);
        heads.Add(CharacterName.Majo, majoHead);
    }

    protected void SetHead(CharacterName characterName)
    {
        Sprite characterHead = heads[characterName];
        if (characterHead != null)
        {
            head.GetComponent<SpriteRenderer>().sprite = characterHead;
        }
    }

    #endregion

    #region Event Handlers

    /// <summary>
    /// Invoked when a healthbar is emptied
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