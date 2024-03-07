using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class Animatronic : MonoBehaviour
{
    protected readonly string WALK_ANIMATION_NAME = "WALK_ANIMATION";
    protected readonly string CHASE_ANIMATION_NAME = "RUN_ANIMATION";

    [field:SerializeField]
    public List<Vector3> PatrolLocations { get; private set; }
    [field: SerializeField]
    public GameObject Player { get; private set; }
    [field: SerializeField]
    public float MovementSpeed { get; private set; }
    [field: SerializeField]
    public float FootStepDelay { get; private set; }
    [field: SerializeField]
    public AnimatronicsSoundsController SoundsController { get; private set; }
    [field: SerializeField]
    public AnimatronicNavMeshController AnimatronicNavMeshController { get; private set; }
    [field: SerializeField]
    public Animator Animator { get; private set; }
    [field: SerializeField]
    public FootstepController FootstepController { get; private set; }
    public float RunningMultiplier { get; private set; } = 2.5f;
    public AnimatorClipInfo[] AnimatorClipInfo { get; private set; }
    public StateMachine StateMachine { get; private set; }
    public HidingSpot HidingSpotToCheck { get; private set; }

    [SerializeField]
    private Camera playerCamera;
    [SerializeField]
    protected AIFieldOfView fov;
    [SerializeField]
    private bool isOn;

    public abstract bool IsPlayerSpotted();

    private void Update()
    {
        UpdateAnimatorName();
    }

    public void UpdateAnimatorName()
    {
        AnimatorClipInfo = Animator.GetCurrentAnimatorClipInfo(0); 
    }

    public bool IsVisible(GameObject target)
    {
        var planes = GeometryUtility.CalculateFrustumPlanes(playerCamera);
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

    public void AssignSoundController(AnimatronicsSoundsController soundController)
    {
        this.SoundsController = soundController;
    }

    public void SetStateMachine(StateMachine stateMachine)
    {
        this.StateMachine = stateMachine;
    }

    public void SetNewHidingSpotToCheck(HidingSpot hidingSpotToCheck)
    {
        this.HidingSpotToCheck = hidingSpotToCheck;
    }
}
