using UnityEngine;

public class AnimatronicsSoundsController : MonoBehaviour
{
    [SerializeField] private AudioClip startUpSound;
    [SerializeField] private AudioClip screamSound;
    [SerializeField] private AudioClip chaseSound;
    [SerializeField] private AudioClip idleSound;
    [SerializeField] private AudioClip footstepSound;

    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayStartUpSound()
    {
        if (!audioSource.isPlaying)
            audioSource.PlayOneShot(startUpSound);
    }

    public void PlayScream()
    {
        if (!audioSource.isPlaying)
        { 
            audioSource.volume = 1f;
            audioSource.PlayOneShot(screamSound);
        }
    }
}
