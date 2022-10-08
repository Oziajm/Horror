using UnityEngine;

public class FootstepController : PlayerSettings
{
    [SerializeField] private AudioClip[] walkingClips;
    [SerializeField] private AudioSource audioSource;

    private float footstepTimer = 0f;
    private float GetStepOffset => IsSprinting ? stepSoundDelay * sprintingDelayMultiplier : stepSoundDelay;

    private void Update()
    {
        if (!duringCrouchAnimation)
        {
            HandleFootSteps();
        }
    }

    private void HandleFootSteps()
    {
        if (!IsMoving) return;

        footstepTimer -= Time.deltaTime;
        if (footstepTimer <= 0 && !IsCrouching)
        {
            audioSource.PlayOneShot(walkingClips[Random.Range(0, walkingClips.Length - 1)]);
            footstepTimer = GetStepOffset;
        }
    }
}
