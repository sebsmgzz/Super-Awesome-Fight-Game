using UnityEngine;
using UnityEngine.UI;

public class GameoverMenu : MonoBehaviour
{

    private Text winnerText;
    private string winnerTextEnding = "wins!";
    public Fighter.Name winner = Fighter.Name.Player;

    private void Start()
    {
        winnerText = gameObject.transform.Find("Canvas/WinnerText").gameObject.GetComponent<Text>();
    }

    private void Update()
    {
        winnerText.text = winner + " " + winnerTextEnding;
    }

    public void HandleReturnButtonOnClickEvent()
    {
        MenuManager.GoToMenu(MenuName.Main);
        MenuManager.RemoveTemporalMenu(gameObject);
    }

    public void HandleQuitButtonOnClickEvent()
    {
        Application.Quit();
    }

}
