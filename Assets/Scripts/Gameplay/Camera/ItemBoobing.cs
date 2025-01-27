using UnityEngine;
using UnityEngine.InputSystem;

public class ItemBoobing : MonoBehaviour
{
    private readonly float MAX_ROTATE_ANGLE = 30f;
    private readonly float TILT_AMOUNT = 10f;
    private readonly float TILT_SPEED = 10f;

    private readonly float BREATHING_AMOUNT = 0.02f;
    private readonly float BREATHING_SPEED = 1.5f;

    private InputActions inputActions;

    private Vector3 breathingOffset;
    private Quaternion originalRotation;

    private void Start()
    {
        inputActions = new();
        inputActions.Player.Enable();

        originalRotation = transform.localRotation;
    }

    private void Update()
    {
        HandleMovement();
        HandleBreathing();
    }

    private void HandleMovement()
    {
        float horizontalMoveInput = inputActions.Player.Movement.ReadValue<Vector2>().x;
        float verticalMoveInput = inputActions.Player.Movement.ReadValue<Vector2>().y;

        float horizontalLookInput = inputActions.Player.CameraInput.ReadValue<Vector2>().x;
        float verticalLookInput = inputActions.Player.CameraInput.ReadValue<Vector2>().y;

        float targetHorizontalTilt = (horizontalLookInput + horizontalMoveInput) * TILT_AMOUNT;
        float targetVerticalTilt = (verticalLookInput + verticalMoveInput) * TILT_AMOUNT;

        targetHorizontalTilt = Mathf.Clamp(targetHorizontalTilt, -MAX_ROTATE_ANGLE, MAX_ROTATE_ANGLE);
        targetVerticalTilt = Mathf.Clamp(targetVerticalTilt, -MAX_ROTATE_ANGLE, MAX_ROTATE_ANGLE);

        Quaternion targetRotation = originalRotation * Quaternion.Euler(targetVerticalTilt, 0, targetHorizontalTilt);

        transform.localRotation = Quaternion.Lerp(transform.localRotation, targetRotation, Time.deltaTime * TILT_SPEED) * Quaternion.Euler(breathingOffset);
    }

    private void HandleBreathing()
    {
        float breathingOffsetY = Mathf.Sin(Time.time * BREATHING_SPEED) * BREATHING_AMOUNT;
        float breathingOffsetX = Mathf.Cos(Time.time * BREATHING_SPEED * 0.5f) * BREATHING_AMOUNT * 0.5f;

        breathingOffset = new Vector3(breathingOffsetX, breathingOffsetY, 0);

        transform.localPosition += breathingOffset * Time.deltaTime;
    }
}