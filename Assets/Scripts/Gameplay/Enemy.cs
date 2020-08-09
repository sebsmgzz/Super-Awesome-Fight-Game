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

    public override Name FighterName => Name.Enemy;

    public override bool CanTakeDamage => controls.CurrentState != Controller.StateName.Covering;

    #endregion

    #region Unity API

    protected override void Start()
    {
        base.Start();
        controls = gameObject.AddComponent<EnemyController>();
        InitializeHead();
        InitializeDifficulty();
    }

    #endregion

    #region Methods

    private void InitializeHead()
    {
        CharacterName playerCharacterName = (CharacterName)PlayerPrefs.GetInt(GameConstants.CharacterPrefKey);
        List<CharacterName> characterNames = Enum.GetValues(typeof(CharacterName)).Cast<CharacterName>().ToList<CharacterName>();
        characterNames.Remove(playerCharacterName);
        characterNames.Remove(CharacterName.Majo);   // missing sprite
        CharacterName myCharacterName = characterNames[UnityEngine.Random.Range(0, characterNames.Count)];
        SetHead(myCharacterName);
    }

    private void InitializeDifficulty()
    {
        switch ((DifficultyLevel)PlayerPrefs.GetInt(GameConstants.DifficultyPrefKey))
        {
            case DifficultyLevel.Easy:
                controls.MaxRunVelocity = 2f;
                break;
            case DifficultyLevel.Medium:
                controls.MaxRunVelocity = 2.5f;
                break;
            case DifficultyLevel.Hard:
                controls.MaxRunVelocity = 3f;
                break;
        }
    }

    #endregion

}
