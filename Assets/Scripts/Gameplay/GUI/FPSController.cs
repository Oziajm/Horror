using UnityEngine;
using TMPro;

public class FPSController : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI fpsText;

    private float timer;
    private float avgFramerate;
    private int framesElapsed;

    private void Update()
    {
        if(framesElapsed == 5)
        {
            ShowFPS();
            framesElapsed = 0;
        }

        framesElapsed++;
    }

    private void ShowFPS()
    {
        float timelapse = Time.smoothDeltaTime;
        timer -= timelapse;

        if (timer <= 0) avgFramerate = (int)(1f / timelapse);
        fpsText.text = avgFramerate.ToString() + " FPS";
    }
}
