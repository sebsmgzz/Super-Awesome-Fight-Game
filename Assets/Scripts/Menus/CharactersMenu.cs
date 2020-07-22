using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CharactersMenu : MonoBehaviour
{

    #region Fields

    [SerializeField]
    private GameObject selectedCharacterTextGameObject;
    private Text selectedCharacterText;
    private string selectedCharacterTextPre = "Selected character:\n";
    private CharacterName selectedCharacter;

    #endregion

    #region Unity API

    private void Start()
    {
        selectedCharacterText = selectedCharacterTextGameObject.GetComponent<Text>();
    }

    #endregion

    #region Methods

    public void HandleCharacterButtonOnClickEvent(GameObject gameObject)
    {
        selectedCharacter = GetCharacterName(gameObject.name);
        selectedCharacterText.text = selectedCharacterTextPre + gameObject.name;
        Debug.Log(selectedCharacter);
    }

    public void HandlePlayButtonOnClickEvent()
    {

    }

    private CharacterName GetCharacterName(string buttonGameObjectName)
    {
        switch (buttonGameObjectName)
        {
            case "Sebas":
                return CharacterName.Sebas;
            case "Camacho":
                return CharacterName.Camacho;
            case "Chavarria":
                return CharacterName.Chavarria;
            case "Ramon":
                return CharacterName.Ramon;
            case "Joaquin":
                return CharacterName.Joaquin;
            case "Maritza":
                return CharacterName.Maritza;
            case "Majo":
                return CharacterName.Majo;
            default:
                return CharacterName.Undefined;
        }
    }

    #endregion

}
