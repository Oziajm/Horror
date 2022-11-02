using UnityEngine;


public class AIStateVisualizer : MonoBehaviour
{
    [SerializeField] private Vector3 offset = Vector3.up;

    private StateMachine[] stateMachines;
    private GUIStyle style;

    private void Start()
    {
        style = new GUIStyle();
        style.normal.textColor = Color.green;
        style.fontSize = 40;
    }

    private void OnDrawGizmos()
    {
        if (stateMachines == null)
            stateMachines = FindObjectsOfType<StateMachine>();
       
        foreach (var stateMachine in stateMachines)
        {
            if(stateMachine.transform.position != null)
                UnityEditor.Handles.Label(stateMachine.transform.position + offset, stateMachine.CurrentState?.ToString(), style);
        }
    }
}
