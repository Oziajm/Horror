using UnityEngine;

public class ButtonInteractions : MonoBehaviour
{
    [SerializeField] private Transform door;
    [SerializeField] private Transform openedDoorLocation;
    [SerializeField] private Renderer button;
    [SerializeField] private BatteryController batteryController;
    private Vector3 oldPosition;
    public bool isPressed = false;
    public bool isOpen = true;

    private void Start()
    {
        oldPosition = door.position;
    }

    private void FixedUpdate()
    {
        if (!isPressed)
        {
            button.material.SetColor("_EmissionColor", Color.green * 1f);
            door.position = Vector3.MoveTowards(door.position, openedDoorLocation.position, 5f * Time.deltaTime);
        }
        else
        {
            door.position = Vector3.MoveTowards(door.position, oldPosition, 5f * Time.deltaTime);
            button.material.SetColor("_EmissionColor", Color.red * 1f);
        }

        isOpen = door.position == openedDoorLocation.position;
    }
    private void OnMouseDown()
    {
        if(door.position == openedDoorLocation.position || door.position == oldPosition)
        {
            isPressed = !isPressed;
        }
        if ((door.position == openedDoorLocation.position || door.position == oldPosition) && isPressed)
        {
            batteryController.usage += 1;
        }
        else if((door.position == openedDoorLocation.position || door.position == oldPosition) && !isPressed)
        {
            batteryController.usage -= 1;
        }
    }
}
