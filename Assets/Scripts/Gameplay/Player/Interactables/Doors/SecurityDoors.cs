using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SecurityDoors : Interactable
{
    private const float Y_VALUE_TO_GET_OPENED_POSITION = 2.25f;
    private const float POWER_CONSUMPTION_PER_SECOND = 10f;
    private const float MAX_BATTERY_AMOUNT = 100f;
    private const int NEW_DURATION = 1;

    [Header("Door Values")]
    [Space(10)]
    [SerializeField] private Transform door;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip audioClip;
    [SerializeField] private Light doorLight;

    private Coroutine doorAnimation;
    private Vector3 closedPos, openedPos;
    private Vector3 closedHandleRot, openedHandleRot;
    private float batteryAmount;
    private bool isOpen;

    private void Start()
    {
        InitializeDoorPositions();
        batteryAmount = MAX_BATTERY_AMOUNT;
        OpenCloseDoor();
    }

    private void InitializeDoorPositions()
    {
        closedPos = door.position;
        openedPos = closedPos + Vector3.up * Y_VALUE_TO_GET_OPENED_POSITION;

        openedHandleRot = transform.rotation.eulerAngles;
        closedHandleRot = openedHandleRot + Vector3.right * 70;
    }

    public override string GetHoverText() => batteryAmount > 0 ? "Use" : "IT'S ME";

    public override void Interact()
    {
        if (batteryAmount > 0)
            OpenCloseDoor();
    }

    private void OpenCloseDoor()
    {
        if (doorAnimation == null)
            doorAnimation = StartCoroutine(ChangeDoorPosition());

        if (!isOpen)
        {
            audioSource.PlayOneShot(audioClip);
            doorLight.enabled = true;

            StartCoroutine(ConsumePower());
        }
        else
        {
            doorLight.enabled = false;
            StopCoroutine(ConsumePower());
        }
    }

    private IEnumerator ConsumePower()
    {
        while (!isOpen && batteryAmount > 0)
        {
            batteryAmount -= POWER_CONSUMPTION_PER_SECOND * Time.deltaTime;
            yield return new WaitForSeconds(0.1f);
        }

        if (batteryAmount <= 0)
            OpenCloseDoor();
    }

    private IEnumerator ChangeDoorPosition()
    {
        Vector3 newPos = isOpen ? closedPos : openedPos;
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
}