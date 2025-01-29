using UnityEngine;
using UnityEngine.AI;

public class AnimatronicNavMeshController : MonoBehaviour
{
    //REMEMBER TO ALWAYS ENABLE NAVMESHAGENT BEFORE USING ANY AVAIBLE IN THIS FILE METHODS

    [SerializeField]
    private NavMeshAgent navMeshAgent;

    public void SwitchAnimatronicMovement(bool toggle, float speed)
    {
        navMeshAgent.speed = speed;
        navMeshAgent.acceleration = toggle ? 8 : 0;
    }

    public void SetNewDestination(Vector3 newLocation)
    {
        navMeshAgent.destination = newLocation;
    }

    public float GetRemaningDistance()
    {
        return navMeshAgent.remainingDistance;
    }

    public float GetStoppingDistance()
    {
        return navMeshAgent.stoppingDistance;
    }
}
