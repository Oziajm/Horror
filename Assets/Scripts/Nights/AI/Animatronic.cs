using UnityEngine;
using UnityEngine.AI;

public abstract class Animatronic : MonoBehaviour
{
    [SerializeField] public Vector3[] patrolLocations;
    [SerializeField] public GameObject player;

    [SerializeField] protected AIFieldOfView fov;
    
    [HideInInspector] public AnimatorClipInfo[] animatorClipInfo;
    [HideInInspector] public AnimatronicsSoundsController soundsController;

    public NavMeshAgent navMeshAgent;
    public Animator animator;

    protected StateMachine stateMachine;

    public bool isOn = false;

    public bool haveScreamedYet = false;

    public abstract bool IsPlayerSpotted();

    public void UpdateAnimatorName()
    {
        animatorClipInfo = animator.GetCurrentAnimatorClipInfo(0);
    }
}
