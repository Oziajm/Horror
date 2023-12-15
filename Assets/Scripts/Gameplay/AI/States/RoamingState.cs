using System;
using UnityEngine;

public class RoamingState : BaseState
{
    private readonly Animatronic ANIMATRONIC;

    private readonly string IDLE_ANIMATION_NAME = "IDLE_ANIMATION";

    private bool initialized = false;

    private int lastLocationNumber = 0;
    private int newLocationNumber = 1;

    public RoamingState(Animatronic animatronic) : base(animatronic.gameObject)
    {
        this.ANIMATRONIC = animatronic;
    }

    public override void Initialize()
    {
        initialized = false;

        ANIMATRONIC.AnimatronicNavMeshController.SwitchAnimatronicMovement(true, ANIMATRONIC.MovementSpeed);

        newLocationNumber = UnityEngine.Random.Range(0, ANIMATRONIC.PatrolLocations.Count - 1);
        Debug.Log(newLocationNumber);

        if (lastLocationNumber == newLocationNumber)
        {
            newLocationNumber++;
            if (newLocationNumber > ANIMATRONIC.PatrolLocations.Count - 1)
            {
                newLocationNumber = 0;
            }
        }

        lastLocationNumber = newLocationNumber;

        Vector3 destination = ANIMATRONIC.PatrolLocations[newLocationNumber];

        ANIMATRONIC.AnimatronicNavMeshController.SetNewDestination(destination);

        initialized = true;
    }

    public override Type Tick()
    {
        if (!initialized) return null;

        ANIMATRONIC.UpdateAnimatorName();

        if (ANIMATRONIC.IsPlayerSpotted())
        {
            ANIMATRONIC.AnimatronicNavMeshController.SwitchAnimatronicMovement(false, 0);
        }

        if (ANIMATRONIC.AnimatronicNavMeshController.GetRemaningDistance() <= 2)
        {
            ANIMATRONIC.Animator.Play(IDLE_ANIMATION_NAME);
            return typeof(IdleState);
        }

        if (ANIMATRONIC as Endo)
        {
            bool shouldBeActive = !ANIMATRONIC.IsVisible(ANIMATRONIC.gameObject);

            ANIMATRONIC.Animator.enabled = shouldBeActive;
            ANIMATRONIC.AnimatronicNavMeshController.SwitchAnimatronicMovement(shouldBeActive, shouldBeActive ? ANIMATRONIC.MovementSpeed : 0);
        }

        return null;
    }
}