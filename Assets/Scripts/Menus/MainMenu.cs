using UnityEngine;

public class MainMenu : MonoBehaviour
{

    private void Start()
    {
        
    }

    public void HandlePlayButtonOnClickEvent()
    {
        MenuManager.GoToMenu(MenuName.Difficulty);
    }

    public void HandleHelpButtonOnClickEvent()
    {
        MenuManager.GoToMenu(MenuName.Help);
    }

    public void HandleQuitButtonOnClickEvent()
    {
        Application.Quit();
    }

}
