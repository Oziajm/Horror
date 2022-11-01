using UnityEngine;
using UnityEngine.AI;

public abstract class Animatronic : MonoBehaviour
{
    public Vector3[] patrolLocations;
    public GameObject player;

    [SerializeField] private Camera c;

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

    public bool IsVisible(GameObject target)
    {
        var planes = GeometryUtility.CalculateFrustumPlanes(c);
        var point = target.transform.position;

        foreach (var plane in planes)
        {
            if (plane.GetDistanceToPoint(point) < 0)
            {
                return false;
            }
        }
        return true;
    }
    
    public void UndoState()
    {
        stateMachine.UndoState();
    }
}
