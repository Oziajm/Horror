using System;
using System.Collections.Generic;

public class Endo : Animatronic
{
    private MovementController playerMovement;

    public void Start()
    {
        stateMachine = GetComponent<StateMachine>();
        InitializeStateMachine();
        UpdateAnimatorName();
        playerMovement = player.GetComponent<MovementController>();
        soundsController = GetComponent<AnimatronicsSoundsController>();
    }
    protected void InitializeStateMachine()
    {
        stateMachine.SetStates(new Dictionary<Type, BaseState>()
        {
            {typeof(DisabledState), new DisabledState(this)},
            {typeof(RoamingState), new RoamingState(this)},
            {typeof(FrozenState), new FrozenState(this)},
            {typeof(ChaseState), new ChaseState(this)}
        });
    }

    public override bool IsPlayerSpotted()
    {
        return fov.canSeePlayer && !playerMovement.IsCrouching;
    }
}
