using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HintSystem : MonoBehaviour
{
    private struct Hint
    {
        string message;
        float duration;
    }

    private static event Action<string> OnHintRequested;

    //[SerializeField] private GameObject hintMessage;
    [SerializeField] private GameObject hintsContainer;
    [SerializeField] private float fadeDuration = 1f;
    [SerializeField] private uint maxHints = 5;

    private Coroutine fadeAnimation = null;
    private List<Hint> hints;

    public static void InvokeHint(string message)
    {
        OnHintRequested.Invoke(message);
    }

    void Start()
    {
        OnHintRequested += HandleHintRequested;
    }

    private void OnDestroy()
    {
        OnHintRequested -= HandleHintRequested;
    }

    private void HandleHintRequested(string message)
    {
        //if (fadeAnimation != null)
        //    StopCoroutine(fadeAnimation);
        //hintMessage.text = message;
        var hint = CreateHint(message, hintsContainer.transform);
        fadeAnimation = StartCoroutine(DoFadeAnimation(hint));
    }

    private TextMeshProUGUI CreateHint(string message, Transform parent)
    {
        var go = new GameObject();
        go.transform.parent = parent;

        var hint = go.AddComponent<TextMeshProUGUI>();
        hint.text = message;
        hint.fontSize = 20;
        hint.faceColor = new Color(1f, 0.21f, 0.78f);
        hint.fontStyle = FontStyles.Bold;
        hint.alignment = TextAlignmentOptions.Center;
        return hint;
    }

    IEnumerator DoFadeAnimation(TextMeshProUGUI hintMessage)
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
