using System.Collections;
using UnityEngine;

public class LightButton : Button
{
    public bool isPressed = false;

    [SerializeField] private GameObject spotlight;
    [SerializeField] private Renderer button;
    [SerializeField] private GameController batteryController;

    // Start is called before the first frame update
    void Start()
    {
        //spotlightBulb.material.SetColor("_EmissionColor", spotlightBulb.material.color * 0f);
        button.material.SetColor("_EmissionColor", button.material.color * 0f);
    }

    public override void OnClick()
    {
        isPressed = !isPressed;
        if (!isPressed)
        {
            //spotlight.SetActive(false);
            batteryController.usage -= 1;
            button.material.SetColor("_EmissionColor", button.material.color * 0f);
            //spotlightBulb.material.SetColor("_EmissionColor", spotlightBulb.material.color * 0f);
            StartCoroutine(CountDown());
        }
        if (isPressed)
        {
            //spotlight.SetActive(true);
            batteryController.usage += 1;
            button.material.SetColor("_EmissionColor", Color.white * 1f);
            //spotlightBulb.material.SetColor("_EmissionColor", Color.white * 1f);
            StartCoroutine(CountDown());
        }
    }

    IEnumerator CountDown()
    {
        while (true)
        {
            yield return new WaitForSeconds(2f);
        }
    }
}
