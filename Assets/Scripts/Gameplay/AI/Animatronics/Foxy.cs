using System;
using System.Collections.Generic;
using UnityEngine;

public class Foxy : Animatronic
{
    private readonly string RUN_ANIMATION = "RUN_ANIMATION";

    [SerializeField]
    private FoxysEyesController foxysEyesController;

    [SerializeField]
    private FlashLightController flashLightController;

    [SerializeField]
    private FoxyParameters foxyParameters;

    private bool canBeStunned;
    private float elapsedTime;

    public void Start()
    {
        canBeStunned = true;
        elapsedTime = 0;

        foxysEyesController.SetFoxyCalmEyes();
        stateMachine = GetComponent<StateMachine>();
        InitializeStateMachine();
        UpdateAnimatorName();
        AssignSoundController(GetComponent<AnimatronicsSoundsController>());
    }
    protected void InitializeStateMachine()
    {
        stateMachine.SetStates(new Dictionary<Type, BaseState>()
        {
            {typeof(DisabledState), new DisabledState(this)},
            {typeof(RoamingState), new RoamingState(this)},
            {typeof(IdleState), new IdleState(this)},
            {typeof(StunnedState), new StunnedState(this)},
            {typeof(ChaseState), new ChaseState(this)}
        });
    }

    private void Update()
    {
        UpdateAnimatorName();
        HandleFootSteps();
        HandleWeakness();
    }

    public override bool IsPlayerSpotted()
    {
        return fov.canSeePlayer;
    }

    private void HandleFootSteps()
    {
        if (AnimatorClipInfo[0].clip.name == WALK_ANIMATION_NAME)
        {
            FootstepController.HandleFootSteps(FootStepDelay);
        }
        else if (AnimatorClipInfo[0].clip.name == CHASE_ANIMATION_NAME)
        {
            FootstepController.HandleFootSteps(FootStepDelay / 2);
        }
    }

    private void HandleWeakness()
    {
        bool weaknessTrggered = IsVisible(gameObject) && flashLightController.IsOn && IsPlayerSpotted();

        if (weaknessTrggered)
        {
            elapsedTime = 0;

            if (canBeStunned)
            {
                canBeStunned = false;
                foxysEyesController.SetFoxyTriggeredEyes();
                SoundsController.PlayAngerSound();

                stateMachine.SwitchState(typeof(StunnedState));
            }
            else
            {
                if (AnimatorClipInfo[0].clip.name == RUN_ANIMATION)
                {
                    Animator.speed = foxyParameters.AngerMultiplier;
                    AnimatronicNavMeshController.SwitchAnimatronicMovement(true, MovementSpeed * RunningMultiplier * foxyParameters.AngerMultiplier);
                }
            }
        }
        else
        {
            elapsedTime+=Time.deltaTime;

            if (elapsedTime > foxyParameters.AngerDuration)
            {
                canBeStunned = true;
                foxysEyesController.SetFoxyCalmEyes();
                Animator.speed = 1;

                elapsedTime = 0;
            }
        }
    }
}
