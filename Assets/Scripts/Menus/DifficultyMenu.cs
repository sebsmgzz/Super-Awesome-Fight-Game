using UnityEngine;

public class DifficultyMenu : MonoBehaviour
{

    public void HandleEasyButtonOnClickEvent()
    {
        PlayerPrefs.SetInt(GameConstants.DifficultyPrefKey, (int)DifficultyLevel.Easy);
        MenuManager.GoToMenu(MenuName.Characters);
    }

    public void HandleMediumButtonOnClickEvent()
    {
        PlayerPrefs.SetInt(GameConstants.DifficultyPrefKey, (int)DifficultyLevel.Medium);
        MenuManager.GoToMenu(MenuName.Characters);
    }

    public void HandleHardButtonOnClickEvent()
    {
        PlayerPrefs.SetInt(GameConstants.DifficultyPrefKey, (int)DifficultyLevel.Hard);
        MenuManager.GoToMenu(MenuName.Characters);
    }

}
