using UnityEngine;

public class PauseMenu : MonoBehaviour
{

    #region Fields

    private float timeScale;

    #endregion

    #region Unity API

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

    #endregion

    #region Events Handlers

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

    #endregion

}
