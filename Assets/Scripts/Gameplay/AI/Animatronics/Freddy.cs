using System;
using System.Collections.Generic;

public class Freddy : Animatronic
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
        return fov.canSeePlayer && !playerMovement.IsCrouching;
    }
}
