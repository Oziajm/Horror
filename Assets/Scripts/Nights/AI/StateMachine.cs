using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    private BaseState currentState;
    private Dictionary<Type, BaseState> states;
    private Type lastState;
    
    public event Action<BaseState> OnStateChanged;
    
    public void SetStates(Dictionary<Type, BaseState> states)
    {
        this.states = states;
    }

    void Update()
    {
        if(currentState == null)
        {
            currentState = states.Values.First();
            lastState = typeof(currentState);
        }

        var nextState = currentState.Tick();

        if (nextState != null && nextState != currentState?.GetType())
            SwitchState(nextState);
    }

    private void SwitchState(Type newState)
    {
        if(states[newState] != null)
        {
            lastState = typeof(currentState);
            currentState = states[newState];
            OnStateChanged?.Invoke(currentState);
        }
    }
}
