using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class Freddy : Animatronic
{
    private PlayerMovement playerMovement;

    public void Start()
    {
        animatorClipInfo = animator.GetCurrentAnimatorClipInfo(0);
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
