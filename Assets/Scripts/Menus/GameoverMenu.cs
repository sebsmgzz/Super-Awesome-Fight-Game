using UnityEngine;
using UnityEngine.UI;

public class GameoverMenu : MonoBehaviour
{

    #region Fields

    private float timeScale;
    private Text winnerText;
    private string winnerTextEnding = "wins!";
    public FighterType winner = FighterType.Player;

    #endregion

    #region Properties

    public FighterType Winner
    {
        get { return winner; }
        set { winner = value; }
    }

    #endregion

    #region Unity API

    private void Start()
    {
        timeScale = Time.timeScale;
        Time.timeScale = 0;
        winnerText = gameObject.transform.Find("Canvas/WinnerText").gameObject.GetComponent<Text>();
    }

    private void Update()
    {
        winnerText.text = winner + " " + winnerTextEnding;
    }

    #endregion

    #region Events Handlers

    public void HandleReturnButtonOnClickEvent()
    {
        MenuManager.GoToMenu(MenuName.Main);
        Time.timeScale = timeScale;
        MenuManager.RemoveTemporalMenu(gameObject);
    }

    public void HandleQuitButtonOnClickEvent()
    {
        Application.Quit();
    }

    #endregion

}
