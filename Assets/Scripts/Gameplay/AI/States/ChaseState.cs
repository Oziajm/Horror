using System;
using UnityEngine;

public class ChaseState : BaseState
{
    private readonly Animatronic animatronic;
    private readonly Transform playerTransform;

    float elapsedTime = 0f;
    public ChaseState(Animatronic animatronic) : base(animatronic.gameObject)
    {
        this.animatronic = animatronic;
        this.playerTransform = animatronic.player.GetComponent<Transform>();
    }

    public override Type Tick()
    {
        animatronic.UpdateAnimatorName();

        if (animatronic.IsVisible(animatronic.gameObject))
            return typeof(FrozenState);

        ChaseSequence();

        return null;
    }

    private Type ChaseSequence()
    {
        elapsedTime += 1f * Time.deltaTime;
        if (!animatronic.IsPlayerSpotted() && elapsedTime > 5f)
        {
            elapsedTime = 0f;
            return typeof(RoamingState);
        }

        if (animatronic.IsPlayerSpotted())
            elapsedTime = 0f;

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
