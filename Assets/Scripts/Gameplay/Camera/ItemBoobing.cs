using UnityEngine;
using UnityEngine.InputSystem;

public class ItemBoobing : MonoBehaviour
{
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

        if (inputVector.magnitude > 0f)
        {
            sinTime += Time.deltaTime * effectSpeed;

            float sinAmountY = -Mathf.Abs(effectIntensity * Mathf.Sin(sinTime));
            Vector3 sinAmountX = transform.right * effectIntensity * Mathf.Cos(sinTime) * effectIntensityX;

            offset = new Vector3
            {
                x = 0,
                y = sinAmountY,
                z = 0
            };

            offset += sinAmountX;

            transform.position = Vector3.MoveTowards(transform.position, origin.position + offset, 0.002f);
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, origin.position, 0.001f);
        }
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