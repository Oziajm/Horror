using Gameplay.Managers;
using System;
using UnityEngine;

public class CheckHidingSpotState : BaseState
{
    private readonly Animatronic ANIMATRONIC;

    private readonly string CHECK_PHOTOBOOTH_ANIMATION_NAME = "CheckPhotoBooth";

    private bool isReadyToPlayAnimation;

    public CheckHidingSpotState(Animatronic animatronic, Vector3 positionToCheck) : base(animatronic.gameObject)
    {
        this.ANIMATRONIC = animatronic;
    }

    public override void Initialize()
    {
        isReadyToPlayAnimation = false;
        
        EventsManager.Instance.PlayerLeftHidingSpot += ChangeStateToChaseState;
    }

    public override Type Tick()
    {
        if (ANIMATRONIC.AnimatronicNavMeshController.GetRemaningDistance() <= 0.05f)
        {
            ANIMATRONIC.transform.position = ANIMATRONIC.HidingSpotToCheck.AnimatronicPositionToCheckSpot;

            if (ANIMATRONIC.IsPlayerSpotted())
            {
                ChangeStateToChaseState();
            }
            else
            {
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
        }
        else
        {
            ANIMATRONIC.AnimatronicNavMeshController.SetNewDestination(ANIMATRONIC.HidingSpotToCheck.AnimatronicPositionToCheckSpot);
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

        ANIMATRONIC.transform.rotation = Quaternion.LookRotation(dir);
        ANIMATRONIC.AnimatronicNavMeshController.SwitchAnimatronicMovement(false, 0);
        isReadyToPlayAnimation = true;
    }
}
