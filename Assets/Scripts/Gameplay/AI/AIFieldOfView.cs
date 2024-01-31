using UnityEngine;
using System.Collections;

public class AIFieldOfView : MonoBehaviour
{
    public bool CanSeePlayer { get; private set; }

    [SerializeField]
    private float radius;
    [Range(0,360)]
    [SerializeField]
    private float angle;

    [SerializeField]
    private GameObject player;

    [SerializeField]
    private LayerMask targetMask;
    [SerializeField]
    private LayerMask obstructionMask;

    private void Start()
    {
        StartCoroutine(FOVRoutine());
    }

    private IEnumerator FOVRoutine()
    {
        WaitForSeconds delay = new (0.2f);

        while (true)
        {
            yield return delay;
            FieldOfViewCheck();
        }
    }

    private void FieldOfViewCheck()
    {
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, radius, targetMask);

        if (rangeChecks.Length != 0)
        {
            Transform target = rangeChecks[0].transform;
            Vector3 directionToTarget = (target.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, directionToTarget) < angle / 2)
            {
                float disanceToTarget = Vector3.Distance(transform.position, target.position);

                if (!Physics.Raycast(transform.position, directionToTarget, disanceToTarget, obstructionMask))
                    CanSeePlayer = true;
                else
                    CanSeePlayer = false;
            }
            else
                CanSeePlayer = false;
        }
        else if (CanSeePlayer)
            CanSeePlayer = false;
    }

}
