using UnityEngine;

public class DoorButton : Interactable
{
    public bool isPressed = false;
    public bool isOpen = true;

    [SerializeField] private Transform door;
    [SerializeField] private Transform openedDoorLocation;
    [SerializeField] private Renderer button;
    [SerializeField] private GameController batteryController;

    private Vector3 oldPosition;

    // Start is called before the first frame update
    void Start()
    {
        oldPosition = door.position;
    }

    // Update is called once per frame
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
