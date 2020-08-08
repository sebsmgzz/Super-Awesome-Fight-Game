using UnityEngine;

public class HelpMenu : MonoBehaviour
{
    
    public void HandleReturnButtonOnClickEvent()
    {
        MenuManager.RemoveTemporalMenu(gameObject);
    }

}
