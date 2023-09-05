using System;
using UnityEngine;

public class ChaseState : BaseState
{
    private readonly float SECONDS_NEEDED_TO_RETURN_TO_ROAMING = 5f;

    private readonly string IDLE_ANIMATION_NAME = "IDLE_ANIMATION";

    private readonly Animatronic ANIMATRONIC;

    private float elapsedTime;

    public ChaseState(Animatronic animatronic) : base(animatronic.gameObject)
    {
        elapsedTime = 0f;
        this.ANIMATRONIC = animatronic;
    }

    public override void Initialize()
    {
        ANIMATRONIC.AnimatronicNavMeshController.SwitchAnimatronicMovement(true, ANIMATRONIC.MovementSpeed * ANIMATRONIC.RunningMultiplier);
    }

    public override Type Tick()
    {
        ANIMATRONIC.UpdateAnimatorName();

        elapsedTime += Time.deltaTime;
        ANIMATRONIC.AnimatronicNavMeshController.SetNewDestination(ANIMATRONIC.Player.transform.position);

        if (ANIMATRONIC.IsPlayerSpotted())
        {
            elapsedTime = 0f;
        }
        else
        {
            if (elapsedTime > SECONDS_NEEDED_TO_RETURN_TO_ROAMING)
            {
                elapsedTime = 0f;

                ANIMATRONIC.Animator.Play(IDLE_ANIMATION_NAME);
                return typeof(IdleState);
            }
        }

        if (ANIMATRONIC as Endo)
        {
            bool shouldBeActive = !ANIMATRONIC.IsVisible(ANIMATRONIC.gameObject);

            ANIMATRONIC.Animator.enabled = shouldBeActive;
            ANIMATRONIC.AnimatronicNavMeshController.SwitchAnimatronicMovement(shouldBeActive, shouldBeActive ? ANIMATRONIC.MovementSpeed * ANIMATRONIC.RunningMultiplier : 0);
        }

        return null;
    }
}
