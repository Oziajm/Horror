using UnityEngine;

public class PlayerSettings : MonoBehaviour
{
    protected float crouchingSpeed = 2f;
    protected float standHeight = 1.7f;
    protected float timeToCrouch = 0.25f;

    protected float sprintingDelayMultiplier = 0.5f;
    protected float sprintSpeed = 5f;
    protected float maxStamina = 10f;

    protected float stepSoundDelay = 0.5f;
    protected float walkingSpeed = 3f;

    protected float stamina = 10f;
    protected float staminaUsageSpeed = 1f;
    protected float staminaRegenerationDelay = 2f;
    protected float staminaRegenerationSpeed = 1f;

    protected bool isMoving = false;
    protected bool isSprinting = false;
    protected bool isCrouching = false;

    protected bool duringCrouchAnimation = false;

    public bool IsMoving => isMoving;
    public bool IsSprinting => isSprinting;
    public bool IsCrouching => isCrouching;
}
