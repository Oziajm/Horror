using UnityEngine;
using UnityEngine.InputSystem;

public class FlashLightController : MonoBehaviour
{
    [SerializeField] private GameObject flashLight;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip audioClip;

    private InputActions inputActions;
    private bool isOn;

    public bool IsOn => isOn;

    private void Start()
    {
        inputActions = new();
        inputActions.Player.Enable();
        inputActions.Player.Flashlight.performed += ChangeFlashlightLightning;
    }

    private void ChangeFlashlightLightning(InputAction.CallbackContext context)
    {
        isOn = !isOn;
        flashLight.SetActive(isOn);
        audioSource.PlayOneShot(audioClip);
    }
}
