using UnityEngine;

public class PauseMenu : MonoBehaviour
{

    private float timeScale;

    private void Start()
    {
        timeScale = Time.timeScale;
        Time.timeScale = 0;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            HandleReturnButtonOnClickEvent();
        }
    }

    public void HandleHelpButtonOnClickEvent()
    {
        MenuManager.GoToMenu(MenuName.Help);
    }

    public void HandleReturnButtonOnClickEvent()
    {
        Time.timeScale = timeScale;
        MenuManager.RemoveTemporalMenu(gameObject);
    }

    public void HandleQuitButtonOnClickEvent()
    {
        Time.timeScale = timeScale;
        Application.Quit();
    }

}
