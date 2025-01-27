using UnityEngine;
using UnityEngine.AI;

public class AnimatronicNavMeshController : MonoBehaviour
{
    //REMEMBER TO ALWAYS ENABLE NAVMESHAGENT BEFORE USING ANY AVAIBLE IN THIS FILE METHODS

    [SerializeField]
    private NavMeshAgent navMeshAgent;

    public void SwitchAnimatronicMovement(bool toggle, float speed)
    {
        navMeshAgent.enabled = toggle;
        navMeshAgent.speed = speed;
        navMeshAgent.acceleration = toggle ? 8 : 0;
    }

    public void SetNewDestination(Vector3 newLocation)
    {
        if (navMeshAgent.enabled)
            navMeshAgent.destination = newLocation;
    }

    public float GetRemaningDistance()
    {
        if (!navMeshAgent.enabled)
            return 0;

        return navMeshAgent.remainingDistance;
    }

    public float GetStoppingDistance()
    {
        if (!navMeshAgent.enabled)
            return 0;

        return navMeshAgent.stoppingDistance;
    }
}
