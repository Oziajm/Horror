using System;
using Gameplay.Managers;
using UnityEngine;

public class DefaultState : BaseState
{
    private readonly Animatronic ANIMATRONIC;

    private float elapsedTime;

    public DefaultState(Animatronic animatronic) : base(animatronic.gameObject)
    {
        this.ANIMATRONIC = animatronic;
    }

    public override void Initialize()
    {
        ANIMATRONIC.Animator.CrossFade(StringsManager.Instance.EXITING_STUN_ANIMATION, 1f);
    }
    public override Type Tick()
    {
        elapsedTime += Time.deltaTime;

        if (elapsedTime > ANIMATRONIC.AnimatronicSettings.StartUpDuration)
        {
            ANIMATRONIC.SoundsController.PlayStartUpSound();
            return typeof(IdleState);
        }

        return null;
    }
}
