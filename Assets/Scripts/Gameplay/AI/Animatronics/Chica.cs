using System;
using System.Collections.Generic;

public class Chica : Animatronic
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
        StateMachine.SetStates(new Dictionary<Type, BaseState>()
        {

        });
    }

    public override bool IsPlayerSpotted()
    {
        return fov.CanSeePlayer && !playerMovement.IsCrouching;
    }
}