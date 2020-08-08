using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.Events;

public class Player : Fighter, IPlayerStartedStateInvoker
{

    #region Fields

    // events
    private PlayerStartedState playerStartedStateEvent = new PlayerStartedState();

    #endregion

    #region Properties

    public override Name FighterName => Name.Player;

    protected override float MaxVelocity => 5f;

    #endregion

    #region IPlayerStartedStateInvoker

    public void AddPlayerStartedStateListener(UnityAction<State.Case> listener)
    {
        playerStartedStateEvent.AddListener(listener);
    }

    #endregion

    #region Unity API

    protected override void Awake()
    {
        // base call
        base.Awake();
        // events
        EventsManager.AddInvoker((IPlayerStartedStateInvoker)this);
    }

    protected override void Start()
    {
        // base call
        base.Start();
        Initialize();
        // haad
        CharacterName characterName = (CharacterName)PlayerPrefs.GetInt(GameConstants.CharacterPrefKey);
        SetHead(characterName);
    }

    protected override void Update()
    {
        // base call
        base.Update();
    }

    #endregion

    #region Controller Overrides

    protected override void StateOnEnter(State.Case stateName)
    {
        // events
        playerStartedStateEvent.Invoke(stateName);
    }

    protected override bool IdlingTrigger() => statesTimer.Finished;
    protected override bool CrouchingTrigger() => Input.GetKey(KeyCode.S);
    protected override bool StandInputCheck() => !Input.GetKey(KeyCode.S);
    protected override bool CoveringTrigger() => Input.GetKey(KeyCode.E);
    protected override bool UncoveringTrigger() => !Input.GetKey(KeyCode.E);
    protected override bool LaunchingTrigger() => Input.GetKeyDown(KeyCode.W);
    protected override bool FallingTrigger() => statesTimer.Finished;
    protected override bool LandingTrigger() => landed;
    protected override bool FlippingLeftTrigger() => Input.GetKeyDown(KeyCode.A);
    protected override bool FlippingRightTrigger() => Input.GetKeyDown(KeyCode.D);
    protected override bool RunningTrigger() => Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D);
    protected override bool AttackingTrigger() => Input.GetKeyDown(KeyCode.Q);
    protected override bool ThrowingTrigger() => Input.GetKeyDown(KeyCode.Space);

    #endregion

}
