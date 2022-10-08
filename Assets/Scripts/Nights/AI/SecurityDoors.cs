using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SecurityDoors : Interactable
{
    #region Variables

    [Header("Door Values")]
    [Space(10)]
    [SerializeField] private Transform door;
    [SerializeField] private float batteryAmmount;
    [SerializeField] private Image filledImage;

    private Coroutine doorAnimation = null;
    private Coroutine powerConsumption = null;

    private Vector3 closedPos;
    private Vector3 openedPos;

    Vector3 closedRot = new (0, 0, 60);
    Vector3 openedRot = new (0, 0, -60);

    private float maxBatteryAmmount;

    private bool isOpen = false;

    #endregion

    #region Unity Methods

    private void Start()
    {
        closedPos = door.position;
        openedPos = door.position;
        openedPos.y += 2.1f;

        maxBatteryAmmount = batteryAmmount;

        OpenCloseDoor();
    }

    #endregion

    #region Public Methods

    public override string GetHoverText()
    {
        return batteryAmmount > 0 ? "Press E to Use" : "IT'S ME";
    }

    public override void Interact()
    {
        if (batteryAmmount > 0)
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

        if (!isOpen && powerConsumption == null) {
            powerConsumption = StartCoroutine(ConsumePower());
        } else if (isOpen)
        {
            powerConsumption = null;
        }

    }

    IEnumerator ConsumePower()
    {
        while (!isOpen)
        {
            if (batteryAmmount > 0)
            {
                batteryAmmount -= 100f * Time.deltaTime;
            } else
            {
                OpenCloseDoor();
                isOpen = true;
            }  

            filledImage.fillAmount = batteryAmmount / maxBatteryAmmount;

            yield return new WaitForSeconds(1f);
        }
    }

    IEnumerator ChangeDoorPosition()
    {
        Vector3 newPos = (isOpen ? closedPos : openedPos);
        Quaternion newRot = Quaternion.Euler(isOpen ? closedRot : openedRot);

        isOpen = !isOpen;

        float newDuration = 1;
        float newTime = 0;
        while (newTime < newDuration)
        {
            transform.localRotation = Quaternion.Slerp(transform.localRotation, newRot, newTime / newDuration);
            door.position = Vector3.MoveTowards(door.position, newPos, newTime / newDuration);
            yield return null;
            newTime += Time.deltaTime;
        }
        doorAnimation = null;
    }

    #endregion 
}
