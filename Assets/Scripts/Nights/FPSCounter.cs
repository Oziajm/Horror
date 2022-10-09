using UnityEngine;
using TMPro;

public class FPSCounter : MonoBehaviour
{
    private TextMeshProUGUI fpsText;

    private float timer, avgFramerate;

    private void Start()
    {
        fpsText = GetComponent<TextMeshProUGUI>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        ShowFPS();
    }

    private void ShowFPS()
    {
        float timelapse = Time.smoothDeltaTime;
        timer -= timelapse;

        if (timer <= 0) avgFramerate = (int)(1f / timelapse);
        fpsText.text = avgFramerate.ToString() + " FPS";
    }
}
