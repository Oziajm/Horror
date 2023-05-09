using System.Collections;
using UnityEngine;

public class Door : Interactable
{
    #region Variables

    private const float NEW_DURATION = 1f;

    [Header("Door Rotations")]
    [Space(10)]
    [SerializeField] 
    private Vector3 closedRotation;
    [SerializeField] 
    private Vector3 openRotation;

    private bool isOpen;
    private Coroutine doorAnimation;
    private Quaternion startRot;

    #endregion

    #region Unity Methods

    private void Start()
    {
        startRot = transform.rotation;
        transform.rotation = Quaternion.Euler(closedRotation);
    }

    #endregion

    #region Public Methods

    public override void Interact()
    {
        OpenCloseDoor();
    }

    public override string GetHoverText()
    {
        return isOpen ? "Close" : "Open";
    }

    #endregion

    #region Private Methods

    private void OpenCloseDoor()
    {
        if (doorAnimation == null)
        {
            doorAnimation = StartCoroutine(RotateDoors());
        }
    }

    private IEnumerator RotateDoors()
    {
        Quaternion endRot = Quaternion.Euler(isOpen ? closedRotation : openRotation);

        float newTime = 0;
 
        while (newTime < NEW_DURATION)
        {
            transform.rotation = Quaternion.Slerp(startRot, endRot, newTime / NEW_DURATION);
            yield return null;
            newTime += Time.deltaTime;
        }

        transform.rotation = endRot;

        isOpen = !isOpen;

        doorAnimation = null;
    }

    #endregion
}