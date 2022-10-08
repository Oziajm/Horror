using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Space(10)]
    [Header("General")]
    [Space(10)]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private CharacterController controller;
    [SerializeField] private Transform cameraTransform;

    [Space(10)]
    [Header("General Settings")]
    [Space(10)]
    [SerializeField] private float mouseSensitivity = 100f;
    [SerializeField] private float speed = 2f;
    [SerializeField] private float crouchSpeed = 1f;
    [SerializeField] private float standHeight = 1.7f;
    [SerializeField] private float timeToCrouch = 0.25f;
    private bool isMoving = false;
    private bool isSprinting = false;
    private bool isCrouching = false;
    private bool duringCrouchAnimation = false;

    [Space(10)]
    [Header("Stamina")]
    [Space(10)]
    [SerializeField] private StaminaBarController staminaController;
    [SerializeField] private float sprintSpeed = 5f;
    [SerializeField] private float staminaUsageSpeed = 1f;
    [SerializeField] private float staminaRegenerationDelay = 2f;
    [SerializeField] private float staminaRegenerationSpeed = 1f;
    [SerializeField] private float maxStamina = 10f;
    private float stamina = 10f;
    private Coroutine staminaRegeneration = null;

    [Space(10)]
    [Header("Camera Shake")]
    [Space(10)]
    [SerializeField] private float magnitude = 0.05f;
    [SerializeField] private float sprintingFrequency = 20f;
    [SerializeField] private float walkingFrequency = 10f;
    [SerializeField] private float crouchingFrequency = 5;
    private float GetCameraShakeFrequency => isSprinting ? sprintingFrequency : isCrouching ? crouchingFrequency : walkingFrequency;
    private Vector3 defaultCameraLocalPos;
    private float cameraShakeTimer;

    [Space(10)]
    [Header("Footsteps")]
    [Space(10)]
    [SerializeField] private float stepSpeed = 0.6f;
    [SerializeField] private float sprintingMultiplier = 0.5f;
    [SerializeField] private AudioSource audioSource = default;
    [SerializeField] private AudioClip[] walkingClips;
    private float footstepTimer = 0f;
    private float GetStepOffset => isSprinting ? stepSpeed * sprintingMultiplier : stepSpeed;

    private readonly float groundDistance = 0.4f;
    private readonly float gravity = -19.62f;

    Vector3 velocity;
    bool isGrounded;
    float xRotation = 0f;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        staminaController.SetMaxValue(maxStamina);
        staminaController.SetStaminaVisible(false);
        defaultCameraLocalPos = cameraTransform.localPosition;
    }

    void Update()
    {
        MovementController();
        HandleFootSteps();
        CameraController();
    }

    private void CameraController()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90, 90f);

        cameraTransform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
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

        float appliedSpeed = isCrouching ? crouchSpeed : speed;
        if (stamina > 0 && Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.W))
        {
            if (duringCrouchAnimation) StopCoroutine(DoCrouchStand());
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
            appliedSpeed = sprintSpeed;
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

        if(Input.GetKeyDown(KeyCode.C) && !Input.GetKey(KeyCode.LeftShift))
        {
            // toggle crouch
            if (duringCrouchAnimation) StopCoroutine(DoCrouchStand());
            isCrouching = !isCrouching;
            StartCoroutine(DoCrouchStand());
        }

        if (x != 0 || z != 0)
        {
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }

        if (!duringCrouchAnimation)
        {
            CameraShake();
        }

        staminaController.SetStaminaVisible(stamina < maxStamina);
        staminaController.SetValue(stamina);

        controller.Move(appliedSpeed * Time.deltaTime * move);

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }

    private void HandleFootSteps()
    {
        if (!isMoving) return;

        footstepTimer -= Time.deltaTime;
        if(footstepTimer <= 0 && !isCrouching)
        {
            audioSource.PlayOneShot(walkingClips[Random.Range(0, walkingClips.Length - 1)]);
            footstepTimer = GetStepOffset;
        }
    }

    private void CameraShake()
    {
        if(isMoving)
        {
            Vector3 newPos = defaultCameraLocalPos;
            newPos.y += magnitude * Mathf.Sin(cameraShakeTimer * GetCameraShakeFrequency);
            cameraTransform.localPosition = newPos;
            cameraShakeTimer += Time.deltaTime;
            return;
        }

        Vector3.MoveTowards(cameraTransform.localPosition, defaultCameraLocalPos, 1f);
        cameraShakeTimer = 0;
    }

    IEnumerator RegenerateStamina()
    {
        yield return new WaitForSeconds(staminaRegenerationDelay);

        while (stamina < maxStamina)
        {
            stamina += Time.deltaTime * staminaRegenerationSpeed;
            staminaController.SetValue(stamina);
            yield return null;
        }

        staminaRegeneration = null;
    }

    IEnumerator DoCrouchStand()
    {
        duringCrouchAnimation = true;
        float time = 0f;
        float startHeight = controller.height;
        float targetHeight = isCrouching ? standHeight - 1f : standHeight;
        float targetCenter = isCrouching ? 0.2f : 0f;

        while(time <= timeToCrouch)
        {
            Debug.Log($"startHeight: {startHeight} | currentHeight: {controller.height} | targetHeight: {targetHeight}");
            controller.height = Mathf.Lerp(startHeight, targetHeight, time/timeToCrouch);
            time += Time.deltaTime;
            yield return null;
        }

        controller.height = targetHeight;
        controller.center = new Vector3(0f, targetCenter, 0f);

        duringCrouchAnimation = false;
    }
}