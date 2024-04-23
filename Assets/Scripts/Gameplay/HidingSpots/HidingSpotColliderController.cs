using Gameplay.Managers;
using UnityEngine;

public class HidingSpotColliderController : MonoBehaviour
{
    private const string PLAYER_TAG = "Player";

    [SerializeField]
    private HidingSpot hidingSpot;

    [SerializeField]
    private Vector3 animatronicPositionToCheckSpot;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(PLAYER_TAG))
        {
            hidingSpot.OnPlayerEnterTrigger(transform.position, animatronicPositionToCheckSpot);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(PLAYER_TAG))
        {
            hidingSpot.OnPlayerLeaveTrigger();
        }
    }
}