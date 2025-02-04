using Gameplay.Managers;
using Gameplay.Utils;
using System.Collections;
using UnityEngine;

public class AmbientMusicManager : Singleton<AmbientMusicManager>
{
    [SerializeField]
    private AudioSource audioSource;

    [SerializeField]
    private AudioClip ambient;
    [SerializeField]
    private AudioClip chaseAmbient;

    private void PlayMusic(AudioClip musicClip)
    {
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }
        audioSource.clip = musicClip;
        audioSource.Play();
    }

    private void PlayChaseMusic()
    {
        PlayMusic(chaseAmbient);
    }

    private void PlayAmbientMusic()
    {
        PlayMusic(ambient);
    }

    private void OnEnable()
    {
        EventsManager.Instance.PlayerSpotted += PlayChaseMusic;
        EventsManager.Instance.PlayerOutOfSight += PlayAmbientMusic;
    }

    private void OnDisable()
    {
        EventsManager.Instance.PlayerSpotted -= PlayChaseMusic;
        EventsManager.Instance.PlayerOutOfSight -= PlayAmbientMusic;
    }
}