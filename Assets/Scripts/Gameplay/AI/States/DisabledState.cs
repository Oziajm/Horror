using System;
using UnityEngine;
using Gameplay.Managers;

public class DisabledState : BaseState
{
    private readonly Animatronic animatronic;
    private bool turnedOnEvent = false;
    public DisabledState(Animatronic animatronic) : base(animatronic.gameObject)
    {
        this.animatronic = animatronic;
        EventsManager.Instance.AnimatronicsActivated += OnAnimatronicsActivated;
    }

    public override Type Tick()
    {
        if(animatronic.IsVisible(animatronic.player))
            return typeof(FrozenState);
        if (turnedOnEvent)
            return typeof(RoamingState);
        return null;
    }

    private void OnAnimatronicsActivated()
    {
        animatronic.animator.SetBool("is2AM", true);
        animatronic.soundsController.PlayStartUpSound();
        turnedOnEvent = true;
    }
}
