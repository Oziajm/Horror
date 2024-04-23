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
    [SerializeField] private AudioClip audioClip;

    private bool isBeingOpen;

    public override string GetHoverText() => IsOpen ? "Close" : "Open";

    public override void Interact()
    {
        if (isBeingOpen) return; 

        PlaySound();

        if (IsOpen)
            animator.SetBool(PARAMETER_NAME, false);
        else
            animator.SetBool(PARAMETER_NAME, true);

        IsOpen = !IsOpen;
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
            audioSource.PlayOneShot(audioClip);
    }
}