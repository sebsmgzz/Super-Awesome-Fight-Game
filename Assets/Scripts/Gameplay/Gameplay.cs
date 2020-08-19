using UnityEngine;

public class Gameplay : MonoBehaviour
{

    #region Unity API

    private void Awake()
    {
        EventsManager.AddHealthEmptiedListener(HandleHealthEmptiedEvent);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            MenuManager.GoToMenu(MenuName.Pause);
        }
    }

    #endregion

    #region Events Handlers

    private void HandleHealthEmptiedEvent(FighterType fighterName)
    {
        FighterType winner = (fighterName == FighterType.Enemy) ? FighterType.Player : FighterType.Enemy;
        MenuManager.GoToMenu(MenuName.Gameover);
        GameObject GOM = GameObject.FindGameObjectWithTag("GameoverMenu");
        GOM.GetComponent<GameoverMenu>().Winner = winner;
    }

    #endregion

}
