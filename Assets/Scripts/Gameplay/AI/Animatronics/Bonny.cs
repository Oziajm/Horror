using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class Bonny : Animatronic
{
    private MovementController playerMovement;

    public void Start()
    {
        UpdateAnimatorName();
        playerMovement = Player.GetComponent<MovementController>();
        AssignSoundController(GetComponent<AnimatronicsSoundsController>());
    }

    protected void InitializeStateMachine()
    {
        stateMachine.SetStates(new Dictionary<Type, BaseState>()
        {

        });
    }

    public override bool IsPlayerSpotted()
    {
        return fov.CanSeePlayer && !playerMovement.IsCrouching;
    }
}
