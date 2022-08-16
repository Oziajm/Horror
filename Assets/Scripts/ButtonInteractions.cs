using UnityEngine;

public class ButtonInteractions : MonoBehaviour
{
    public bool isPressed = false;
    public bool isOpen = true;

    [SerializeField] private Transform door;
    [SerializeField] private Transform openedDoorLocation;
    [SerializeField] private Renderer button;
    [SerializeField] private BatteryController batteryController;
    
    private Vector3 oldPosition;

    private void Start()
    {
        oldPosition = door.position;
    }

    private void Update()
    {
        door.position = Vector3.MoveTowards(door.position, !isPressed ? openedDoorLocation.position : oldPosition, 5f * Time.deltaTime);
    }

    private void ChangeButtonsColor()
    {
        button.material.SetColor("_EmissionColor", !isPressed ? Color.green * 1f : Color.red * 1f);

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

        ChangeButtonsColor();
    }
}
