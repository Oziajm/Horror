using Gameplay.Managers;
using System;
using UnityEngine;

public class ChaseState : BaseState
{
    private readonly float SECONDS_NEEDED_TO_RETURN_TO_ROAMING = 15f;

    private readonly string RUN_ANIMATION_NAME = "RUN_ANIMATION";

    private readonly Animatronic ANIMATRONIC;

    private float elapsedTime;

    private bool isPlayerSpotted;

    public ChaseState(Animatronic animatronic) : base(animatronic.gameObject)
    {
        this.ANIMATRONIC = animatronic;
    }

    public override void Initialize()
    {
        elapsedTime = 0f;

        EventsManager.Instance.PlayerEnteredHidingSpot += ChangeStateToCheckHidingSpotState;

        ANIMATRONIC.Animator.Play(RUN_ANIMATION_NAME);

        ANIMATRONIC.AnimatronicNavMeshController.SwitchAnimatronicMovement(true, ANIMATRONIC.MovementSpeed * ANIMATRONIC.RunningMultiplier);
    }

    public override Type Tick()
    {
        ANIMATRONIC.UpdateAnimatorName();

        ANIMATRONIC.AnimatronicNavMeshController.SetNewDestination(ANIMATRONIC.Player.transform.position);

        if (ANIMATRONIC.IsPlayerSpotted())
        {
            elapsedTime = 0f;
            isPlayerSpotted = true;
        }
        else
        {
            elapsedTime += Time.deltaTime;

            if (elapsedTime > SECONDS_NEEDED_TO_RETURN_TO_ROAMING)
            {
                isPlayerSpotted = false;

                EventsManager.Instance.PlayerEnteredHidingSpot -= ChangeStateToCheckHidingSpotState;
                return typeof(IdleState);
            }
        }

        return null;
    }

    private void ChangeStateToCheckHidingSpotState(HidingSpot hidingSpotToCheck)
    {
        if (isPlayerSpotted)
        {
            ANIMATRONIC.SetNewHidingSpotToCheck(hidingSpotToCheck);
            EventsManager.Instance.PlayerEnteredHidingSpot -= ChangeStateToCheckHidingSpotState;
            ANIMATRONIC.StateMachine.SwitchState(typeof(CheckHidingSpotState));
        }
    }
}
