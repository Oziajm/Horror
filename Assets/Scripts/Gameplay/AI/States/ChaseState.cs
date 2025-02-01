using Gameplay.Managers;
using System;
using System.Collections;
using UnityEngine;

public class ChaseState : BaseState
{
    private readonly float SECONDS_NEEDED_TO_RETURN_TO_ROAMING = 15f;
    private readonly float DELAY_BEFORE_CHANGING_ANIMATION = 0.1f;

    private readonly Animatronic ANIMATRONIC;

    private float elapsedTime;
    private GameObject targetPlayer;
    private bool isAnimationFinished;

    public ChaseState(Animatronic animatronic) : base(animatronic.gameObject)
    {
        this.ANIMATRONIC = animatronic;
    }

    public override void Initialize()
    {
        elapsedTime = 0f;
        isAnimationFinished = false;
        EventsManager.Instance.PlayerSpotted?.Invoke();
        targetPlayer = ANIMATRONIC.GetTargetPlayer();
        ANIMATRONIC.Animator.CrossFade(StringsManager.Instance.RUN_ANIMATION_NAME, DELAY_BEFORE_CHANGING_ANIMATION);
        ANIMATRONIC.StartCoroutine(WaitForAnimationToEnd());
    }

    private IEnumerator WaitForAnimationToEnd()
    {
        yield return new WaitForSeconds(DELAY_BEFORE_CHANGING_ANIMATION);
        isAnimationFinished = true;
        ANIMATRONIC.AnimatronicNavMeshController.SwitchAnimatronicMovement(true, ANIMATRONIC.AnimatronicSettings.MovementSpeed * ANIMATRONIC.AnimatronicSettings.RunningMultiplier);
    }

    public override Type Tick()
    {
        if (!isAnimationFinished)
            return null;

        ANIMATRONIC.AnimatronicNavMeshController.SetNewDestination(targetPlayer.transform.position);

        if (ANIMATRONIC.IsPlayerSpotted())
        {
            elapsedTime = 0f;
        }
        else
        {
            elapsedTime += Time.deltaTime;

            if (elapsedTime > SECONDS_NEEDED_TO_RETURN_TO_ROAMING)
            {
                EventsManager.Instance.PlayerOutOfSight?.Invoke();
                return typeof(IdleState);
            }
        }

        return null;
    }
}