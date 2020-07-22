using UnityEngine;
using UnityEngine.SceneManagement;

public static class MenuManager
{
    public static void GoToMenu(MenuName menu)
    {
        switch (menu)
        {
            case MenuName.Main:
                SceneManager.LoadScene("MainMenu");
                break;
            case MenuName.Gameplay:
                SceneManager.LoadScene("Gameplay");
                break;
            case MenuName.Difficulty:
                SceneManager.LoadScene("DifficultyMenu");
                break;
            case MenuName.Characters:
                SceneManager.LoadScene("CharactersMenu");
                break;
            case MenuName.Help:
                Object.Instantiate(Resources.Load("HelpMenu"));
                break;
            case MenuName.Pause:
                Object.Instantiate(Resources.Load("PauseMenu"));
                break;
            case MenuName.Gameover:
                Object.Instantiate(Resources.Load("GameOverMenu"));
                break;

        }
    }
}