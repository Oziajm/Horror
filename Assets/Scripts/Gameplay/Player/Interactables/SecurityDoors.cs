using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SecurityDoors : Interactable
{
    #region Variables

    private const float Y_VALUE_TO_GET_OPENED_POSITION = 2.1f;

    private const float POWER_CONSUMPTION_PER_SECOND = 10f;
    private const float MAX_BATTERY_AMOUNT = 100f;

    private const int NEW_DURATION = 1;

    [Header("Door Values")]
    [Space(10)]
    [SerializeField] 
    private Transform door;
    [SerializeField] 
    private Image filledImage;

    private Coroutine doorAnimation;

    private Vector3 closedPos;
    private Vector3 openedPos;

    Vector3 closedHandleRot = new (0, 0, 60);
    Vector3 openedHandleRot = new (0, 0, -60);

    private float batteryAmount;

    private bool isOpen;

    #endregion

    #region Unity Methods

    private void Start()
    {
        closedPos = door.position;
        openedPos = door.position;
        openedPos.y += Y_VALUE_TO_GET_OPENED_POSITION;

        batteryAmount = MAX_BATTERY_AMOUNT;

        OpenCloseDoor();
    }

    #endregion

    #region Public Methods

    public override string GetHoverText()
    {
        return batteryAmount > 0 ? "Use" : "IT'S ME";
    }

    public override void Interact()
    {
        if (batteryAmount > 0)
            OpenCloseDoor();
    }

    #endregion

    #region Private Methods

    private void OpenCloseDoor()
    {
        if (doorAnimation == null)
        {
            doorAnimation = StartCoroutine(ChangeDoorPosition());
        }

        if (!isOpen)
        {
            StartCoroutine(ConsumePower());
        }
        else
        {
            StopCoroutine(ConsumePower());
        }
    }

    private IEnumerator ConsumePower()
    {
        while (!isOpen)
        {
            if (batteryAmount > 0)
            {
                batteryAmount -= POWER_CONSUMPTION_PER_SECOND * Time.deltaTime;
            } 
            else
            {
                OpenCloseDoor();
            }

            filledImage.fillAmount = batteryAmount / MAX_BATTERY_AMOUNT;

            yield return new WaitForSeconds(0.1f);
        }
    }

    private IEnumerator ChangeDoorPosition()
    {
        Vector3 newPos = (isOpen ? closedPos : openedPos);
        Quaternion newRot = Quaternion.Euler(isOpen ? closedHandleRot : openedHandleRot);

        isOpen = !isOpen;

        float newTime = 0;

        while (newTime < NEW_DURATION)
        {
            transform.localRotation = Quaternion.Slerp(transform.localRotation, newRot, newTime / NEW_DURATION);
            door.position = Vector3.MoveTowards(door.position, newPos, newTime / NEW_DURATION);
            yield return null;
            newTime += Time.deltaTime;
        }

        doorAnimation = null;
    }

    #endregion 
}
