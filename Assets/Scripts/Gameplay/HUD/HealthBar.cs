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
    /// <param name="damageAmount">The amount to reduce</param>
    public void TakeDamage(float damageAmount)
    {
        slider.value -= damageAmount;
    }

    #endregion

}
