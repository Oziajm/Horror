using UnityEngine;

public class PlayerMovement : PlayerPoseController
{
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundMask;

    protected readonly float groundDistance = 0.4f;
    protected readonly float gravity = -19.62f;

    private Vector3 velocity;
    private bool isGrounded;

    void FixedUpdate()
    {
        MovementController();

        //StaminaController Methods
        UpdateStaminaBarValues();
        StartStopCrouching();
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

        float currentSpeed = isCrouching ? crouchingSpeed : walkingSpeed;

        if (stamina > 0 && isMoving && Input.GetKey(KeyCode.LeftShift))
        {
            if (duringCrouchAnimation) 
                StopCoroutine(DoCrouchStand());

            if(isCrouching)
            {
                isCrouching = false;
                StartCoroutine(DoCrouchStand());
            }

            if (staminaRegeneration != null)
            {
                StopCoroutine(staminaRegeneration);
                staminaRegeneration = null;
            }

            isSprinting = true;
            currentSpeed = sprintSpeed;
            stamina = Mathf.Clamp(stamina - Time.deltaTime*staminaUsageSpeed, 0, maxStamina);
        }
        else if (stamina < maxStamina)
        {
            isSprinting = false;
            if (staminaRegeneration == null)
            {
                staminaRegeneration = StartCoroutine(RegenerateStamina());
            }
        }

        characterController.Move(currentSpeed * Time.deltaTime * move);

        velocity.y += gravity * Time.deltaTime;

        characterController.Move(velocity * Time.deltaTime);
    }
}