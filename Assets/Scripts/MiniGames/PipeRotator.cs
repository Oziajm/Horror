using UnityEngine;
public class PipeRotator : Interactable
{
    [SerializeField] private PipeController pipeController;

    private void Start()
    {
        int randomNumber = Random.Range(0, 4);
        transform.Rotate(0, 0, randomNumber * 90);
    }

    public override void Interact()
    {
        RotatePipes();
        CheckIfConnected();
    }
    public override string GetHoverText()
    {
        return "Rotate";
    }

    private void RotatePipes()
    {
        transform.Rotate(0, 0, 90);
    }

    private void CheckIfConnected()
    {
        pipeController.CheckIfConnected();
    }
}