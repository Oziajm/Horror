using System.Collections;
using UnityEngine;

public class Door : Interactable
{
    #region Variables

    [Header("Door Values")]
    [Space(10)]
    [SerializeField] private Vector3 closedRotation;
    [SerializeField] private Vector3 openRotation;

    private bool isOpen = false;
    private Coroutine doorAnimation;

    #endregion

    #region Public Methods

    public override void Interact()
    {
        OpenCloseDoor();
    }
    public override string GetHoverText()
    {
        return isOpen ? "Press E to Close" : "Press E to Open";
    }

    #endregion

    #region Private Methods

    private void OpenCloseDoor()
    {
        if (doorAnimation == null)
        {
            doorAnimation = StartCoroutine(DoDoorRotation());
        }
    }

    IEnumerator DoDoorRotation()
    {
        Quaternion startRot = transform.rotation;
        Quaternion endRot = Quaternion.Euler(isOpen ? closedRotation : openRotation);

        isOpen = !isOpen;

        float newDuration = 1;
        float newTime = 0;
        while (newTime < newDuration)
        {
            transform.rotation = Quaternion.Slerp(startRot, endRot, newTime / newDuration);
            yield return null;
            newTime += Time.deltaTime;
        }
        doorAnimation = null;
    }

    #endregion
}