using UnityEngine;

public class Player : Fighter
{

    #region Fields

    private PlayerController controls;

    #endregion

    #region Properties

    public override FighterState CurrentState => controls.CurrentState;

    #endregion

    #region Unity API

    private void Start()
    {
        base.InitializeBodyParts();
        // fighterType
        fighterType = FighterType.Player;
        // character name
        characterName = (CharacterName)PlayerPrefs.GetInt(Game.Constants.PreferencesKeys[Preference.Character]);
        // throwable
        throwable = Pools.DefaultThrowable(characterName);
        // strenght
        damageTakenInCollision = ConfigurationManager.DefaultDamageForce;
        // head
        SetHead(characterName);
        // controls
        controls = gameObject.GetComponent<PlayerController>();
        controls.AddControllerChangedStateListener(HandleControllerChangedStateEvent);
    }

    #endregion

    #region Events Handler

    private void HandleControllerChangedStateEvent(FighterState previousState, FighterState nextState)
    {
        if(nextState == FighterState.Throwing)
        {
            GameObject throwableGO = Pools.FishFor(throwable);
            throwableGO.layer = Game.Constants.Layers[Layer.PlayerThrowable];
            throwableGO.transform.position = new Vector3(
                gameObject.transform.position.x,
                gameObject.transform.position.y,
                gameObject.transform.position.z);
            float direction = (gameObject.transform.localScale.x < 0) ? 1 : -1;
            throwableGO.GetComponent<Rigidbody2D>().AddForce(
                new Vector2(direction * 5, 5), ForceMode2D.Impulse);
        }
        else if(nextState == FighterState.Attacking)
        {
            AudioManager.Play(AudioClipName.Attack);
        }
        else if(nextState == FighterState.Launching)
        {
            AudioManager.Play(AudioClipName.Jump);
        }
    }

    #endregion

}
