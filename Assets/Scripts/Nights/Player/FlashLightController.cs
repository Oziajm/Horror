using UnityEngine;
using UnityEngine.InputSystem;

public class FlashLightController : MonoBehaviour
{
    [SerializeField] private GameObject flashLight;

    private InputActions inputActions;
    private bool isOn = false;

    public bool IsOn => isOn;

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
