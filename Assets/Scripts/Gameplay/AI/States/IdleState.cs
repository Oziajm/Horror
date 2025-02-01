using Gameplay.Managers;
using System;
using UnityEngine;

public class IdleState : BaseState
{
    private readonly Animatronic ANIMATRONIC;

    private float elapsedTime;

    public IdleState(Animatronic animatronic) : base(animatronic.gameObject)
    {
        this.ANIMATRONIC = animatronic;
    }

    public override void Initialize()
    {
        ANIMATRONIC.AnimatronicNavMeshController.SwitchAnimatronicMovement(false, 0);
        ANIMATRONIC.Animator.CrossFade(StringsManager.Instance.IDLE_ANIMATION_NAME, 0.1f);
        elapsedTime = 0;
    }

    public override Type Tick()
    {
        elapsedTime += Time.deltaTime;

        if (elapsedTime > ANIMATRONIC.AnimatronicSettings.IdleDuration)
        {
            return typeof(RoamingState);
        }

        return null;
    }
}
