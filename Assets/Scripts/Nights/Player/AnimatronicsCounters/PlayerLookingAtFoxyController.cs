using UnityEngine;
using UnityEngine.AI;

public class PlayerLookingAtFoxyController : PlayerLookingAtAnimatronics
{
    [SerializeField] private GameObject foxy;
    [SerializeField] private FlashLightController flashLightController;
    [SerializeField] private Material eyes;
    [SerializeField] private Light[] eyesLights;

    private Camera cam;

    private void Start()
    {
        cam = GetComponent<Camera>();
        ChangeFoxyEyesColor(Color.white);
    }

    private void Update()
    {
        if (IsVisible(cam, foxy) && flashLightController.IsOn && !foxy.GetComponent<Foxy>().isImmuneToFlashlight)
        {
            Debug.Log("flash");
            foxy.GetComponent<Animator>().SetBool("isFlashedOut", true);
            foxy.GetComponent<NavMeshAgent>().speed = 0f;
            foxy.GetComponent<StateMachine>().enabled = false;
            foxy.GetComponent<Foxy>().isImmuneToFlashlight = true;
            ChangeFoxyEyesColor(Color.red);
        }

        if (IsVisible(cam, foxy) && flashLightController.IsOn && !foxy.GetComponent<Foxy>().isImmuneToFlashlight)
        {
            foxy.GetComponent<Foxy>().isTriggered = false;
            foxy.GetComponent<Animator>().SetBool("isFoxyTriggered", true);
        } else
        {
            foxy.GetComponent<Animator>().SetBool("isFoxyTriggered", false);
        }

        if (!foxy.GetComponent<Foxy>().isTriggered && !IsVisible(cam, foxy))
        {
            float time = 5f;
            time -= 1f * Time.deltaTime;
            if (time < 0)
            {
                ChangeFoxyEyesColor(Color.white);
                foxy.GetComponent<Animator>().SetBool("isFlashedOut", false);
            }
        }
    }

    private void ChangeFoxyEyesColor(Color color)
    {
        foreach (var eyeLight in eyesLights)
        {
            eyeLight.color = color;
        }
        eyes.color = color;
        eyes.SetColor("_EmissionColor", color * 1f);
    }
}
