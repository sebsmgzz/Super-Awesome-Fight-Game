using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class MenuManager
{

    private static Dictionary<MenuName, Object> temporalMenus =
        new Dictionary<MenuName, Object>()
        {
            { MenuName.Gameover, null},
            { MenuName.Help, null},
            { MenuName.Pause, null}
        };

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
                if(temporalMenus[MenuName.Help] == null)
                {
                    temporalMenus[MenuName.Help] = 
                        GameObject.Instantiate(Resources.Load("HelpMenu"));
                }
                break;
            case MenuName.Pause:
                if (temporalMenus[MenuName.Pause] == null)
                {
                    temporalMenus[MenuName.Pause] = 
                        GameObject.Instantiate(Resources.Load("PauseMenu"));
                }
                break;
            case MenuName.Gameover:
                if(temporalMenus[MenuName.Gameover] == null)
                {
                    temporalMenus[MenuName.Gameover] =
                        GameObject.Instantiate(Resources.Load("GameOverMenu")); ;
                }
                break;
        }
    }

    public static void RemoveTemporalMenu(GameObject gameObject)
    {
        foreach(KeyValuePair<MenuName, Object> pair in temporalMenus)
        {
            if(pair.Value == gameObject)
            {
                GameObject.Destroy(gameObject);
                temporalMenus[pair.Key] = null;
                break;
            }
        }
    }

}