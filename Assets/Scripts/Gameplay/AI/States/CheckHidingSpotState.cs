using Gameplay.Managers;
using System;
using UnityEngine;

public class CheckHidingSpotState : BaseState
{
    private readonly Animatronic ANIMATRONIC;

    private readonly string CHECK_PHOTOBOOTH_ANIMATION_NAME = "CHECK_PHOTOBOOTH_ANIMATION";
    private readonly string IDLE_ANIMATION_NAME = "IDLE_ANIMATION";

    private bool isReadyToPlayAnimation;

    public CheckHidingSpotState(Animatronic animatronic ) : base(animatronic.gameObject)
    {
        this.ANIMATRONIC = animatronic;
    }

    public override void Initialize()
    {
        isReadyToPlayAnimation = false;

        ANIMATRONIC.AnimatronicNavMeshController.SetNewDestination(ANIMATRONIC.HidingSpotToCheck.AnimatronicPositionToCheckSpot);

        EventsManager.Instance.PlayerLeftHidingSpot += ChangeStateToChaseState;
    }

    public override Type Tick()
    {
        if (ANIMATRONIC.AnimatronicNavMeshController.GetRemaningDistance() <= 0.05f)
        {
            ANIMATRONIC.Animator.Play(IDLE_ANIMATION_NAME);

            if (ANIMATRONIC.IsPlayerSpotted())
            {
                ChangeStateToChaseState();
            }

                if (!isReadyToPlayAnimation)
                {
                    PlaceAnimatronicForAnimation();
                }
                else
                {
                    ANIMATRONIC.Animator.Play(CHECK_PHOTOBOOTH_ANIMATION_NAME);

                    if(!ANIMATRONIC.HidingSpotToCheck.IsOpen)
                    {
                        ANIMATRONIC.HidingSpotToCheck.OpenByAnimatronic();
                    }
                }
        }

        return null;
    }

    public void ChangeStateToChaseState()
    {
        ANIMATRONIC.StateMachine.SwitchState(typeof(ChaseState));
    }

    private void PlaceAnimatronicForAnimation()
    {
        Vector3 dir = (ANIMATRONIC.HidingSpotToCheck.HidingSpotPosition - ANIMATRONIC.HidingSpotToCheck.AnimatronicPositionToCheckSpot).normalized;

        ANIMATRONIC.AnimatronicNavMeshController.SwitchAnimatronicMovement(false, 0);
        ANIMATRONIC.transform.rotation = Quaternion.LookRotation(dir);
        ANIMATRONIC.transform.position = ANIMATRONIC.HidingSpotToCheck.AnimatronicPositionToCheckSpot;
    }
}
