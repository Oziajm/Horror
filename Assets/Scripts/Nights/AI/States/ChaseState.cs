using System;
using UnityEngine;

public class ChaseState : BaseState
{
    private Animatronic animatronic;
    private Transform playerTransform;
    public ChaseState(Animatronic animatronic) : base(animatronic.gameObject)
    {
        this.animatronic = animatronic;
        this.playerTransform = animatronic.player.GetComponent<Transform>();
    }

    public override Type Tick()
    {
        if (!animatronic.IsPlayerSpotted()) return typeof(RoamingState);

        if (animatronic.animatorClipInfo[0].clip.name == "Scream")
        {
            animatronic.enabled = false;
            animatronic.soundsController.PlayScream();
        }
        animatronic.enabled = true;
        animatronic.navMeshAgent.SetDestination(playerTransform.position);
        if (animatronic.animatorClipInfo[0].clip.name == "RunningAnimatronics")
        {
            animatronic.navMeshAgent.speed = 15f;
        }
        return null;
    }

}
