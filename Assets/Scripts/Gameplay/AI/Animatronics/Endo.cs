using System;
using System.Collections.Generic;

public class Endo : Animatronic
{
    public void Start()
    {
        SetStateMachine(GetComponent<StateMachine>());
        InitializeStateMachine();
        AssignSoundController(GetComponent<AnimatronicsSoundsController>());
    }
    protected void InitializeStateMachine()
    {
        StateMachine.SetStates(new Dictionary<Type, BaseState>()
        {
            {typeof(RoamingState), new RoamingState(this)},
            {typeof(IdleState), new IdleState(this)},
            {typeof(ChaseState), new ChaseState(this)}
        });
    }

    private void Update()
    {
        FreezeIfNeeded();
    }

    private void FreezeIfNeeded()
    {
        bool shouldBeActive = !IsVisible(gameObject);

        Animator.enabled = shouldBeActive;
        AnimatronicNavMeshController.SwitchAnimatronicMovement(shouldBeActive, shouldBeActive ? AnimatronicSettings.MovementSpeed : 0);

        //TODO: Zablokowac kroki
    }

    public override bool IsPlayerSpotted()
    {
        return fov.SeenPlayer != null;
    }
}
