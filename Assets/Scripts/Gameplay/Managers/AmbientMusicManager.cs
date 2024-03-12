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

    private float volume;
    private float fadeOutValue;

    private void Start()
    {
        volume = audioSource.volume;
        fadeOutValue = volume / 10;

        EventsManager.Instance.PlayerSpotted += PlayChaseMusic;
        EventsManager.Instance.PlayerOutOfSight += PlayAmbientMusic;

        PlayAmbientMusic();
    }

    private void PlayMusic(AudioClip musicClip)
    {
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }
        audioSource.clip = musicClip;
        audioSource.Play();
    }

    public void PlayChaseMusic()
    {
        PlayMusic(chaseAmbient);
    }

    public void PlayAmbientMusic()
    {
        PlayMusic(ambient);
    }
}