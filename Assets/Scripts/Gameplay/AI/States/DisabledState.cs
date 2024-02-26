using System;
using Gameplay.Managers;

public class DisabledState : BaseState
{
    private readonly string ANIMATRONIC_ACTIVATED_VARIABLE = "animatronicActivated";

    private readonly Animatronic ANIMATRONIC;

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
        OnAnimatronicsActivated();
        return typeof(IdleState);
    }

    private void OnAnimatronicsActivated()
    {
        ANIMATRONIC.Animator.SetBool(ANIMATRONIC_ACTIVATED_VARIABLE, true);
        ANIMATRONIC.SoundsController.PlayStartUpSound();
    }
}
