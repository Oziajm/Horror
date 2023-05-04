using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable/Player Settings", fileName = "Player Settings")]
public class PlayerSettings : ScriptableObject
{
    public float crouchSpeed = 2f;
    public float standHeight = 1.7f;
    public float timeToCrouch = 0.25f;
    
    public float sprintSoundDelay = 0.5f;
    public float sprintSpeed = 5f;
    
    public float stepSoundDelay = 0.5f;
    public float walkSpeed = 3f;
    
    public float maxStamina = 10f;
    public float staminaUsageSpeed = 1f;
    public float staminaRegenerationDelay = 2f;
    public float staminaRegenerationSpeed = 1f;
}
