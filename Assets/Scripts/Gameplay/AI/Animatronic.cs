using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class Animatronic : MonoBehaviour
{
    [field:SerializeField]
    public List<Vector3> PatrolLocations { get; private set; }
    [field: SerializeField]
    public AnimatronicsSoundsController SoundsController { get; private set; }
    [field: SerializeField]
    public AnimatronicNavMeshController AnimatronicNavMeshController { get; private set; }
    [field: SerializeField]
    public FootstepController FootstepController { get; private set; }
    [field: SerializeField]
    public AnimatronicSettings AnimatronicSettings { get; private set; }
    [field: SerializeField]
    public Animator Animator { get; private set; }

    public StateMachine StateMachine { get; private set; }
    public bool IsStunned { get; private set; }
    public bool isMoving => AnimatronicNavMeshController.IsAnimatronicMoving();

    [SerializeField]
    protected AIFieldOfView fov;
    protected bool playerSpotted = false;

    private Camera playerCamera;

    private void Awake()
    {
        playerCamera = Camera.main;
    }

    public abstract bool IsPlayerSpotted();

    protected bool IsVisible(GameObject target)
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

    protected void HandleFootsteps()
    {
        if (!isMoving) return;

        float stepDelay = StateMachine.CurrentState is ChaseState
            ? AnimatronicSettings.FootStepDelay / 2
            : AnimatronicSettings.FootStepDelay;

        FootstepController.HandleFootSteps(stepDelay);
    }

    public GameObject GetTargetPlayer()
    {
        return fov.SeenPlayer;
    }

    public void AssignSoundController(AnimatronicsSoundsController soundController)
    {
        this.SoundsController = soundController;
    }

    public void SetStateMachine(StateMachine stateMachine)
    {
        this.StateMachine = stateMachine;
    }

    public void ToggleStun(bool isStunned)
    {
        this.IsStunned = isStunned;
    }
}
