using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private CharacterController controller;
    [SerializeField] private float mouseSensitivity = 100f;
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private StaminaBarController staminaController;

    private readonly float groundDistance = 0.4f;
    private readonly float speed = 2f;
    private readonly float sprintSpeed = 5f;
    private readonly float staminaRegenerationDelay = 2f;
    private readonly float staminaRegenerationSpeed = 1f;
    private readonly float maxStamina = 10f;
    private readonly float gravity = -19.62f;

    private float stamina = 10f;
    private Coroutine staminaRegeneration = null;
    private Vector3 defaultCameraLocalPos;
    private float cameraShakeTimer;

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

        float appliedSpeed = speed;
        bool isSprinting = false;
        if (stamina > 0 && Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.W))
        {
            if (staminaRegeneration != null)
            {
                StopCoroutine(staminaRegeneration);
                staminaRegeneration = null;
            }

            isSprinting = true;
            appliedSpeed = sprintSpeed;
            stamina = Mathf.Clamp(stamina - Time.deltaTime, 0, maxStamina);
        }
        else if (stamina < maxStamina)
        {
            if (staminaRegeneration == null)
            {
                staminaRegeneration = StartCoroutine(RegenerateStamina());
            }
        }
        if (x != 0 || z != 0) CameraShake(isSprinting);
        else 
        {
            Vector3.MoveTowards(cameraTransform.localPosition, defaultCameraLocalPos, 1f);
            cameraShakeTimer = 0;
        }

        staminaController.SetStaminaVisible(stamina < maxStamina);
        staminaController.SetValue(stamina);

        controller.Move(appliedSpeed * Time.deltaTime * move);

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }

    private void CameraShake(bool isSprinting)
    {
        float frequency = isSprinting ? 20f : 10f;
        float magnitude = 0.05f;

        Vector3 newPos = defaultCameraLocalPos;
        newPos.y += magnitude * Mathf.Sin(cameraShakeTimer * frequency);
        cameraTransform.localPosition = newPos;
        cameraShakeTimer += Time.deltaTime;
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
}