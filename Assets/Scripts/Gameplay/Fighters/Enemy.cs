using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Enemy : Fighter
{

    #region Fields

    private EnemyController controls;

    #endregion

    #region Properties

    public override FighterState CurrentState => controls.CurrentState;

    #endregion

    #region Unity API

    private void Start()
    {
        base.InitializeBodyParts();
        // fighterType
        fighterType = FighterType.Enemy;
        // character
        InitializeCharacter();
        // head
        SetHead(characterName);
        // throwable
        throwable = Pools.DefaultThrowable(characterName);
        // controls
        controls = gameObject.GetComponent<EnemyController>();
        controls.AddControllerChangedStateListener(HandleControllerChangedStateEvent);
        // difficulty
        InitializeDifficulty();
    }

    #endregion

    #region Methods

    private void InitializeCharacter()
    {
        string characterKey = Game.Constants.PreferencesKeys[Preference.Character];
        CharacterName playerCharacterName = (CharacterName)PlayerPrefs.GetInt(characterKey);
        IEnumerable<CharacterName> characterNames = Enum.GetValues(typeof(CharacterName)).Cast<CharacterName>();
        List<CharacterName> characterNamesList = characterNames.ToList();
        characterNamesList.Remove(playerCharacterName);
        characterName = characterNamesList[UnityEngine.Random.Range(0, characterNamesList.Count)];
    }

    private void InitializeDifficulty()
    {
        string difficultyKey = Game.Constants.PreferencesKeys[Preference.Difficulty];
        switch ((DifficultyLevel)PlayerPrefs.GetInt(difficultyKey))
        {
            case DifficultyLevel.Easy:
                damageTakenInCollision = ConfigurationManager.DefaultDamageForce + 2;
                controls.MaxRunVelocity -= 1f;
                break;
            case DifficultyLevel.Medium:
                damageTakenInCollision = ConfigurationManager.DefaultDamageForce;
                break;
            case DifficultyLevel.Hard:
                damageTakenInCollision = ConfigurationManager.DefaultDamageForce - 2;
                controls.MaxRunVelocity += 1f;
                break;
        }
    }

    #endregion

    #region Events Handlers

    private void HandleControllerChangedStateEvent(FighterState previousState, FighterState nextState)
    {
        //Debug.Log(nextState);
        if (nextState == FighterState.Throwing)
        {
            GameObject throwableGO = Pools.FishFor(throwable);
            throwableGO.layer = Game.Constants.Layers[Layer.EnemyThrowable];
            throwableGO.transform.position = new Vector3(
                gameObject.transform.position.x,
                gameObject.transform.position.y,
                gameObject.transform.position.z);
            float direction = (gameObject.transform.localScale.x < 0) ? 1 : -1;
            throwableGO.GetComponent<Rigidbody2D>().AddForce(
                new Vector2(direction * 5, 5), ForceMode2D.Impulse);
        }
    }

    #endregion

}
