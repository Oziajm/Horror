using UnityEngine;
using UnityEngine.UI;

public class StaminaBarController : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private Gradient gradient;
    [SerializeField] private Image fill;

    public void SetMaxValue(float value)
    {
        slider.maxValue = value;
        SetValue(value);
    }

    public void SetValue(float value)
    {
        slider.value = value;
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }
    public void SetStaminaVisible(bool isSprinting)
    {
        slider.gameObject.SetActive(isSprinting);
    }
}
