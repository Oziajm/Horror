using UnityEngine;

public class SecurityDoors : Interactable
{
    [Space(10)]
    [Header("Public Values")]
    [Space(10)]
    public bool isPressed = false;
    public bool isOpen = true;

    [Space(10)]
    [Header("Door Values")]
    [Space(10)]
    [SerializeField] private Transform door;
    [SerializeField] private Transform openedDoorLocation;
    [SerializeField] private Renderer button;
    [SerializeField] private GameController batteryController;

    private Vector3 oldPosition;

    void Start()
    {
        oldPosition = door.position;
    }

    void Update()
    {
        door.position = Vector3.MoveTowards(door.position, !isPressed ? openedDoorLocation.position : oldPosition, 5f * Time.deltaTime);
        isOpen = door.position == openedDoorLocation.position;
    }

    public override string GetHoverText()
    {
        return "Press E to Use";
    }

    public override void Interact()
    {
        if (door.position == openedDoorLocation.position || door.position == oldPosition)
        {
            isPressed = !isPressed;
        }

        if ((door.position == openedDoorLocation.position || door.position == oldPosition) && isPressed)
        {
            batteryController.usage += 1;
        }
        else if ((door.position == openedDoorLocation.position || door.position == oldPosition) && !isPressed)
        {
            batteryController.usage -= 1;
        }

        ChangeButtonsColor();
    }

    public void ChangeButtonsColor()
    {
        button.material.SetColor("_EmissionColor", !isPressed ? Color.green * 1f : Color.red * 1f);
    }
}
