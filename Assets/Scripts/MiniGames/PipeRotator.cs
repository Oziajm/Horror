using UnityEngine;
public class PipeRotator : Interactable
{
    public override void Interact()
    {
        RotatePipes();
    }
    public override string GetHoverText()
    {
        return "Rotate";
    }

    private void RotatePipes()
    {
        transform.Rotate(0, 0, 90);
    }
}