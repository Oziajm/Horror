using UnityEngine;
using TMPro;
using System;

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
            EventManager.current.AnimatronicTurned();
        }

        if (currentTime < 360)
        {
            currentTime += 1 * Time.deltaTime;
        }

        var timeSpan = TimeSpan.FromSeconds(currentTime);
        countdownText.SetText(timeSpan.ToString("mm':'ss"));
    }
}
