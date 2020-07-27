using UnityEngine;

public class DifficultyMenu : MonoBehaviour
{

    public void HandleEasyButtonOnClickEvent()
    {
        PlayerPrefs.SetInt("DifficultyLevel", (int)DifficultyLevel.Easy);
        MenuManager.GoToMenu(MenuName.Characters);
    }

    public void HandleMediumButtonOnClickEvent()
    {
        PlayerPrefs.SetInt("DifficultyLevel", (int)DifficultyLevel.Medium);
        MenuManager.GoToMenu(MenuName.Characters);
    }

    public void HandleHardButtonOnClickEvent()
    {
        PlayerPrefs.SetInt("DifficultyLevel", (int)DifficultyLevel.Hard);
        MenuManager.GoToMenu(MenuName.Characters);
    }

}
