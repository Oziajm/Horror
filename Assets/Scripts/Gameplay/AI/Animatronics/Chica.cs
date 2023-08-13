using System;
using System.Collections.Generic;

public class Chica : Animatronic
{
    private MovementController playerMovement;

    public void Start()
    {
        UpdateAnimatorName();
        playerMovement = player.GetComponent<MovementController>();
        soundsController = GetComponent<AnimatronicsSoundsController>();
    }

    protected void InitializeStateMachine()
    {
        stateMachine.SetStates(new Dictionary<Type, BaseState>()
        {

        });
    }

    public override bool IsPlayerSpotted()
    {
        return fov.canSeePlayer && !playerMovement.IsCrouching;
    }
}