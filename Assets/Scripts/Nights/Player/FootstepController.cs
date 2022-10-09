using UnityEngine;

public class FootstepController : MonoBehaviour
{
    #region Variables

    [SerializeField] private AudioClip[] walkingClips;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private PlayerSettings playerSettings;

    private PlayerMovement playerMovement;

    private float footstepTimer = 0f;
    private float GetStepOffset => playerMovement.IsSprinting ? playerSettings.stepSoundDelay * playerSettings.sprintingDelayMultiplier : playerSettings.stepSoundDelay;

    #endregion

    #region Unity Methods

    private void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        //if (!duringCrouchAnimation)
        //{
            HandleFootSteps();
        //}
    }

    #endregion

    #region Private Methods

    private void HandleFootSteps()
    {
        if (!playerMovement.IsMoving) return;

        footstepTimer -= Time.deltaTime;
        if (footstepTimer <= 0 && !playerMovement.IsCrouching)
        {
            audioSource.PlayOneShot(walkingClips[Random.Range(0, walkingClips.Length - 1)]);
            footstepTimer = GetStepOffset;
        }
    }

    #endregion
}
