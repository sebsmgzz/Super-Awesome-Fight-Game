using UnityEngine;

public class DifficultyMenu : MonoBehaviour
{

    public void HandleEasyButtonOnClickEvent()
    {
        PlayerPrefs.SetInt(Game.Constants.PreferencesKeys[Preference.Difficulty], (int)DifficultyLevel.Easy);
        MenuManager.GoToMenu(MenuName.Characters);
    }

    public void HandleMediumButtonOnClickEvent()
    {
        PlayerPrefs.SetInt(Game.Constants.PreferencesKeys[Preference.Difficulty], (int)DifficultyLevel.Medium);
        MenuManager.GoToMenu(MenuName.Characters);
    }

    public void HandleHardButtonOnClickEvent()
    {
        PlayerPrefs.SetInt(Game.Constants.PreferencesKeys[Preference.Difficulty], (int)DifficultyLevel.Hard);
        MenuManager.GoToMenu(MenuName.Characters);
    }

}
