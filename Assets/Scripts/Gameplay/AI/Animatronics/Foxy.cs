using Gameplay.Managers;
using System;
using System.Collections.Generic;
using UnityEngine;

public class Foxy : Animatronic
{
    [SerializeField]
    private FoxysEyesController foxysEyesController;

    private float elapsedTime = 0f;
    private bool isPlayerFlashlightOn = false;

    private void Start() => Initialize();

    private void Update()
    {
        HandleFootsteps();

        if (IsPlayerSpotted())
        {
            if (IsStunned) { return; }
            elapsedTime = 0f;
            if (IsVisible(gameObject) && isPlayerFlashlightOn)
            {
                if (!IsStunned)
                    StunAnimatronic();
            }
            else
            {
                StateMachine.SwitchState(typeof(ChaseState));
            }
        }
        else
        {
            elapsedTime += Time.deltaTime;
            if (elapsedTime > AnimatronicSettings.StunDuration)
            {
                ResetStunState();
            }
        }
    }

    private void Initialize()
    {
        SetStateMachine(GetComponent<StateMachine>());
        AssignSoundController(GetComponent<AnimatronicsSoundsController>());
        InitializeStateMachine();
        foxysEyesController.SetFoxyCalmEyes();
    }

    private void InitializeStateMachine()
    {
        StateMachine.SetStates(new Dictionary<Type, BaseState>
        {
            { typeof(DefaultState), new DefaultState(this) },
            { typeof(RoamingState), new RoamingState(this) },
            { typeof(IdleState), new IdleState(this) },
            { typeof(StunnedState), new StunnedState(this) },
            { typeof(ChaseState), new ChaseState(this) },
        });
    }

    public override bool IsPlayerSpotted() => fov.SeenPlayer != null;

    private void ResetStunState()
    {
        ToggleStun(false);
        foxysEyesController.SetFoxyCalmEyes();
        elapsedTime = 0f;
    }

    private void StunAnimatronic()
    {
        ToggleStun(true);
        StateMachine.SwitchState(typeof(StunnedState));
        foxysEyesController.SetFoxyAngerEyes();
        SoundsController.PlayAngerSound();
    }

    private void OnToggleFlashlight(bool isOn) => isPlayerFlashlightOn = isOn;

    private void OnEnable() => EventsManager.Instance.ToggleFlashlight += OnToggleFlashlight;
    private void OnDisable() => EventsManager.Instance.ToggleFlashlight -= OnToggleFlashlight;
}