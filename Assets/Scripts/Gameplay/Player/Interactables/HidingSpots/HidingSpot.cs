using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Gameplay.Managers;

public class HidingSpot : Interactable
{
    private const string PARAMETER_NAME = "ShouldOpen";

    public bool IsOpen { private set; get; } = true;
    public Vector3 HidingSpotPosition { private set; get; }
    public Vector3 AnimatronicPositionToCheckSpot { private set; get; }

    [Space(10)]
    [SerializeField] private Animator animator;
    [Space(10)]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip openAudioClip;
    [SerializeField] private AudioClip closeAudioClip;

    private bool isBeingOpen;

    public override string GetHoverText() => IsOpen ? "Close" : "Open";

    public override void Interact()
    {
        if (isBeingOpen) return; 

        IsOpen = !IsOpen;
        PlaySound();
        animator.SetBool(PARAMETER_NAME, IsOpen);
    }

    public void OnPlayerEnterTrigger(Vector3 hidingSpotPosition, Vector3 animatronicPositionToCheckSpot)
    {
        this.HidingSpotPosition = hidingSpotPosition;
        this.AnimatronicPositionToCheckSpot = animatronicPositionToCheckSpot;

        EventsManager.Instance.PlayerEnteredHidingSpot?.Invoke(this);
    }

    public void OnPlayerLeaveTrigger()
    {
        EventsManager.Instance.PlayerLeftHidingSpot?.Invoke();
    }

    public void OpenByAnimatronic()
    {
        isBeingOpen = true;
        PlaySound();
        animator.SetBool(PARAMETER_NAME, true);
    }

    private void PlaySound()
    {
        if (!audioSource.isPlaying)
            audioSource.PlayOneShot( IsOpen ? closeAudioClip : openAudioClip );
    }
}