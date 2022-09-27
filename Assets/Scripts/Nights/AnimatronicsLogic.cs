using UnityEngine;
using UnityEngine.AI;

public class AnimatronicsLogic : MonoBehaviour
{
    [SerializeField] private NavMeshAgent animatronic;
    [SerializeField] private Transform target;
    [SerializeField] private Animator animator;
    [SerializeField] private GameController gameController;

    private void Start()
    {
        
    }

    void FixedUpdate()
    {
        
        animatronic.SetDestination(target.position);
        float distance = Vector3.Distance(target.position, animatronic.transform.position);

        animator.SetBool("isClose", distance < 2.5f);
        if (distance < 2.5f)
        {
            animatronic.transform.LookAt(target);
        }
    }
}
