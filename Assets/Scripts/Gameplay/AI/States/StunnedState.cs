using System;
using Gameplay.Managers;
using UnityEngine;

public class StunnedState : BaseState
{
    private readonly Animatronic ANIMATRONIC;

    private float elapsedTime;

    public StunnedState(Animatronic animatronic) : base(animatronic.gameObject)
    {
        this.ANIMATRONIC = animatronic;
    }

    public override void Initialize()
    {
        EventsManager.Instance.PlayerOutOfSight?.Invoke();
        ANIMATRONIC.AnimatronicNavMeshController.SwitchAnimatronicMovement(false, 0);
        elapsedTime = 0;
        ANIMATRONIC.Animator.CrossFade(StringsManager.Instance.FLASHED_ANIMATION, 0.05f);
    }

    public override Type Tick()
    {
        elapsedTime += Time.deltaTime;

        if (elapsedTime > ANIMATRONIC.AnimatronicSettings.StunDuration)
        {
            return typeof(IdleState);
        }

        return null;
    }
}
