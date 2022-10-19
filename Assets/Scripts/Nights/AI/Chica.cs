using System;
using System.Collections.Generic;

public class Chica : Animatronic
{
    private PlayerMovement playerMovement;

    public void Start()
    {
        UpdateAnimatorName();
        playerMovement = player.GetComponent<PlayerMovement>();
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