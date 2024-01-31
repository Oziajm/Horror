using System;
using System.Collections.Generic;

public class Endo : Animatronic
{
    public void Start()
    {
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
            {typeof(ChaseState), new ChaseState(this)}
        });
    }

    private void Update()
    {
        if (!IsVisible(gameObject))
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
    }

    public override bool IsPlayerSpotted()
    {
        return fov.CanSeePlayer;
    }
}
