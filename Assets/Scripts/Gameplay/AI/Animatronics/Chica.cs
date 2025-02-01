using System;
using System.Collections.Generic;

public class Chica : Animatronic
{
    private MovementController playerMovement;

    public void Start()
    {

    }

    protected void InitializeStateMachine()
    {
        StateMachine.SetStates(new Dictionary<Type, BaseState>()
        {

        });
    }

    public override bool IsPlayerSpotted()
    {
        return fov.SeenPlayer != null;
    }
}