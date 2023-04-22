using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    #region Variables

    public bool IsMoving { get; private set; }
    public bool IsSprinting { get; private set; }
    public bool IsCrouching { get { return playerPoseController.IsCrouching; } }

    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private PlayerSettings playerSettings;
    [SerializeField] private CharacterController characterController;

    private Transform player;

    private float currentSpeed;

    private PlayerPoseController playerPoseController;
    private StaminaController staminaController;

    private readonly float groundDistance = 0.4f;
    private readonly float gravity = -19.62f;

    private InputActions inputActions;

    private Vector3 velocity;
    private Vector2 inputVector;
    private bool isGrounded;
    private bool isSprintingRequested = false;

    #endregion

    #region Unity Methods

    private void Start()
    {
        player = GetComponent<Transform>();
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

        currentSpeed = IsCrouching ? playerSettings.crouchingSpeed : IsSprinting ? playerSettings.sprintSpeed : playerSettings.walkingSpeed;
    }

    #endregion

    #region Private Methods

    private void ChangeCurrentSpeedToRunningSpeed(InputAction.CallbackContext context)
    {
        isSprintingRequested = true;
    }

    private void ChangeCurrentSpeedToWalkingSpeed(InputAction.CallbackContext context)
    {
        isSprintingRequested = false;
    }

    private void MoveCharacter()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        inputVector = inputActions.Player.Movement.ReadValue<Vector2>();

        IsMoving = inputVector.x != 0 || inputVector.y != 0;

        Vector3 move = player.forward * inputVector.y + player.right * inputVector.x;

        characterController.Move(currentSpeed * Time.deltaTime * move);
    }

    private void HandleStamina()
    {
        IsSprinting = false;
        if (staminaController.IsAvaiable() && IsMoving && isSprintingRequested) // check if sprinting
        {
            playerPoseController.SetCrouch(false);
            staminaController.StopRegenerating();

            currentSpeed = playerSettings.sprintSpeed;
            staminaController.StartUsing();
            IsSprinting = true;
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

    #endregion
}