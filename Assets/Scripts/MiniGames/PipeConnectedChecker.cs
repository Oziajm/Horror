using UnityEngine;

public class PipeConnectedChecker : MonoBehaviour
{
    public bool isPowered;
    public bool areStartingConnectors = false;
    public PipeConnectedChecker collidersPipeConnectedChecker;

    [SerializeField] private PipeController pipeController;

    private void OnTriggerStay(Collider other)
    {
        collidersPipeConnectedChecker = other.gameObject.GetComponent<PipeConnectedChecker>();
        ChangePipesParameters();
    }

    private void OnTriggerExit(Collider other)
    {
        collidersPipeConnectedChecker = null;
        ChangePipesParameters();
    }

    public void ChangePipesParameters()
    {
        if (!areStartingConnectors)
            isPowered = CheckIfConnectedPipesArePowered();
        if(pipeController != null)
            pipeController.ChangePipeColor();
    }

    private bool CheckIfConnectedPipesArePowered()
    {
        if (collidersPipeConnectedChecker != null)
        {
            return collidersPipeConnectedChecker.isPowered;
        }
        else
        {
            return false;
        }
    }
}
