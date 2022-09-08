using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class StaminaBarController : MonoBehaviour
{
    //[SerializeField] private Slider slider;
    [SerializeField] private Image fill;
    [SerializeField] private Gradient gradientFill;
    [SerializeField] private Image border;
    [SerializeField] private Gradient gradientBorder;

    private float maxValue = 10;
    private float maxFillWidth = 500;
    private float normalizedValue = 1;
    private bool borderLocked = false;
    private Coroutine fadeOutAnimation = null;

    /*public void SetMaxValue(float value)
    {
        //slider.maxValue = value;
        SetValue(value);
    }

    public void SetValue(float value)
    {
        //slider.value = value;
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }
    public void SetStaminaVisible(bool isSprinting)
    {
        //slider.gameObject.SetActive(isSprinting);
    }*/

    private void Start()
    {
        maxFillWidth = border.rectTransform.rect.width - 2;
    }

    public void SetMaxValue(float value)
    {
        maxValue = value;
        SetValue(value);
    }

    public void SetValue(float value)
    {
        normalizedValue = value / maxValue;
        fill.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, normalizedValue * maxFillWidth);

        fill.color = gradientFill.Evaluate(normalizedValue);
        if (borderLocked) return;
        border.color = gradientBorder.Evaluate(normalizedValue);
        if (normalizedValue <= 0f) StartCoroutine(DoBorderAniamtion());
    }

    public void SetStaminaVisible(bool value)
    {
        if(!value)
        {
            if(fadeOutAnimation == null)
            {
                fadeOutAnimation = StartCoroutine(DoFadeOutAnimation());
            }
            return;
        }

        if(fadeOutAnimation != null)
        {
            StopCoroutine(fadeOutAnimation);
            fadeOutAnimation = null;
        }

        border.color = new Color(border.color.r, border.color.g, border.color.b, 1f);
        fill.color = new Color(fill.color.r, fill.color.g, fill.color.b, 1f);
        border.gameObject.SetActive(true);
        fill.gameObject.SetActive(true);
    }

    IEnumerator DoBorderAniamtion()
    {
        float time = 0;
        float frequency = 10;
        borderLocked = true;
        while (normalizedValue < 0.1f)
        {
            var alphaChange = 0.8f * (Mathf.Sin(time * frequency) + 1) / 2;
            border.color = new Color(border.color.r, border.color.g, border.color.b, 1f - alphaChange);
            time += Time.deltaTime;
            yield return null;
        }
        borderLocked = false;
    }

    IEnumerator DoFadeOutAnimation()
    {
        float time = 0;
        float duration = 0.5f;
        while (time <= duration)
        {
            border.color = new Color(border.color.r, border.color.g, border.color.b, Mathf.InverseLerp(duration, 0f, time));
            fill.color = new Color(fill.color.r, fill.color.g, fill.color.b, Mathf.InverseLerp(duration, 0f, time));
            time += Time.deltaTime;
            yield return null;
        }
        border.gameObject.SetActive(false);
        fill.gameObject.SetActive(false);
    }
}
