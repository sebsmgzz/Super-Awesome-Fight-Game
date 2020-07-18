using UnityEngine.UI;
using UnityEngine;

public class HealthBar : MonoBehaviour
{

    #region Fields

    // components
    private Slider slider;

    #endregion

    #region Properties

    /// <summary>
    /// The value of the health bar
    /// </summary>
    public float Value
    {
        get { return slider.value; }
    }

    #endregion

    #region Unity API

    void Start()
    {
        // components
        slider = gameObject.GetComponent<Slider>();
        // startup
        slider.value = slider.maxValue;
    }

    #endregion

    #region Methods

    /// <summary>
    /// Reduces the value of the health bar
    /// </summary>
    public void TakeDamage()
    {
        slider.value -= 1;
    }

    /// <summary>
    /// Sets the location of the healthbar
    /// </summary>
    /// <param name="position"></param>
    public void SetPosition(Direction position)
    {
        float positionX = 0;
        switch(position)
        {
            case Direction.Left:
                positionX = Utilities.Screen.Left;
                break;
            case Direction.Right:
                positionX = Utilities.Screen.Right;
                break;
        }
        float positionY = Utilities.Screen.Top;
        gameObject.transform.position = 
            new Vector2(positionX, positionY);
    }

    #endregion

}
