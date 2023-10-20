using UnityEngine;
using System;
using System.Collections.Generic;

public class CharactersDetector : MonoBehaviour
{
    private const string CHARACTERS_TAG = "Characters";

    [SerializeField]
    private bool isDoorFacingNorth;

    [SerializeField]
    private DoorsController doorsController;

    private bool isPlayerComingFromNorth;

    private List<Collider> charactersInTrigger;

    private void Awake()
    {
        charactersInTrigger = new List<Collider>();
    }

    private void Update()
    {
        Debug.Log(charactersInTrigger.Count);
    }

    private void OnTriggerEnter(Collider other)
    {
        if ( isDoorFacingNorth )
        {
            isPlayerComingFromNorth = other.transform.position.z - transform.position.z < 0;
        }
        else
        {
            isPlayerComingFromNorth = other.transform.position.x - transform.position.x > 0;
        }

        if (other.CompareTag(CHARACTERS_TAG))
        {
            doorsController?.OpenDoors(isPlayerComingFromNorth);
        }

        charactersInTrigger.Add(other);
    }

    private void OnTriggerExit(Collider other)
    {
        charactersInTrigger.Remove(other);

        if (other.CompareTag(CHARACTERS_TAG))
        {
            doorsController?.CloseDoors();
        }
    }
}
