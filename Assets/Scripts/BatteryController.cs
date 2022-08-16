using UnityEngine;
using UnityEngine.UI;

public class BatteryController : MonoBehaviour
{
    public float usage = 1;

    [SerializeField] private Image batteryImage;
    [SerializeField] private Image usageImage;

    [SerializeField] private GameObject[] lights;
    [SerializeField] private Renderer[] lightsEmissions;
    [SerializeField] private ButtonInteractions[] buttonInteractions;
    [SerializeField] private ButtonLightsInteractions[] buttonLightsInteractions;
    [SerializeField] private BatteryController batteryController;

    private float batteryAmmount = 1f;

    private void Update()
    {
        batteryImage.fillAmount = batteryAmmount / 1;
        usageImage.fillAmount = usage / 5;

        batteryAmmount -= usage * 0.00002f;

        if(batteryAmmount <= 0.001f)
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
                    buttonInteractions[i].enabled = false;
                    buttonLightsInteractions[i].enabled = false;
                }
            }
            usage = 0f;
            batteryController.enabled = false;
        }

        if(usage <= 2)
        {
            usageImage.color = Color.green;
        }
        else if(usage > 2 && usage <= 4)
        {
            usageImage.color = Color.yellow;
        }
        else if(usage > 4)
        {
            usageImage.color = Color.red;
        }
    }
}
