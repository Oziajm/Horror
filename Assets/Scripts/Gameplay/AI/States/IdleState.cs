using System;

public class IdleState : BaseState
{
    private readonly string IDLE_ANIMATION_NAME = "IDLE_ANIMATION";

    private readonly string PLAYER_REACHED_DESTINATION_ANIMATOR_VARIABLE = "reachedDestination";

    private readonly Animatronic ANIMATRONIC;

    public IdleState(Animatronic animatronic) : base(animatronic.gameObject)
    {
        this.ANIMATRONIC = animatronic;
    }

    public override void Initialize()
    {
        ANIMATRONIC.Animator.SetBool(PLAYER_REACHED_DESTINATION_ANIMATOR_VARIABLE, false);
        ANIMATRONIC.Animator.Play(IDLE_ANIMATION_NAME);

        ANIMATRONIC.AnimatronicNavMeshController.SwitchAnimatronicMovement(false, 0);
    }

    public override Type Tick()
    {   
        ANIMATRONIC.UpdateAnimatorName();

        if (ANIMATRONIC.IsPlayerSpotted())
        {
            return typeof(ChaseState);
        }

        ANIMATRONIC.Animator.SetBool(PLAYER_REACHED_DESTINATION_ANIMATOR_VARIABLE, true);
        if (ANIMATRONIC.AnimatorClipInfo[0].clip.name != IDLE_ANIMATION_NAME)
        {
            return typeof(RoamingState);
        }

        return null;
    }
}
