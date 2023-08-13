using UnityEngine;
using System;

public class CharactersDetector : MonoBehaviour
{
    private const string CHARACTERS_TAG = "Characters";

    public Action<Vector3> characterEnternedCollider;
    public Action characterLeftCollider;

    private void OnTriggerEnter(Collider other)
    {
        if (CompareTag(CHARACTERS_TAG))
        {
            characterEnternedCollider?.Invoke(other.transform.position);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (CompareTag(CHARACTERS_TAG))
        {
            characterLeftCollider?.Invoke();
        }
    }
}
