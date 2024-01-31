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

    private void Awake()
    {
        volume = audioSource.volume;
        fadeOutValue = volume/10;
    }

    private void Start()
    {
        PlayAmbientMusic();
    }

    private void PlayMusic(AudioClip musicClip)
    {
        StartCoroutine(FadeOutMusic());
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }
        audioSource.clip = musicClip;
        audioSource.Play();
        StartCoroutine(FadeInMusic());
    }

    public void PlayChaseMusic()
    {
        PlayMusic(chaseAmbient);
    }

    public void PlayAmbientMusic()
    {
        PlayMusic(ambient);
    }

    private IEnumerator FadeOutMusic()
    {
        while (audioSource.volume > 0)
        {
            audioSource.volume -= fadeOutValue * Time.deltaTime;

            yield return null;
        }
    }

    private IEnumerator FadeInMusic()
    {
        while (audioSource.volume < volume)
        {
            audioSource.volume += fadeOutValue * Time.deltaTime;

            yield return null;
        }
    }
}