using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;

public class PlayerPoseController : MonoBehaviour
{
    #region Variables

    [SerializeField] protected CharacterController characterController;
    [SerializeField] private PlayerSettings playerSettings;

    private Coroutine crouchingCoroutine = null;

    public bool IsCrouching { get; private set; }

    #endregion

    #region Unity Methods

    private void Start()
    {
        InputActions inputActions = new();
        inputActions.Player.Enable();
        inputActions.Player.Crouch.performed += SetCrouch;

        IsCrouching = false;
    }

    #endregion

    #region Public Methods

    public void SetCrouch(InputAction.CallbackContext context)
    {
        IsCrouching = !IsCrouching;

        if (crouchingCoroutine == null)
        {
            crouchingCoroutine = StartCoroutine(DoCrouchStand());
        }
    }

    public IEnumerator DoCrouchStand()
    {
        float time = 0f;
        float startHeight = characterController.height;
        Vector3 startCenter = Vector3.zero;
        float targetHeight = IsCrouching ? playerSettings.standHeight - 1f : playerSettings.standHeight;
        Vector3 targetCenter = IsCrouching ? new(0, 0.2f, 0) : startCenter;

        while (time <= playerSettings.timeToCrouch)
        {
            characterController.height = Mathf.Lerp(startHeight, targetHeight, time / playerSettings.timeToCrouch);
            characterController.center = Vector3.Lerp(startCenter, targetCenter, time / playerSettings.timeToCrouch);
            time += 1 * Time.deltaTime;
            yield return null;
        }

        characterController.height = targetHeight;
        characterController.center = targetCenter;

        crouchingCoroutine = null;
        StopCoroutine(DoCrouchStand());
    }

    #endregion
}