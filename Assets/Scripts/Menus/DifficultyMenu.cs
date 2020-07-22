using UnityEngine;

public class DifficultyMenu : MonoBehaviour
{

    public void HandleEasyButtonOnClickEvent()
    {
        ConfigurationManager.DifficultyLevelSelected = DifficultyLevel.Easy;
        MenuManager.GoToMenu(MenuName.Characters);
    }

    public void HandleMediumButtonOnClickEvent()
    {
        ConfigurationManager.DifficultyLevelSelected = DifficultyLevel.Medium;
        MenuManager.GoToMenu(MenuName.Characters);
    }

    public void HandleHardButtonOnClickEvent()
    {
        ConfigurationManager.DifficultyLevelSelected = DifficultyLevel.Hard;
        MenuManager.GoToMenu(MenuName.Characters);
    }

}
