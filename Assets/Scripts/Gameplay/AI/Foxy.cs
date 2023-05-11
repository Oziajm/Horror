using System;
using System.Collections.Generic;
using UnityEngine;

public class Foxy : Animatronic
{
    public FlashLightController flashLightController;
    public Material eyes;
    public Light[] eyesLights;
    public bool isImmuneToFlashlight = false;
    public bool isTriggered = false;

    private MovementController playerMovement;

    public bool IsFlashlightOn => flashLightController.IsOn;

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
            {typeof(FoxyFlashedState), new FoxyFlashedState(this)},
            {typeof(ChaseState), new ChaseState(this)}
        });
    }

    public override bool IsPlayerSpotted()
    {
        return fov.canSeePlayer;
    }
}
