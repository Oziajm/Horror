using UnityEngine;
using System.Collections;
using Gameplay.Managers;

public class AIFieldOfView : MonoBehaviour
{
    public GameObject SeenPlayer { get; private set; }

    [field:SerializeField]
    public float Radius { get; private set; }
    [field:SerializeField]
    public float Angle { get; private set; }

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
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, Radius, targetMask);

        if (rangeChecks.Length != 0)
        {
            Transform target = rangeChecks[0].transform;
            Vector3 directionToTarget = (target.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, directionToTarget) < Angle / 2)
            {
                float distanceToTarget = Vector3.Distance(transform.position, target.position);

                if (!Physics.Raycast(transform.position, directionToTarget, out RaycastHit hit, distanceToTarget, obstructionMask))
                {
                    SeenPlayer = target.gameObject;
                }
            }
            else
            {
                SeenPlayer = null;
            }
        }
        else
        {
            SeenPlayer = null;
        }
    }
}
