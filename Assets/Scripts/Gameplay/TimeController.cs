using UnityEngine;
using TMPro;
using System;

public class TimeController : MonoBehaviour
{
    private TextMeshPro countdownText;

    private float currentTime = 0;

    private void Start()
    {
        countdownText = GetComponent<TextMeshPro>();
    }

    private void Update()
    {
        ShowTime();
    }

    private void ShowTime()
    {
        if (Mathf.RoundToInt(currentTime) == 5)
            EventManager.current.AnimatronicTurned();
        if (currentTime < 360)
        {
            currentTime += 1 * Time.deltaTime;
        }
        var timeSpan = TimeSpan.FromSeconds(currentTime);
        countdownText.text = Mathf.RoundToInt(currentTime) % 2 == 0 ? timeSpan.ToString("mm':'ss") : timeSpan.ToString("mm' 'ss");
    }
}
