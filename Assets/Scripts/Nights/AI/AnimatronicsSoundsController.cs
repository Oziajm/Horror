using UnityEngine;

public abstract class AnimatronicsSoundsController : MonoBehaviour
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
}
