using System;
using UnityEngine;

public class RoamingState : BaseState
{
    private readonly Animatronic ANIMATRONIC;

    private readonly string WALK_ANIMATION_NAME = "WALK_ANIMATION";

    private int lastLocationNumber;
    private int newLocationNumber;

    private bool initialized;

    public RoamingState(Animatronic animatronic) : base(animatronic.gameObject)
    {
        this.ANIMATRONIC = animatronic;
    }

    public override void Initialize()
    {
        ANIMATRONIC.AnimatronicNavMeshController.SwitchAnimatronicMovement(true, ANIMATRONIC.MovementSpeed);

        ANIMATRONIC.Animator.Play(WALK_ANIMATION_NAME);

        GetNextLocationToCheck();
    }

    public override Type Tick()
    {
        ANIMATRONIC.UpdateAnimatorName();

        if (ANIMATRONIC.IsPlayerSpotted())
        {
            return typeof(ChaseState);
        }

        if (ANIMATRONIC.AnimatronicNavMeshController.GetRemaningDistance() <= 0.05f)
        {
            return typeof(IdleState);
        }

        return null;
    }

    private void GetNextLocationToCheck()
    {
        int amountOfPatrolLocations = ANIMATRONIC.PatrolLocations.Count - 1;

        newLocationNumber = UnityEngine.Random.Range(0, amountOfPatrolLocations);

        if (lastLocationNumber == newLocationNumber)
        {
            newLocationNumber++;
            if (newLocationNumber > amountOfPatrolLocations)
            {
                newLocationNumber = 0;
            }
        }

        lastLocationNumber = newLocationNumber;

        Vector3 newDestination = ANIMATRONIC.PatrolLocations[newLocationNumber];

        ANIMATRONIC.AnimatronicNavMeshController.SetNewDestination(newDestination);
    }
}