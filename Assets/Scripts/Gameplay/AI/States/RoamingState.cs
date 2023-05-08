using System;
using System.Collections;
using UnityEngine;
using Gameplay.Managers;

public class RoamingState : BaseState
{
    private Coroutine animatronicDestinationCoroutine;

    private readonly Animatronic animatronic;

    private readonly WaitForSeconds animationDuration = new(10f);
    private readonly WaitForSeconds waitForAnimationFinished = new(3f);

    public RoamingState(Animatronic animatronic) : base(animatronic.gameObject)
    {
        this.animatronic = animatronic;
    }

    public override Type Tick()
    {
        animatronic.UpdateAnimatorName();
        
        bool isPlayerSpotted = animatronic.IsPlayerSpotted();

        animatronic.animator.SetBool("isPlayerSpotted", isPlayerSpotted);
        animatronic.navMeshAgent.stoppingDistance = isPlayerSpotted ? 0f : 2f; // q

        if (isPlayerSpotted)
        {
            animatronicDestinationCoroutine = null;

            EventsManager.Instance.StopCoroutine(SetNewAnimatronicDestinationToCheck());

            return typeof(ChaseState);
        }

        if (animatronicDestinationCoroutine == null)
        {
            OnPlayerIsNotSpotted();
        }

        if (animatronic.IsVisible(animatronic.gameObject))
            return typeof(FrozenState);

        return null;
    }
    private void OnPlayerIsNotSpotted()
    {
        if (animatronic.animatorClipInfo[0].clip.name == "Idle")
        {
            animatronic.navMeshAgent.enabled = false;
        }
        animatronic.haveScreamedYet = false;

        animatronicDestinationCoroutine = EventsManager.Instance.StartCoroutine(SetNewAnimatronicDestinationToCheck());
    }

    private IEnumerator SetNewAnimatronicDestinationToCheck()
    {
        animatronic.UpdateAnimatorName();
        yield return animationDuration;
        animatronic.navMeshAgent.enabled = true;
        animatronic.navMeshAgent.speed = 0.3f;
        while (true)
        {
            if (animatronic.isActiveAndEnabled)
            {
                yield return waitForAnimationFinished;
                if (!animatronic.navMeshAgent.pathPending)
                {
                    if (animatronic.navMeshAgent.remainingDistance <= animatronic.navMeshAgent.stoppingDistance)
                    {
                        if (!animatronic.navMeshAgent.hasPath || animatronic.navMeshAgent.velocity.sqrMagnitude < 2f)
                        {
                            animatronic.animator.SetBool("reachedDestination", true);
                            animatronic.navMeshAgent.speed = 0;
                            yield return animationDuration;
                            animatronic.animator.SetBool("reachedDestination", false);
                            animatronic.navMeshAgent.speed = 0.3f;
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
}
