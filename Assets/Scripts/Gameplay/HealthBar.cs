using UnityEngine.UI;
using UnityEngine;

public class HealthBar : MonoBehaviour
{

    #region Fields

    // components
    private Slider slider;
    private Text text;

    #endregion

    #region Properties

    /// <summary>
    /// The value of the health bar
    /// </summary>
    public float Value
    {
        get { return slider.value; }
    }

    /// <summary>
    /// The text of the health bar
    /// </summary>
    public string Text
    {
        get { return text.text; }
        set { text.text = value; }
    }

    #endregion

    #region Unity API

    private void Start()
    {
        // components
        slider = gameObject.GetComponent<Slider>();
        text = gameObject.GetComponentInChildren<Text>();
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
    public void SetPosition(Fighter.Name fighterName)
    {
        switch(fighterName)
        {
            case Fighter.Name.Enemy:
                gameObject.transform.position =
                    new Vector2(
                        ScreenUtilities.Left,
                        ScreenUtilities.Top);
                break;
            case Fighter.Name.Player:
                gameObject.transform.position =
                    new Vector2(
                        ScreenUtilities.Right,
                        ScreenUtilities.Top);
                break;
        }

    }

    #endregion

}
