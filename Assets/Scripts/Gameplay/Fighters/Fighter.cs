using UnityEngine;
using UnityEngine.Events;

public abstract class Fighter : MonoBehaviour, IDamageMadeInvoker
{

    #region Fields

    // events
    private DamageMade damageMadeEvent = new DamageMade();

    // config
    protected FighterType fighterType;
    protected CharacterName characterName;
    protected PoolItem throwable;
    protected float damageTakenInCollision;

    // body parts
    protected GameObject body;
    protected GameObject head;
    protected GameObject shield;
    protected GameObject sword;
    protected GameObject leftLeg;
    protected GameObject rightLeg;

    #endregion

    #region Properties

    /// <summary>
    /// The current state of the fighter
    /// </summary>
    public abstract FighterState CurrentState { get; }

    #endregion

    #region Unity API

    private void Awake()
    {
        EventsManager.AddInvoker(this);
        EventsManager.AddHealthEmptiedListener(HandleHealthEmptiedEvent);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(CurrentState != FighterState.Covering)
        {
            string swordTag = Game.Constants.GameObjectsTags[Tag.Sword];
            bool collidesWithSword = collision.collider.gameObject.CompareTag(swordTag);
            string throwableTag = Game.Constants.GameObjectsTags[Tag.Throwable];
            bool collidesWithThrowable = collision.collider.gameObject.CompareTag(throwableTag);
            if (collidesWithSword || collidesWithThrowable)
            {
                damageMadeEvent.Invoke(damageTakenInCollision, fighterType);
            }
        }
    }

    #endregion

    #region Methods

    /// <summary>
    /// Initializes the body parts
    /// </summary>
    protected void InitializeBodyParts()
    {
        body = gameObject.transform.Find("Body").gameObject;
        head = gameObject.transform.Find("Body/Head").gameObject;
        shield = gameObject.transform.Find("Body/Shield").gameObject;
        sword = gameObject.transform.Find("Body/Sword").gameObject;
        leftLeg = gameObject.transform.Find("LeftLeg").gameObject;
        rightLeg = gameObject.transform.Find("RightLeg").gameObject;
    }

    /// <summary>
    /// Sets the head sprite of the fighter
    /// </summary>
    /// <param name="characterName">The character name of the head</param>
    protected bool SetHead(CharacterName characterName)
    {
        Sprite characterHead = Resources.Load<Sprite>(Game.ResourcesPath.Heads[characterName]);
        if (characterHead != null && head != null)
        {
            head.GetComponent<SpriteRenderer>().sprite = characterHead;
            return true;
        }
        return false;
    }

    #endregion

    #region IDamageTakenInvoker

    public void AddDamageMadeListener(UnityAction<float, FighterType> listener)
    {
        damageMadeEvent.AddListener(listener);
    }

    #endregion

    #region Event Handlers

    public void HandleHealthEmptiedEvent(FighterType fighterEmptied)
    {
        switch(fighterEmptied)
        {
            case FighterType.Enemy:
                AudioManager.Play(AudioClipName.Victory);
                break;
            case FighterType.Player:
                AudioManager.Play(AudioClipName.Die);
                break;
        }
        if (fighterEmptied == fighterType)
        {
            Destroy(gameObject);
        }
    }

    #endregion

}