using System;
using Gameplay.Managers;

public class DisabledState : BaseState
{
    private readonly string ANIMATRONIC_ACTIVATED_VARIABLE = "animatronicActivated";

    private readonly string STUN_ANIMATION = "STUN_ANIMATION_1";
    private readonly string EXITING_STUN_ANIMATION = "STUN_ANIMATION_2";

    private readonly Animatronic ANIMATRONIC;

    public DisabledState(Animatronic animatronic) : base(animatronic.gameObject)
    {
        this.ANIMATRONIC = animatronic;
    }

    public override void Initialize()
    {
        ANIMATRONIC.Animator.SetBool(ANIMATRONIC_ACTIVATED_VARIABLE, true);
    }

    public override Type Tick()
    {
        ANIMATRONIC.UpdateAnimatorName();

        string animationName = ANIMATRONIC.AnimatorClipInfo[0].clip.name;

        if (animationName != STUN_ANIMATION && animationName != EXITING_STUN_ANIMATION)
            return typeof(IdleState);

        return null;
    }
}
