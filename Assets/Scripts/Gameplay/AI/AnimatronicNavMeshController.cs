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
        navMeshAgent.acceleration = toggle ? 8 : -100;
    }

    public void SetNewDestination(Vector3 newLocation)
    {
        navMeshAgent.enabled = true;
        navMeshAgent.destination = newLocation;
    }

    public float GetRemaningDistance()
    {
        if (navMeshAgent.remainingDistance == 0 || !navMeshAgent.isActiveAndEnabled)
        {
            return 100;
        }

        return navMeshAgent.remainingDistance;
    }

    public bool IsAnimatronicMoving()
    {
        if (!navMeshAgent.isActiveAndEnabled) { return false; }

        return navMeshAgent.speed != 0;
    }
}
