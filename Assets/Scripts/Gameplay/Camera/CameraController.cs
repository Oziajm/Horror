using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    #region Variables

    [SerializeField] private Transform playerTransform;
    [SerializeField] private CameraSettings cameraSettings;

    private InputActions inputActions;

    private Vector2 inputVector;

    private float xRotation = 0f;

    #endregion

    #region Unity Methods

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        inputActions = new();
        inputActions.Player.Enable();
    }

    private void FixedUpdate()
    {
        RotateCamera();
    }

    #endregion

    #region Private Methods

    private void RotateCamera()
    {
        inputVector = inputActions.Player.CameraInput.ReadValue<Vector2>();

        float mouseX = inputVector.x * cameraSettings.mouseSensitivity * Time.deltaTime;
        float mouseY = inputVector.y * cameraSettings.mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerTransform.Rotate(Vector3.up * mouseX);
    }

    #endregion
}
