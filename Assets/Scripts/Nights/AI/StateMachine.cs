using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    private BaseState currentState;
    private Dictionary<Type, BaseState> states;

    private event Action<BaseState> onStateChanged;

    public void SetStates(Dictionary<Type, BaseState> states)
    {
        this.states = states;
    }

    void Update()
    {
        if(currentState == null)
        {
            currentState = states.Values.First();
        }
        Debug.Log(currentState.GetType());
        var nextState = currentState.Tick();

        if (nextState != null && nextState != currentState?.GetType())
            SwitchState(nextState);
    }

    private void SwitchState(Type newState)
    {
        currentState = states[newState];
        onStateChanged?.Invoke(currentState);
    }
}
