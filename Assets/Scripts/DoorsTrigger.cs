using UnityEngine;
using UnityEngine.AI;

public class DoorsTrigger : MonoBehaviour
{
    [SerializeField] private NavMeshAgent animatronic;
    [SerializeField] private ButtonInteractions door;
    [SerializeField] private Animator animator;

    private void OnTriggerEnter(Collider other)
    {
        animatronic.enabled = !door.isOpen;
        animator.SetBool("isClose", !door.isOpen);
    }
}
