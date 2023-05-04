using UnityEngine;

public class FootstepController : MonoBehaviour
{
    #region Variables

    [SerializeField] 
    private AudioClip[] walkingClips;
    [SerializeField] 
    private AudioSource audioSource;
    [SerializeField] 
    private PlayerSettings playerSettings;

    private PlayerMovement playerMovement;

    private float footstepTimer;
    private float GetStepOffset => playerMovement.IsSprinting ? playerSettings.sprintSoundDelay : playerSettings.stepSoundDelay;

    #endregion

    #region Unity Methods

    private void Start()
    {
        footstepTimer = 0f;

        playerMovement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        if (playerMovement.IsMoving && !playerMovement.IsCrouching)
        {
            HandleFootSteps();
        }
    }

    #endregion

    #region Private Methods

    private void HandleFootSteps()
    {
        footstepTimer -= Time.deltaTime;
        if (footstepTimer <= 0)
        {
            audioSource.PlayOneShot(walkingClips[Random.Range(0, walkingClips.Length - 1)]);
            footstepTimer = GetStepOffset;
        }
    }

    #endregion
}
