using UnityEngine;
using UnityEngine.InputSystem;

public class ItemBoobing : MonoBehaviour
{
    private const float STANDING_SPEED = 1f;
    private const float MAX_ROTATION = 5f;

    [SerializeField]
    private Vector3 offset;
    [SerializeField]
    private Transform origin;

    [SerializeField]
    private float effectIntensity;
    [SerializeField]
    private float effectIntensityX;
    [SerializeField]
    private float effectSpeed;

    private float sinTime;
    private InputActions inputActions;

    private float addingValue = 1f;
    private float currentRotation = 0f;

    private void Start()
    {
        inputActions = new();
        inputActions.Player.Enable();
        inputActions.Player.Sprint.performed += ChangeEffectSpeedToRunning;
        inputActions.Player.Sprint.canceled += ChangeEffectSpeedToWalking;
    }

    private void Update()
    {
        Vector3 inputVector = inputActions.Player.Movement.ReadValue<Vector2>();

        float speed = inputVector.magnitude > 0f ? effectSpeed : STANDING_SPEED;
        sinTime += Time.deltaTime * speed;
        float sinAmountY = Mathf.Abs(effectIntensity * Mathf.Sin(sinTime));

        offset = new Vector3
        {
            x = 0,
            y = sinAmountY,
            z = 0
        };

        if (inputVector.magnitude > 0f)
        {
            Vector3 sinAmountX = transform.right * effectIntensity * Mathf.Cos(sinTime) * effectIntensityX;

            offset += sinAmountX;
        }

        transform.position = Vector3.MoveTowards(transform.position, origin.position + offset, 0.001f);
    }

    private void ChangeEffectSpeedToRunning(InputAction.CallbackContext context)
    {
        effectSpeed *= 2;
    }

    private void ChangeEffectSpeedToWalking(InputAction.CallbackContext context)
    {
        effectSpeed /= 2;
    }
}