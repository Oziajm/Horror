using Gameplay.Managers;
using System;
using UnityEngine;

public class RoamingState : BaseState
{
    private readonly Animatronic animatronic;
    private int lastLocationNumber = -1;
    private const float DestinationThreshold = 0.05f;

    public RoamingState(Animatronic animatronic) : base(animatronic.gameObject)
    {
        this.animatronic = animatronic;
    }

    public override void Initialize()
    {
        animatronic.AnimatronicNavMeshController.SwitchAnimatronicMovement(true, animatronic.AnimatronicSettings.MovementSpeed);
        SetNextPatrolLocation();
        animatronic.Animator.CrossFade(StringsManager.Instance.WALK_ANIMATION_NAME, 0.1f);
    }

    public override Type Tick()
    {
        if (animatronic.AnimatronicNavMeshController.GetRemaningDistance() <= DestinationThreshold)
        {
            return typeof(IdleState);
        }

        return null;
    }

    private void SetNextPatrolLocation()
    {
        int patrolPoints = animatronic.PatrolLocations.Count;
        if (patrolPoints == 0) return;

        int newLocationNumber;
        do
        {
            newLocationNumber = UnityEngine.Random.Range(0, patrolPoints);
        } while (newLocationNumber == lastLocationNumber && patrolPoints > 1);

        lastLocationNumber = newLocationNumber;
        animatronic.AnimatronicNavMeshController.SetNewDestination(animatronic.PatrolLocations[newLocationNumber]);
    }
}