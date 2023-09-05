using System;
using System.Collections;
using UnityEngine;
using Gameplay.Managers;
using System.Linq;
using System.Collections.Generic;

public class RoamingState : BaseState
{
    private readonly Animatronic ANIMATRONIC;

    private readonly string IDLE_ANIMATION_NAME = "IDLE_ANIMATION";

    public RoamingState(Animatronic animatronic) : base(animatronic.gameObject)
    {
        this.ANIMATRONIC = animatronic;
    }

    public override void Initialize()
    {
        ANIMATRONIC.AnimatronicNavMeshController.SwitchAnimatronicMovement(true, ANIMATRONIC.MovementSpeed);

        ANIMATRONIC.AnimatronicNavMeshController.SetNewDestination(ANIMATRONIC.PatrolLocations[UnityEngine.Random.Range(0, ANIMATRONIC.PatrolLocations.Count)]);
    }

    public override Type Tick()
    {
        ANIMATRONIC.UpdateAnimatorName();

        if (ANIMATRONIC.IsPlayerSpotted())
        {
            ANIMATRONIC.AnimatronicNavMeshController.SwitchAnimatronicMovement(false, 0);
        }

        if (ANIMATRONIC.AnimatronicNavMeshController.GetRemaningDistance() == 0)
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