using System;

public class DisabledState : BaseState
{
    private readonly Animatronic animatronic;
    private bool turnedOnEvent = false;
    public DisabledState(Animatronic animatronic) : base(animatronic.gameObject)
    {
        this.animatronic = animatronic;
        EventManager.current.OnAnimatronicTurnedEvent += OnAnimatronicsTurned;
    }

    public override Type Tick()
    {
        if (turnedOnEvent)
            return typeof(RoamingState);
        return null;
    }

    private void OnAnimatronicsTurned()
    {
        animatronic.animator.SetBool("is2AM", true);
        animatronic.soundsController.PlayStartUpSound();
        turnedOnEvent = true;
    }
}
