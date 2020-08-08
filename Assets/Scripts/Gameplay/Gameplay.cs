using UnityEngine;

public class Gameplay : MonoBehaviour
{

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

    private void HandleHealthEmptiedEvent(Fighter.Name fighterName)
    {
        Fighter.Name winner = (fighterName == Fighter.Name.Enemy) ? 
            Fighter.Name.Player : Fighter.Name.Enemy;
        MenuManager.GoToMenu(MenuName.Gameover);
        GameObject GOM = GameObject.FindGameObjectWithTag("GameoverMenu");
        GOM.GetComponent<GameoverMenu>().winner = winner;
    }

}
