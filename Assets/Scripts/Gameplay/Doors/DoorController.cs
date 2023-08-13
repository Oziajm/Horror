using System.Collections;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    #region Variables

    private const float OPENING_DURATION = 1.5f;

    [Header("Door Rotations")]
    [Space(10)]
    [SerializeField] 
    private Quaternion openedRotationSouth;
    [SerializeField]
    private Vector3 openedPositionSouth;
    [Space(10)]
    [SerializeField]
    private Quaternion openedRotationNorth;
    [SerializeField]
    private Vector3 openedPositionNorth;
    [Space(10)]
    [SerializeField]
    private CharactersDetector charactersDetector;

    private Quaternion closedRotation;
    private Vector3 closedPosition;

    #endregion

    #region Unity Methods

    private void Awake()
    {
        closedRotation = transform.rotation;
        closedPosition = transform.position;

        charactersDetector.characterEnternedCollider += OpenDoors;
        charactersDetector.characterLeftCollider += CloseDoors;
    }

    #endregion

    #region Private Methods

    private void OpenDoors(Vector3 position)
    {
        if (position.x >= 0)
        {
            StartCoroutine(RotateDoors(openedPositionSouth, openedRotationSouth));
        }
        else
        {
            StartCoroutine(RotateDoors(openedPositionNorth, openedRotationNorth));
        }
    }

    private void CloseDoors()
    {
        StartCoroutine(RotateDoors(closedPosition, closedRotation));
    }

    private IEnumerator RotateDoors(Vector3 targetPosition, Quaternion targetRotation)
    {
        float newTime = 0;

        while (newTime < OPENING_DURATION)
        {
            transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation, newTime / OPENING_DURATION);
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, targetPosition, newTime / OPENING_DURATION);
            yield return null;
            newTime += Time.deltaTime;
        }
    }

    #endregion
}