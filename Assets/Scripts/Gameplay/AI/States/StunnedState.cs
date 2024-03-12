using System;
using Gameplay.Managers;
using UnityEngine;

public class StunnedState : BaseState
{
    private readonly float STUN_DURATION = 5f;
    private readonly string FLASHED_ANIMATION = "FLASHED_ANIMATION";

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
        ANIMATRONIC.Animator.Play(FLASHED_ANIMATION);
    }

    public override Type Tick()
    {
        ANIMATRONIC.UpdateAnimatorName();
        elapsedTime += Time.deltaTime;

        if (elapsedTime > STUN_DURATION)
        {
            if (ANIMATRONIC.AnimatorClipInfo[0].clip.name != FLASHED_ANIMATION)
                return typeof(DisabledState);
        }


        return null;
    }
}
