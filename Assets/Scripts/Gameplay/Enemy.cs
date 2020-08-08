using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Enemy : Fighter
{

    #region Fields

    // player
    GameObject playerGameObject;
    Controller playerController;

    // control
    Action updateCheck;
    private Dictionary<State.Case, Action> updateChecks =
        new Dictionary<State.Case, Action>();
    private Dictionary<State.Case, bool> statesFlags = 
        new Dictionary<State.Case, bool>()
    {
        { State.Case.Idling, false },
        { State.Case.Crouching, false },
        { State.Case.Standing, false },
        { State.Case.Covering, false },
        { State.Case.Uncovering, false },
        { State.Case.Launching, false },
        { State.Case.Falling, false },
        { State.Case.Landing, false },
        { State.Case.FlippingLeft, false },
        { State.Case.FlippingRight, false },
        { State.Case.Running, false },
        { State.Case.Attacking, false },
        { State.Case.Throwing, false },
    };

    // config
    private float nearDistanceTriggerer = 2.2f;
    private float maxVelocity = 2f;

    #endregion

    #region Properties

    public override Name FighterName => Name.Enemy;

    protected override float MaxVelocity => maxVelocity;

    #endregion

    #region Unity API

    protected override void Awake()
    {
        // base call
        base.Awake();
        // events
        EventsManager.AddPlayerStartedStateListener(HandlePlayerStartedState);
    }

    protected override void Start()
    {
        // base call
        base.Start();
        // player
        playerGameObject = GameObject.FindGameObjectWithTag(GameConstants.PlayerTag);
        playerController = playerGameObject.GetComponent<Controller>();
        // initialize fields
        Initialize();
        updateCheck = updateChecks[State.Case.Idling];
        // set head
        base.Initialize();
        CharacterName playerCharacterName = (CharacterName)PlayerPrefs.GetInt(GameConstants.CharacterPrefKey);
        List<CharacterName> characterNames = Enum.GetValues(typeof(CharacterName)).Cast<CharacterName>().ToList<CharacterName>();
        characterNames.Remove(playerCharacterName);
        characterNames.Remove(CharacterName.Undefined);
        characterNames.Remove(CharacterName.Majo);
        CharacterName myCharacterName = characterNames[UnityEngine.Random.Range(0, characterNames.Count)];
        SetHead(myCharacterName);
        // set difficulty
        switch((DifficultyLevel)PlayerPrefs.GetInt(GameConstants.DifficultyPrefKey))
        {
            case DifficultyLevel.Easy:
                maxVelocity = 2f;
                break;
            case DifficultyLevel.Medium:
                maxVelocity = 2.5f;
                break;
            case DifficultyLevel.Hard:
                maxVelocity = 3f;
                break;
        }
    }

    protected override void Update()
    {
        // base call
        base.Update();
        // update check
        updateCheck?.Invoke();
    }

    #endregion

    #region Methods

    /// <summary>
    /// Setupds initial configuration
    /// </summary>
    private void Initialize()
    {
        // update Checks
        updateChecks.Add(State.Case.Idling, WhileIdling);
        updateChecks.Add(State.Case.Crouching, WhileCrouching);
        updateChecks.Add(State.Case.Standing, WhileStanding);
        updateChecks.Add(State.Case.Covering, WhileCovering);
        updateChecks.Add(State.Case.Uncovering, WhileUncovering);
        updateChecks.Add(State.Case.Launching, WhileLaunching);
        updateChecks.Add(State.Case.Falling, WhileFalling);
        updateChecks.Add(State.Case.Landing, WhileLanding);
        updateChecks.Add(State.Case.Attacking, WhileAttacking);
        updateChecks.Add(State.Case.FlippingLeft, WhileFlippingLeft);
        updateChecks.Add(State.Case.FlippingRight, WhileFlippingRight);
        updateChecks.Add(State.Case.Running, WhileRunning);
        updateChecks.Add(State.Case.Throwing, WhileThrowing);
        // difficulty
        DifficultyLevel difficultyLevel = (DifficultyLevel)PlayerPrefs.GetInt(GameConstants.DifficultyPrefKey);
        switch (difficultyLevel)
        {
            case DifficultyLevel.Easy:
                maxVelocity = 2f;
                break;
            case DifficultyLevel.Medium:
                maxVelocity = 3f;
                break;
            case DifficultyLevel.Hard:
                maxVelocity = 4f;
                break;
        }
    }

    /// <summary>
    /// Determines if the player if near the enemy
    /// </summary>
    /// <returns>True when player is within nearDistanceTriggerer</returns>
    private bool PlayerIsNear()
    {
        Vector3 playerPosition = playerGameObject.transform.position;
        Vector3 enemyPosition = gameObject.transform.position;
        float distance = Mathf.Sqrt(
            Mathf.Pow(playerPosition.x - enemyPosition.x, 2) +
            Mathf.Pow(playerPosition.y - enemyPosition.y, 2));
        return distance < nearDistanceTriggerer;
    }

    #endregion

    #region Event Handlers

    /// <summary>
    /// Handles a change in state on the player
    /// </summary>
    /// <param name="stateName">The state entered by the player</param>
    private void HandlePlayerStartedState(State.Case stateName)
    {
        //Debug.Log(stateName);
        //switch(stateName)
        //{
        //    case State.Case.Idling:
        //        RunToPlayer();
        //        break;
        //}
    }

    #endregion

    #region Controller Overrides

    protected override void StateOnEnter(State.Case stateName)
    {
        // updateCheck update
        updateCheck = updateChecks[stateName];
        // unflag
        //Debug.Log(stateName);
        statesFlags[stateName] = false;
    }

    protected override bool IdlingTrigger() => statesFlags[State.Case.Idling];
    protected override bool CrouchingTrigger() => statesFlags[State.Case.Crouching];
    protected override bool StandInputCheck() => statesFlags[State.Case.Standing];
    protected override bool CoveringTrigger() => statesFlags[State.Case.Covering];
    protected override bool UncoveringTrigger() => statesFlags[State.Case.Uncovering];
    protected override bool LaunchingTrigger() => statesFlags[State.Case.Launching];
    protected override bool FallingTrigger() => statesFlags[State.Case.Falling];
    protected override bool LandingTrigger() => statesFlags[State.Case.Landing];
    protected override bool FlippingLeftTrigger() => statesFlags[State.Case.FlippingLeft];
    protected override bool FlippingRightTrigger() => statesFlags[State.Case.FlippingRight];
    protected override bool RunningTrigger() => statesFlags[State.Case.Running];
    protected override bool AttackingTrigger() => statesFlags[State.Case.Attacking];
    protected override bool ThrowingTrigger() => statesFlags[State.Case.Throwing];

    #endregion

    #region WhileOnState

    private void WhileIdling()
    {
        // check run controls
        if(!PlayerIsNear())
        {
            bool playerIsToRightOfEnemy =
                gameObject.transform.position.x < playerGameObject.transform.position.x;
            statesFlags[State.Case.FlippingLeft] = !playerIsToRightOfEnemy;
            statesFlags[State.Case.FlippingRight] = playerIsToRightOfEnemy;
            statesFlags[State.Case.Running] = true;
        }
        else
        {
            statesFlags[State.Case.Attacking] = true;
        }
    }
    private void WhileCrouching() { }
    private void WhileStanding() { }
    private void WhileCovering() { }
    private void WhileUncovering() { }
    private void WhileLaunching() { }
    private void WhileFalling() { }
    private void WhileLanding() { }
    private void WhileFlippingLeft() { }
    private void WhileFlippingRight() { }
    private void WhileRunning()
    {
        statesFlags[State.Case.Running] = true;
        if (PlayerIsNear())
        {
            statesFlags[State.Case.Running] = false;
            statesFlags[State.Case.Idling] = true;
        }
        else 
        {
            bool facingRight = gameObject.transform.localScale.x == -1;
            bool playerLocatedRight =
                gameObject.transform.position.x < playerGameObject.transform.position.x;
            statesFlags[State.Case.FlippingLeft] = facingRight && !playerLocatedRight;
            statesFlags[State.Case.FlippingRight] = !facingRight && playerLocatedRight;
        }
    }
    private void WhileAttacking()
    {
        statesFlags[State.Case.Idling] = statesTimer.Finished;
    }
    private void WhileThrowing() { }

    #endregion

}
