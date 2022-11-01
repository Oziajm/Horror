using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    public BaseState CurrentState { get; private set; }
    private Dictionary<Type, BaseState> states;

    public event Action<BaseState> OnStateChanged;

    public void SetStates(Dictionary<Type, BaseState> states)
    {
        this.states = states;
        CurrentState = states.Values.First();
    }

    void Update()
    {
        if(CurrentState == null)
        {
            CurrentState = states.Values.First();
        }

        var nextState = CurrentState?.Tick();

        if (nextState != null && nextState != CurrentState?.GetType())
            SwitchState(nextState);
    }

    private void SwitchState(Type newState)
    {
        if(states[newState] != null)
            CurrentState = states[newState];
        OnStateChanged?.Invoke(CurrentState);
    }
}
