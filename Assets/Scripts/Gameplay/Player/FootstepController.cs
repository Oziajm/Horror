using UnityEngine;

public class FootstepController : MonoBehaviour
{
    #region Variables

    [SerializeField] 
    private AudioClip[] walkingClips;
    [SerializeField] 
    private AudioSource audioSource;

    private float footstepTimer;

    #endregion

    #region Unity Methods

    private void Awake()
    {
        footstepTimer = 0f;
    }

    #endregion

    #region Public Methods

    public void HandleFootSteps(float stepOffSet)
    {
        footstepTimer -= Time.deltaTime;
        if (footstepTimer <= 0)
        {
            audioSource.PlayOneShot(walkingClips[Random.Range(0, walkingClips.Length - 1)]);
            footstepTimer = stepOffSet;
        }
    }

    #endregion
}
