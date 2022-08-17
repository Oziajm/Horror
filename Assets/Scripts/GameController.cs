using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public float usage = 1;

    [SerializeField] private Image batteryImage;
    [SerializeField] private Image usageImage;
    [SerializeField] private GameObject[] lights;
    [SerializeField] private Renderer[] lightsEmissions;
    [SerializeField] private ButtonInteractions[] buttonInteractions;
    [SerializeField] private ButtonLightsInteractions[] buttonLightsInteractions;
    [SerializeField] private TextMeshPro countdownText;
    [SerializeField] private TextMeshProUGUI fpsCounter;

    private float timer, avgFramerate;
    private float currentTime = 0f;
    private float batteryAmmount = 1f;

    private readonly float refresh;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        FPSCounter();
        ShowTime();
        BatteryController();
    }

    private void FPSCounter()
    {
        float timelapse = Time.smoothDeltaTime;
        timer = timer <= 0 ? refresh : timer -= timelapse;

        if (timer <= 0) avgFramerate = (int)(1f / timelapse);
        fpsCounter.text = avgFramerate.ToString() + " FPS";
    }

    private void ShowTime()
    {
        if(currentTime < 360)
        {
            currentTime += 1 * Time.deltaTime;
        }
        var timeSpan = TimeSpan.FromSeconds(currentTime);
        countdownText.text = Mathf.RoundToInt(currentTime) % 2 == 0 ? timeSpan.ToString("mm':'ss") : timeSpan.ToString("mm' 'ss");
    }

    private void BatteryController()
    {
        batteryImage.fillAmount = batteryAmmount / 1;
        usageImage.fillAmount = usage / 5;

        batteryAmmount -= usage * 0.00004f;

        if (batteryAmmount <= 0.001f)
        {
            for (int i = 0; i < 3; i++)
            {
                lights[i].SetActive(false);
                lightsEmissions[i].material.SetColor("_EmissionColor", lightsEmissions[i].material.color * 0f);
            }
            for (int i = 0; i < 2; i++)
            {
                buttonLightsInteractions[i].isPressed = false;
                buttonInteractions[i].isPressed = false;
                if (buttonInteractions[i].isOpen)
                {
                    buttonInteractions[i].ChangeButtonsColor();
                    buttonInteractions[i].enabled = false;
                    buttonLightsInteractions[i].enabled = false;
                }
            }
            usage = 0f;
        }

        if (usage <= 2)
        {
            usageImage.color = Color.green;
        }
        else if (usage > 2 && usage <= 4)
        {
            usageImage.color = Color.yellow;
        }
        else if (usage > 4)
        {
            usageImage.color = Color.red;
        }
    }
}
