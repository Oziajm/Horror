using System;
using System.Collections.Generic;
using UnityEngine;

public class Foxy : Animatronic
{
    private readonly string WALK_ANIMATION_NAME = "WALK_ANIMATION";
    private readonly string RUN_ANIMATION_NAME = "RUN_ANIMATION";

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
        SetStateMachine(GetComponent<StateMachine>());
        InitializeStateMachine();
        AssignSoundController(GetComponent<AnimatronicsSoundsController>());
    }
    protected void InitializeStateMachine()
    {
        StateMachine.SetStates(new Dictionary<Type, BaseState>()
        {
            {typeof(DisabledState), new DisabledState(this)},
            {typeof(RoamingState), new RoamingState(this)},
            {typeof(IdleState), new IdleState(this)},
            {typeof(StunnedState), new StunnedState(this)},
            {typeof(ChaseState), new ChaseState(this)},
            {typeof(CheckHidingSpotState), new CheckHidingSpotState(this)},
        });
    }

    private void Update()
    {
        UpdateAnimatorName();
        HandleWeakness();
        HandleFootSteps();
    }

    public override bool IsPlayerSpotted()
    {
        return fov.CanSeePlayer;
    }

    private void HandleWeakness()
    {
        bool weaknessTriggered = IsVisible(gameObject) && flashLightController.IsOn && IsPlayerSpotted();

        if (weaknessTriggered)
        {
            elapsedTime = 0;

            if (canBeStunned)
                StunnAnimatronic();
            else
                MakeAnimatronicAngry();
        }
        else
        {
            if (canBeStunned) return;

            elapsedTime += Time.deltaTime;

            ResetIfNeeded();
        }
    }

    private void ResetIfNeeded()
    {
        if (elapsedTime > foxyParameters.AngerDuration)
        {
            canBeStunned = true;
            foxysEyesController.SetFoxyCalmEyes();
            Animator.speed = 1;

            elapsedTime = 0;
        }
    }

    private void StunnAnimatronic()
    {
        canBeStunned = false;
        foxysEyesController.SetFoxyTriggeredEyes();
        SoundsController.PlayAngerSound();

        StateMachine.SwitchState(typeof(StunnedState));
    }

    private void MakeAnimatronicAngry()
    {
        if (AnimatorClipInfo[0].clip.name == RUN_ANIMATION_NAME)
        {
            Animator.speed = foxyParameters.AngerMultiplier;
            AnimatronicNavMeshController.SwitchAnimatronicMovement(true, MovementSpeed * RunningMultiplier * foxyParameters.AngerMultiplier);
        }
    }

    private void HandleFootSteps()
    {
        if (AnimatorClipInfo[0].clip.name == WALK_ANIMATION_NAME)
        {
            FootstepController.HandleFootSteps(FootStepDelay);
        }
        else if (AnimatorClipInfo[0].clip.name == RUN_ANIMATION_NAME)
        {
            FootstepController.HandleFootSteps(FootStepDelay / 2);
        }
    }
}
