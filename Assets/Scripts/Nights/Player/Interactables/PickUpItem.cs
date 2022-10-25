using UnityEngine;

public class PickUpItem : Interactable
{
    [SerializeField] private Key keyItem;

    public override void Interact()
    {
        TakeItem();
    }
    public override string GetHoverText()
    {
        return "Take";
    }

    private void TakeItem()
    {
        EventManager.current.PickUpItem(keyItem);
        Destroy(gameObject);
    }
}