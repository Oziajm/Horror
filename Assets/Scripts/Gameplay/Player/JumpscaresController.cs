using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpscaresController : MonoBehaviour
{
    [SerializeField]
    private MovementController movementController;
    [SerializeField]
    private CameraController cameraController;
    [SerializeField]
    private CameraShaker cameraShaker;
    [SerializeField]
    private StaminaBarController staminaBarController;
    [SerializeField]
    private StaminaController staminaController;
    [SerializeField]
    private GameObject gameplayView;

    [SerializeField]
    private GameObject flashlight;

    public void OnJumpscareTriggered(Transform caughtBy)
    {
        movementController.enabled = false;
        cameraShaker.enabled = false;
        staminaBarController.enabled = false;
        staminaController.enabled = false;

        gameplayView.SetActive(false);
        flashlight.SetActive(false);

        cameraController.OnJumpscareTriggered(caughtBy);
    }
}
