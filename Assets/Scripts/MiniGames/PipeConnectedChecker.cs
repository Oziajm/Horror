using UnityEngine;

public class PipeConnectedChecker : MonoBehaviour
{
    public bool isPowered;
    public bool areStartingConnectors = false;

    private PipeConnectedChecker collidersPipeConnectedChecker;

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

    private void OnTriggerEnter(Collider other)
    {
        collidersPipeConnectedChecker = other.gameObject.GetComponent<PipeConnectedChecker>();
        if(!areStartingConnectors)
            isPowered = CheckIfConnectedPipesArePowered();
    }
}
