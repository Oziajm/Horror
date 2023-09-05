using System;

public class IdleState : BaseState
{
    private readonly string WALK_ANIMATION_NAME = "WALK_ANIMATION";
    private readonly string RUN_ANIMATION_NAME = "RUN_ANIMATION";

    private readonly string PLAYER_REACHED_DESTINATION_ANIMATOR_VARIABLE = "reachedDestination";

    private readonly Animatronic ANIMATRONIC;

    public IdleState(Animatronic animatronic) : base(animatronic.gameObject)
    {
        this.ANIMATRONIC = animatronic;
    }

    public override void Initialize()
    {
        ANIMATRONIC.AnimatronicNavMeshController.SwitchAnimatronicMovement(false, 0);
    }

    public override Type Tick()
    {   
        ANIMATRONIC.UpdateAnimatorName();

        if (ANIMATRONIC.IsPlayerSpotted())
        {
            ANIMATRONIC.Animator.Play(RUN_ANIMATION_NAME);
            return typeof(ChaseState);
        }
        else
        {
            ANIMATRONIC.Animator.SetBool(PLAYER_REACHED_DESTINATION_ANIMATOR_VARIABLE, true);

            if (ANIMATRONIC.AnimatorClipInfo[0].clip.name == WALK_ANIMATION_NAME)
                return typeof(RoamingState);
        }

        if (ANIMATRONIC as Endo)
        {
            CheckIfEndoIsVisible();
        }

        return null;
    }

    private void CheckIfEndoIsVisible()
    {
        bool isVisible = ANIMATRONIC.IsVisible(ANIMATRONIC.gameObject);

        ANIMATRONIC.Animator.enabled = !isVisible;
    }
}
