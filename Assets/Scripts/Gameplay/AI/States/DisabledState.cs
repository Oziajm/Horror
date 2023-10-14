using System;
using Gameplay.Managers;

public class DisabledState : BaseState
{
    private readonly string ANIMATRONIC_ACTIVATED_VARIABLE = "animatronicActivated";

    private readonly Animatronic ANIMATRONIC;

    private bool turnedOnEvent = false;

    public DisabledState(Animatronic animatronic) : base(animatronic.gameObject)
    {
        this.ANIMATRONIC = animatronic;
        EventsManager.Instance.AnimatronicsActivated += OnAnimatronicsActivated;
    }

    public override void Initialize()
    {

    }

    public override Type Tick()
    {
        if (turnedOnEvent)
            return typeof(IdleState);
        return null;
    }

    private void OnAnimatronicsActivated()
    {
        ANIMATRONIC.Animator.SetBool(ANIMATRONIC_ACTIVATED_VARIABLE, true);
        ANIMATRONIC.SoundsController.PlayStartUpSound();
        turnedOnEvent = true;
    }
}
