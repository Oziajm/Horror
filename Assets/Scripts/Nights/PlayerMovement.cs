using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private CharacterController controller;
    [SerializeField] private float mouseSensitivity = 100f;
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private StaminaBarController staminaBar;

    private readonly float groundDistance = 0.4f;
    private readonly float speed = 2f;
    private readonly float sprintSpeed = 5f;
    private readonly float staminaRegenerationDelay = 2f;
    private readonly float maxStamina = 10f;
    private readonly float gravity = -19.62f;

    private float stamina = 10f;
    private Coroutine healthRegeneration = null;

    Vector3 velocity;
    bool isGrounded;
    float xRotation = 0f;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        staminaBar.setMaxValue(maxStamina);
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
        if (stamina > 0 && Input.GetKey(KeyCode.LeftShift))
        {
            if(healthRegeneration != null)
            {
                StopCoroutine(healthRegeneration);
                healthRegeneration = null;
            }

            appliedSpeed = sprintSpeed;
            stamina -= Time.deltaTime;
        }
        else if (stamina < maxStamina)
        {
             if(healthRegeneration == null)
             {
                healthRegeneration = StartCoroutine(regenerateHealth());
             }
        }
        staminaBar.setValue(stamina);

        controller.Move(appliedSpeed * Time.deltaTime * move);

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }

    IEnumerator regenerateHealth()
    {
        yield return new WaitForSeconds(staminaRegenerationDelay);

        while (stamina < maxStamina)
        {
            stamina += 0.2f;
            staminaBar.setValue(stamina);
            yield return new WaitForSeconds(0.1f);
        }

        healthRegeneration = null;
    }
}
