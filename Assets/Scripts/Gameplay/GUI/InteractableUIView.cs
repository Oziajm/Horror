using Gameplay.Managers;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InteractableUIView : MonoBehaviour
{
    private const float FADE_OUT_ANIM_DURATION = 0.25f;

    [SerializeField]
    private CanvasGroup textWindowFrame;
    [SerializeField]
    private TextMeshProUGUI useText;

    private bool isTextWindowVisible = false;

    private void Start()
    {
        ToggleVisibility(false);
    }

    private void OnEnable()
    {
        EventsManager.Instance.ToggleInteractableViewVisibity += ToggleVisibility;
        EventsManager.Instance.SetInterctableViewText += (string text) => { useText.SetText(text); };
    }

    private void OnDisable()
    {
        EventsManager.Instance.ToggleInteractableViewVisibity -= ToggleVisibility;
        EventsManager.Instance.SetInterctableViewText -= (string text) => { useText.SetText(text); };
    }

    private void ToggleVisibility(bool isTextWindowVisible)
    {
        if (this.isTextWindowVisible == isTextWindowVisible) { return; }

        this.isTextWindowVisible = isTextWindowVisible;
        StopCoroutine(DoFadeInOutAnimation());
        StartCoroutine(DoFadeInOutAnimation());
    }

    private IEnumerator DoFadeInOutAnimation()
    {
        float time = 0;
        while (time < FADE_OUT_ANIM_DURATION)
        {
            textWindowFrame.alpha = (isTextWindowVisible) ? Mathf.InverseLerp(0, FADE_OUT_ANIM_DURATION, time) : Mathf.InverseLerp(FADE_OUT_ANIM_DURATION, 0, time);
            time += Time.deltaTime;
            yield return null;
        }
        textWindowFrame.alpha = (isTextWindowVisible) ? 1f : 0f;
    }
}
