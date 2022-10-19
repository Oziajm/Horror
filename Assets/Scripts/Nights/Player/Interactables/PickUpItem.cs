public class PickUpItem : Interactable
{
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

    }
}