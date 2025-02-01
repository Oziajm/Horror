using UnityEngine;

public class AnimatronicsSoundsController : MonoBehaviour
{
    [SerializeField]
    private AudioClip startUpSound;

    [SerializeField]
    private AudioClip angerSound;

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayStartUpSound()
    {
        if (!audioSource.isPlaying)
            audioSource.PlayOneShot(startUpSound);
    }

    public void PlayAngerSound()
    {
        audioSource.PlayOneShot(angerSound);
    }
}
