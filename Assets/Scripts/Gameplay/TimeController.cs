using UnityEngine;
using TMPro;
using System;
using Gameplay.Managers;

public class TimeController : MonoBehaviour
{
    [SerializeField]
    private TextMeshPro countdownText;

    private float currentTime;

    private void Awake()
    {
        currentTime = 0;
    }

    private void Update()
    {
        ShowTime();
    }

    private void ShowTime()
    {
        if (Mathf.RoundToInt(currentTime) == 5)
        {
            EventsManager.Instance.AnimatronicsActivated?.Invoke();
        }

        if (currentTime < 360)
        {
            currentTime += 1 * Time.deltaTime;
        }

        var timeSpan = TimeSpan.FromSeconds(currentTime);
        countdownText.SetText(timeSpan.ToString("mm':'ss"));
    }
}
