using System;
using UnityEngine;

public class RoamingState : BaseState
{
    private const string WALK_ANIMATION_NAME = "WALK_ANIMATION";
    private readonly Animatronic animatronic;

    private int lastLocationNumber;
    private int newLocationNumber;
    private bool initialized;

    public RoamingState(Animatronic animatronic) : base(animatronic.gameObject)
    {
        this.animatronic = animatronic ?? throw new ArgumentNullException(nameof(animatronic));
    }

    public override void Initialize()
    {
        initialized = false;
        GetNextLocationToCheck();

        animatronic.AnimatronicNavMeshController.SwitchAnimatronicMovement(true, animatronic.MovementSpeed);
        animatronic.Animator.Play(WALK_ANIMATION_NAME);
    }

    public override Type Tick()
    {
        if (!initialized) return null;

        animatronic.UpdateAnimatorName();

        if (animatronic.IsPlayerSpotted())
        {
            return typeof(ChaseState);
        }

        if (animatronic.AnimatronicNavMeshController.GetRemaningDistance() <= 0.05f)
        {
            Debug.Log(animatronic.AnimatronicNavMeshController.GetRemaningDistance());
            return typeof(IdleState);
        }

        return null;
    }

    private void GetNextLocationToCheck()
    {
        int amountOfPatrolLocations = animatronic.PatrolLocations.Count;
        if (amountOfPatrolLocations == 0) return;

        newLocationNumber = UnityEngine.Random.Range(0, amountOfPatrolLocations);

        if (lastLocationNumber == newLocationNumber)
        {
            newLocationNumber = (newLocationNumber + 1) % amountOfPatrolLocations;
        }

        lastLocationNumber = newLocationNumber;
        Vector3 newDestination = animatronic.PatrolLocations[newLocationNumber];

        animatronic.AnimatronicNavMeshController.SetNewDestination(newDestination);
        initialized = true;
    }
}