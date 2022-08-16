using UnityEngine;
using TMPro;
using System;

public class InGameTime : MonoBehaviour
{
    [SerializeField] TextMeshPro countdownText;

    float currentTime = 0f;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        do
        {
            currentTime += 1 * Time.deltaTime;
        }
        while (currentTime == 360f);
        var timeSpan = TimeSpan.FromSeconds(currentTime);
        countdownText.text = Mathf.RoundToInt(currentTime) % 2 == 0 ? timeSpan.ToString("mm':'ss") : timeSpan.ToString("mm' 'ss");
    }
}
