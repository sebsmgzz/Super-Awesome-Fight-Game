using UnityEngine;
using UnityEngine.UI;

public class CharactersMenu : MonoBehaviour
{

    #region Fields

    [SerializeField]
    private GameObject displayGameObject;
    private Text displayText;
    private string displayPre = "Selected character:\n";

    #endregion

    #region Unity API

    private void Start()
    {
        displayText = displayGameObject.GetComponent<Text>();
        displayText.text = displayPre + (CharacterName)PlayerPrefs.GetInt("CharacterName");
    }

    #endregion

    #region Methods

    public void HandleCharacterButtonOnClickEvent(GameObject gameObject)
    {
        PlayerPrefs.SetInt("CharacterName", (int)GetCharacterName(gameObject.name));
        displayText.text = displayPre + gameObject.name;
    }

    public void HandlePlayButtonOnClickEvent()
    {
        MenuManager.GoToMenu(MenuName.Gameplay);
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
