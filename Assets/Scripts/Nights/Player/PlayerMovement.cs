using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private PlayerSettings playerSettings;
    [SerializeField] private CharacterController characterController;

    public float currentSpeed;

    private PlayerPoseController playerPoseController;
    private StaminaController staminaController;

    protected readonly float groundDistance = 0.4f;
    protected readonly float gravity = -19.62f;

    private InputActions inputActions;

    public bool IsMoving { get; private set; }
    public bool IsSprinting { get; private set; }
    public bool IsCrouching { get { return playerPoseController.IsCrouching; }}

    private Vector3 velocity;
    private Vector2 inputVector;
    private bool isGrounded;

    private void Start()
    {
        staminaController = GetComponent<StaminaController>();
        playerPoseController = GetComponent<PlayerPoseController>();

        currentSpeed = playerSettings.walkingSpeed;

        inputActions = new();
        inputActions.Player.Enable();
        inputActions.Player.Sprint.performed += ChangeCurrentSpeedToRunningSpeed;
        inputActions.Player.Sprint.canceled += ChangeCurrentSpeedToWalkingSpeed;
    }

    private void FixedUpdate()
    {
        DoGravity();
        MoveCharacter();
        HandleStamina();
    }

    private void ChangeCurrentSpeedToRunningSpeed(InputAction.CallbackContext context)
    {
        currentSpeed = playerSettings.sprintSpeed;
        IsSprinting = true;
    }

    private void ChangeCurrentSpeedToWalkingSpeed(InputAction.CallbackContext context)
    {
        currentSpeed = playerSettings.walkingSpeed;
        IsSprinting = false;
    }

    private void MoveCharacter()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        inputVector = inputActions.Player.Movement.ReadValue<Vector2>();

        IsMoving = inputVector.x > 0 || inputVector.y > 0;

        characterController.Move(currentSpeed * Time.deltaTime * new Vector3(inputVector.x, 0f, inputVector.y));
    }

    private void HandleStamina()
    {
        if (staminaController.IsAvaiable() && IsMoving && IsSprinting) // check if sprinting
        {
            staminaController.StopRegenerating();

            currentSpeed = playerSettings.sprintSpeed;
            staminaController.StartUsing();
        }
        else if (!staminaController.IsFull())  // start regenerating if not full
        {
            staminaController.StartRegenerating();
        }

        if (!IsSprinting)
            staminaController.StopUsing();
    }

    private void DoGravity()
    {
        velocity.y += gravity * Time.deltaTime;

        characterController.Move(velocity * Time.deltaTime);
    }
}