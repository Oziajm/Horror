using System;
using UnityEngine;

public class ChaseState : BaseState
{
    private readonly Animatronic animatronic;
    private readonly Transform playerTransform;
    public ChaseState(Animatronic animatronic) : base(animatronic.gameObject)
    {
        this.animatronic = animatronic;
        this.playerTransform = animatronic.player.GetComponent<Transform>();
    }

    public override Type Tick()
    {
        Debug.Log("ChaseState");

        animatronic.UpdateAnimatorName();

        if (!animatronic.IsPlayerSpotted()) 
            return typeof(RoamingState);

        if (animatronic.animatorClipInfo[0].clip.name == "Scream")
        {
            if (!animatronic.haveScreamedYet)
            {
                animatronic.navMeshAgent.enabled = false;
                animatronic.soundsController.PlayScream();
                animatronic.haveScreamedYet = true;
            }
        }

        animatronic.navMeshAgent.enabled = true;
        animatronic.navMeshAgent.SetDestination(playerTransform.position);

        if (animatronic.animatorClipInfo[0].clip.name == "RunningAnimatronics")
        {
            animatronic.navMeshAgent.speed = 15f;
        }
        return null;
    }

}
