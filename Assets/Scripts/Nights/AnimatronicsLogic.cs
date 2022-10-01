using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class AnimatronicsLogic : MonoBehaviour
{
    [Header("Animatronic")]
    [Space(10)]
    [SerializeField] private NavMeshAgent animatronic;
    [SerializeField] private Animator animator;
    [SerializeField] private GameController gameController;
    [SerializeField] private Vector3[] locations;

    [Header("Animatronic")]
    [Space(10)]
    [SerializeField] private Transform playerLocation;
    [SerializeField] private Transform playerCamera;

    [Space(10)]
    [Header("SFX")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip startUp;
    [SerializeField] private AudioClip scream;

    private Coroutine animatronicDestinationCoroutine;
    private bool isPlayerSpotted;
    private bool isAnimatronicOn;
    private bool haventScreamedYet = false;
    private AnimatorClipInfo[] animatorClipInfo;

    private void Start()
    {
        animatronic.enabled = false;
    }

    void FixedUpdate()
    {
        TurnOnAnimatronic();
        AnimatronicsAI();
    }

    private void AnimatronicsAI()
    {
        if (isAnimatronicOn)
        {
            IsInRange();
            animator.SetBool("isPlayerSpotted", isPlayerSpotted);
            animatronic.stoppingDistance = isPlayerSpotted ? 0f : 2f;
            animatorClipInfo = animator.GetCurrentAnimatorClipInfo(0);

            if (isPlayerSpotted)
            {
                PlayerIsSpotted();
            }

            if (animatronicDestinationCoroutine == null && !isPlayerSpotted)
            {
                PlayerIsNotSpotted();
            }
        }
    }

    private void PlayerIsSpotted()
    {
        if (animatorClipInfo[0].clip.name == "Scream")
        {
            animatronic.enabled = false;
            if (!audioSource.isPlaying && !haventScreamedYet)
            {
                audioSource.volume = 1f;
                audioSource.PlayOneShot(scream);
                haventScreamedYet = true;
            }
        }
        animatronicDestinationCoroutine = null;
        StopCoroutine(SetNewAnimatronicDestinationToCheck());
        animatronic.enabled = true;
        animatronic.SetDestination(playerLocation.position);
        if (animatorClipInfo[0].clip.name == "RunningAnimatronics")
        {
            animatronic.speed = 15f;
        }
    }

    private void PlayerIsNotSpotted()
    {
        if (animatorClipInfo[0].clip.name == "Idle")
        {
            animatronic.enabled = false;
        }
        haventScreamedYet = false;
        animatronicDestinationCoroutine = StartCoroutine(SetNewAnimatronicDestinationToCheck());
    }

    private void IsInRange()
    {
        float distance = Vector3.Distance(playerLocation.position, animatronic.transform.position);

        if (distance < 10f)
        {
            isPlayerSpotted = true;
        }
        if (distance > 15f)
        {
            isPlayerSpotted = false;
        }
    }

    private void TurnOnAnimatronic()
    {
        //TODO: Later in development change gameController.CurrentTime value to 60.
        if (gameController.CurrentTime == 5)
        {
            animator.SetBool("is2AM", true);
            if (!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(startUp);
            }
            isAnimatronicOn = true;
        }
    }

    IEnumerator SetNewAnimatronicDestinationToCheck()
    {
        yield return new WaitForSeconds(10f);
        animatronic.enabled = true;
        animatronic.speed = 0.3f;
        while (true)
        {
            yield return new WaitForSeconds(3f);
            if (!animatronic.pathPending)
            {
                if (animatronic.remainingDistance <= animatronic.stoppingDistance)
                {
                    if (!animatronic.hasPath || animatronic.velocity.sqrMagnitude < 2f)
                    {
                        animator.SetBool("reachedDestination", true);
                        animatronic.enabled = false;
                        yield return new WaitForSeconds(10f);
                        animator.SetBool("reachedDestination", false);
                        animatronic.enabled = true;
                        if (animatorClipInfo[0].clip.name != "Idle")
                        {
                            yield return new WaitForSeconds(3f);
                            animatronic.SetDestination(locations[Random.Range(0, 3)]);
                        }
                    }
                }
            }
        }
    }
}
