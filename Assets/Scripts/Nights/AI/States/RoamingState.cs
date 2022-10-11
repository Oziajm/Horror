using System;
using System.Collections;
using UnityEngine;

public class RoamingState : BaseState
{
    private Animatronic animatronic;

    private WaitForSeconds animationDuration = new(10f);
    private WaitForSeconds waitForAnimationFinished = new(3f);

    private Coroutine animatronicDestinationCoroutine;


    public RoamingState(Animatronic animatronic) : base(animatronic.gameObject)
    {
        this.animatronic = animatronic;
    }

    public override Type Tick()
    {
        bool isPlayerSpotted = animatronic.IsPlayerSpotted();

        animatronic.animator.SetBool("isPlayerSpotted", isPlayerSpotted);
        animatronic.navMeshAgent.stoppingDistance = isPlayerSpotted ? 0f : 2f; // q

        if (isPlayerSpotted)
        {
            animatronicDestinationCoroutine = null;
            //StopCoroutine(SetNewAnimatronicDestinationToCheck());
            return typeof(ChaseState);
        }

        if (animatronicDestinationCoroutine == null)
        {
            OnPlayerIsNotSpotted();
        }

        return null;
    }
    private void OnPlayerIsNotSpotted()
    {
        if (animatronic.animatorClipInfo[0].clip.name == "Idle")
        {
            animatronic.enabled = false;
        }
        //animatronicDestinationCoroutine = StartCoroutine(SetNewAnimatronicDestinationToCheck());
    }

    IEnumerator SetNewAnimatronicDestinationToCheck()
    {
        yield return animationDuration;
        animatronic.enabled = true;
        animatronic.animator.speed = 0.3f;
        while (true)
        {
            yield return waitForAnimationFinished;
            if (!animatronic.navMeshAgent.pathPending)
            {
                if (animatronic.navMeshAgent.remainingDistance <= animatronic.navMeshAgent.stoppingDistance)
                {
                    if (!animatronic.navMeshAgent.hasPath || animatronic.navMeshAgent.velocity.sqrMagnitude < 2f)
                    {
                        animatronic.animator.SetBool("reachedDestination", true);
                        animatronic.enabled = false;
                        yield return animationDuration;
                        animatronic.animator.SetBool("reachedDestination", false);
                        animatronic.enabled = true;
                        if (animatronic.animatorClipInfo[0].clip.name != "Idle")
                        {
                            yield return waitForAnimationFinished;
                            animatronic.navMeshAgent.SetDestination(animatronic.patrolLocations[UnityEngine.Random.Range(0, 3)]);
                        }
                    }
                }
            }
        }
    }
}
