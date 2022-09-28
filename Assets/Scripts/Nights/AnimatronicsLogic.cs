using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class AnimatronicsLogic : MonoBehaviour
{
    [SerializeField] private NavMeshAgent animatronic;
    [SerializeField] private Transform target;
    [SerializeField] private Animator animator;
    [SerializeField] private GameController gameController;
    [SerializeField] private Vector3[] locations;

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
        if (isAnimatronicOn)
        {
            float distance = Vector3.Distance(target.position, animatronic.transform.position);
            if (distance < 10f)
            {
                animatronic.enabled = false;
                isPlayerSpotted = true;
                animatorClipInfo = animator.GetCurrentAnimatorClipInfo(0);
                if (animatorClipInfo[0].clip.name == "Scream")
                {
                    animatronic.enabled = false;
                    if (!audioSource.isPlaying && !haventScreamedYet)
                    {
                        audioSource.PlayOneShot(scream);
                        haventScreamedYet = true;
                    }
                }
            }
            if (distance > 15f)
            {
                isPlayerSpotted = false;
                haventScreamedYet = false;
                animatorClipInfo = animator.GetCurrentAnimatorClipInfo(0);
                if (animatorClipInfo[0].clip.name == "Idle")
                {
                    animatronic.enabled = false;
                }
            }
            animator.SetBool("isPlayerSpotted", isPlayerSpotted);

            if (isPlayerSpotted)
            {
                animatronicDestinationCoroutine = null;
                StopCoroutine(SetNewAnimatronicDestination());
                animatronic.enabled = true;
                animatronic.SetDestination(target.position);
                animatronic.speed = 8f;
            }
            else if (animatronicDestinationCoroutine == null && !isPlayerSpotted)
            {
                animatronicDestinationCoroutine = StartCoroutine(SetNewAnimatronicDestination());
            }
        }

        if (gameController.CurrentTime == 5)
        {
            animator.SetBool("is2AM", true);
            if (!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(startUp);
            }
            isAnimatronicOn = true;
        }
        else
        {
            return;
        }
    }

    IEnumerator SetNewAnimatronicDestination()
    {
        yield return new WaitForSeconds(10f);
        animatronic.enabled = true;
        animatronic.SetDestination(locations[Random.Range(0, 3)]);
        while (true)
        {
            animatronic.enabled = true;
            yield return new WaitForSeconds(3f);
            if (!animatronic.pathPending)
            {
                if (animatronic.remainingDistance <= animatronic.stoppingDistance)
                {
                    if (!animatronic.hasPath || animatronic.velocity.sqrMagnitude < 1f)
                    {
                        animator.SetBool("reachedDestination", true);
                        yield return new WaitForSeconds(10f);
                        animatronic.SetDestination(locations[Random.Range(0, 3)]);
                        animator.SetBool("reachedDestination", false);
                    }
                }
            }
        }
    }
}
