using UnityEngine;
using UnityEngine.InputSystem;

public class FlashLightController : MonoBehaviour
{
    public GameObject flashLight;

    private InputActions inputActions;
    private bool isOn = false;

    private void Start()
    {
        inputActions = new();
        inputActions.Player.Enable();
        inputActions.Player.Flashlight.performed += TurnOnOffFlashlight;
    }

    private void TurnOnOffFlashlight(InputAction.CallbackContext context)
    {
        isOn = !isOn;
        flashLight.SetActive(isOn);
    }
}
