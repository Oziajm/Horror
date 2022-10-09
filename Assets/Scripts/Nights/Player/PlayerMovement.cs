using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private PlayerSettings playerSettings;
    [SerializeField] private CharacterController characterController;

    private StaminaController staminaController;
    private PlayerPoseController playerPoseController;

    protected readonly float groundDistance = 0.4f;
    protected readonly float gravity = -19.62f;

    public bool isMoving { get; private set; }
    public bool isSprinting { get; private set; }
    public bool isCrouching { get { return playerPoseController.isCrouching; }}

    private Vector3 velocity;
    private bool isGrounded;

    private void Start()
    {
        isMoving = false;
        isSprinting = false;
        staminaController = GetComponent<StaminaController>();
        playerPoseController = GetComponent<PlayerPoseController>();
    }

    void FixedUpdate()
    {
        MovementController();

        //StaminaController Methods
        staminaController.UpdateStaminaBarValues();
    }

    private void MovementController()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        isMoving = x != 0 || z != 0;

        float currentSpeed = playerPoseController.isCrouching ? playerSettings.crouchingSpeed : playerSettings.walkingSpeed;
        isSprinting = false;
        if (staminaController.IsAvaiable() && isMoving && Input.GetKey(KeyCode.LeftShift)) // check if sprinting
        {
            playerPoseController.SetCrouch(false);

            staminaController.StopRegenerating();

            isSprinting = true;
            currentSpeed = playerSettings.sprintSpeed;
            staminaController.StartUsing();
        }
        else if (!staminaController.IsFull())  // start regenerating if not full
        {            
            staminaController.StartRegenerating();
        }

        if (!isSprinting)
            staminaController.StopUsing();

        if(Input.GetKeyDown(KeyCode.C) && !Input.GetKey(KeyCode.LeftShift))
        {
            playerPoseController.SetCrouch(!playerPoseController.isCrouching);
        }

        characterController.Move(currentSpeed * Time.deltaTime * move);

        velocity.y += gravity * Time.deltaTime;

        characterController.Move(velocity * Time.deltaTime);
    }
}