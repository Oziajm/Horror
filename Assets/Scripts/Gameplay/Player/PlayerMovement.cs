using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    #region Variables

    private const float GROUND_DISTANCE = 0.4f;
    private const float GRAVITY = -19.62f;

    public bool IsMoving => inputVector.x != 0 || inputVector.y != 0;
    public bool IsSprinting { get; private set; }
    public bool IsCrouching { get { return playerPoseController.IsCrouching; } }

    [SerializeField] 
    private Transform groundCheck;
    [SerializeField] 
    private LayerMask groundMask;
    [SerializeField] 
    private PlayerSettings playerSettings;
    [SerializeField] 
    private CharacterController characterController;

    [SerializeField]
    private PlayerPoseController playerPoseController;
    [SerializeField]
    private StaminaController staminaController;

    private InputActions inputActions;

    private float currentSpeed;

    private Vector3 velocity;
    private Vector2 inputVector;

    private bool isGrounded;

    #endregion

    #region Unity Methods

    private void Start()
    {
        currentSpeed = playerSettings.walkSpeed;

        inputActions = new();
        inputActions.Player.Enable();
        inputActions.Player.Sprint.performed += ChangeCurrentSpeedToRunningSpeed;
        inputActions.Player.Sprint.canceled += ChangeCurrentSpeedToWalkingSpeed;
        inputActions.Player.Crouch.performed += ChangeCurrentSpeedToCrouchingSpeed;
        inputActions.Player.Crouch.canceled += ChangeCurrentSpeedToWalkingSpeed;
    }

    private void FixedUpdate()
    {
        DoGravity();
        MoveCharacter();
        HandleStamina();
    }

    #endregion

    #region Private Methods

    private void ChangeCurrentSpeedToRunningSpeed(InputAction.CallbackContext context)
    {
        playerPoseController.SetCrouch(false);
        currentSpeed = playerSettings.sprintSpeed;
    }

    private void ChangeCurrentSpeedToCrouchingSpeed(InputAction.CallbackContext context)
    {
        currentSpeed = playerSettings.crouchSpeed;
    }

    private void ChangeCurrentSpeedToWalkingSpeed(InputAction.CallbackContext context)
    {
        currentSpeed = playerSettings.walkSpeed;
    }

    private void MoveCharacter()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, GROUND_DISTANCE, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        inputVector = inputActions.Player.Movement.ReadValue<Vector2>();

        Vector3 move = transform.forward * inputVector.y + transform.right * inputVector.x;

        characterController.Move(currentSpeed * Time.deltaTime * move);
    }

    private void HandleStamina()
    {
        IsSprinting = staminaController.IsAvaiable() && IsMoving && currentSpeed == playerSettings.sprintSpeed;

        if (IsSprinting)
        {
            staminaController.StartUsing();
            staminaController.StopRegenerating();
        }
        else
        {
            if (staminaController.IsFull())
            {
                return;
            }

            staminaController.StopUsing();
            staminaController.StartRegenerating();
        }
    }

    private void DoGravity()
    {
        velocity.y += GRAVITY * Time.deltaTime;

        characterController.Move(velocity * Time.deltaTime);
    }

    #endregion
}