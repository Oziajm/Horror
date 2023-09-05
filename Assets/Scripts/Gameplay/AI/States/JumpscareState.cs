using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpscareState : MonoBehaviour
{
    private readonly string CHARACTERS_TAG = "Characters";

    [SerializeField]
    private GameObject animatronic;
    [SerializeField]
    private GameObject animatronicJumpscare;
    [SerializeField]
    private Transform animatronicJumpscareHead;

    private JumpscaresController jumpscaresController;

    private void Update()
    {
        transform.position = animatronic.transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag(CHARACTERS_TAG))
        {
            animatronic.SetActive(false);
            animatronicJumpscare.SetActive(true);

            SetAnimatronicPosition(other.transform);

            jumpscaresController = other.GetComponent<JumpscaresController>();
            jumpscaresController.OnJumpscareTriggered(animatronicJumpscareHead);
        }
    }

    private void SetAnimatronicPosition(Transform player)
    {
        Vector3 animatronicOffset = player.forward * 0.3f - player.up;

        animatronicJumpscare.transform.position = player.position + animatronicOffset;
        animatronicJumpscare.transform.LookAt(player.position - player.up);
    }
}
