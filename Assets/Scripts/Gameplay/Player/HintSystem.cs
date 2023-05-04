using System;
using System.Collections;
using UnityEngine;
using TMPro;

public class HintSystem : MonoBehaviour
{
    private static event Action<string> OnHintRequested;

    [SerializeField] private TextMeshProUGUI hintMessage;
    [SerializeField] private float fadeDuration = 1f;

    private Coroutine fadeAnimation = null;

    public static void InvokeHint(string message)
    {
        OnHintRequested.Invoke(message);
    }

    void Start()
    {
        OnHintRequested += HandleHintRequested;
    }

    private void HandleHintRequested(string message)
    {
        if (fadeAnimation != null)
            StopCoroutine(fadeAnimation);
        hintMessage.text = message;
        fadeAnimation = StartCoroutine(DoFadeAnimation());

    }

    IEnumerator DoFadeAnimation()
    {
        float time = 0f;
        float alpha = 0f;
        while(time <= fadeDuration)
        {
            if(alpha < 1f && time <= fadeDuration * 0.25f)
            {
                alpha = Math.Clamp(alpha + (1f / (fadeDuration * 0.25f)) * Time.deltaTime, 0f, 1f);
            }
            else if(alpha > 0f && time >= fadeDuration * 0.75f)
            {
                alpha = Math.Clamp(alpha - (1f / (fadeDuration * 0.25f)) * Time.deltaTime, 0f, 1f);
            }
            hintMessage.color = new Color(hintMessage.color.r, hintMessage.color.g, hintMessage.color.b, alpha);
            time += Time.deltaTime;
            yield return null;
        }
        hintMessage.color = new Color(hintMessage.color.r, hintMessage.color.g, hintMessage.color.b, 0f);
        fadeAnimation = null;
    }
}
