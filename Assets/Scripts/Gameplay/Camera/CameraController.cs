using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    #region Variables

    [SerializeField] 
    private Transform playerTransform;
    [SerializeField] 
    private CameraSettings cameraSettings;
    [SerializeField]
    private Camera cam;

    private InputActions inputActions;
    private Vector2 inputVector;

    private float xRotation;

    private float mouseX;
    private float mouseY;

    private Transform caughtBy;

    private bool previouslyAdded;
    private float currentFOV;

    #endregion

    #region Unity Methods

    private void Start()
    {
        currentFOV = cam.fieldOfView;

        inputActions = new();
        inputActions.Player.Enable();
    }

    private void FixedUpdate()
    {
        if (caughtBy == null)
        {
            RotateCamera();
        }
        else
        {
            JumpscareCameraAnimation();
        }
    }

    #endregion

    #region Private Methods

    public void OnJumpscareTriggered(Transform caughtBy)
    {
        this.caughtBy = caughtBy;

        playerTransform.position += Vector3.up * 0.6f;

        transform.LookAt(caughtBy.position);
        playerTransform.LookAt(caughtBy.position);
    }

    #endregion

    #region Private Methods

    private void JumpscareCameraAnimation()
    {
        currentFOV += previouslyAdded ? -5 : 5;
        previouslyAdded = !previouslyAdded;

        cam.fieldOfView = currentFOV;

        Quaternion newRotation = transform.rotation;
        newRotation.z = caughtBy.rotation.z;
        newRotation.x = caughtBy.rotation.x;
        transform.rotation = newRotation;
    }

    private void RotateCamera()
    {
        inputVector = inputActions.Player.CameraInput.ReadValue<Vector2>();

        mouseX = inputVector.x * cameraSettings.mouseSensitivity * Time.deltaTime;
        mouseY = inputVector.y * cameraSettings.mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90, 80f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerTransform.Rotate(Vector3.up * mouseX);
    }

    #endregion
}
