using UnityEngine;

public class PlayerSettings : ScriptableObject
{
    public float crouchingSpeed = 2f;
    public float standHeight = 1.7f;
    public float timeToCrouch = 0.25f;
    
    public float sprintingDelayMultiplier = 0.5f;
    public float sprintSpeed = 5f;
    public float maxStamina = 10f;
    
    public float stepSoundDelay = 0.5f;
    public float walkingSpeed = 3f;
    
    public float stamina = 10f;
    public float staminaUsageSpeed = 1f;
    public float staminaRegenerationDelay = 2f;
    public float staminaRegenerationSpeed = 1f;
    
    public bool isMoving = false;
    public bool isSprinting = false;
    public bool isCrouching = false;
    
    public bool duringCrouchAnimation = false;

}
