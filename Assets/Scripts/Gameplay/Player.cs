using System;
using UnityEngine;
using UnityEngine.Events;

public class Player : Fighter
{

    #region Fields

    private PlayerController controls;

    #endregion

    #region Properties

    public override Name FighterName => Name.Player;

    public override bool CanTakeDamage => controls.CurrentState != Controller.StateName.Covering;

    #endregion

    #region Unity API

    protected override void Start()
    {
        base.Start();
        // initialize head
        CharacterName characterName = (CharacterName)PlayerPrefs.GetInt(GameConstants.CharacterPrefKey);
        SetHead(characterName);
        // controls
        controls = gameObject.AddComponent<PlayerController>();
    }

    #endregion

}
