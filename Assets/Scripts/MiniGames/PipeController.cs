using UnityEngine;
using UnityEngine.UI;

public class PipeController : MonoBehaviour
{
    public bool isPipePowered = false;

    [SerializeField] private Image image;
    [SerializeField] private PipeConnectedChecker[] pipeConnectedCheckers;

    public void ChangePipeColor()
    {
        if (pipeConnectedCheckers[0].isPowered || pipeConnectedCheckers[1].isPowered)
        {
            isPipePowered = true;
            image.color = Color.cyan;
            ChangeIsPoweredValue(true);
        }
        else
        {
            isPipePowered = false;
            image.color = Color.white;
            ChangeIsPoweredValue(false);
        }
    }

    public void CheckIfConnected()
    {
        foreach(PipeConnectedChecker pipeConnectedChecker in pipeConnectedCheckers)
        {
            ChangeIsPoweredValue(false);
            pipeConnectedChecker.collidersPipeConnectedChecker = null;
            pipeConnectedChecker.ChangePipesParameters();
        }
    }

    private void ChangeIsPoweredValue(bool isPowered)
    {
        pipeConnectedCheckers[0].isPowered = isPowered;
        pipeConnectedCheckers[1].isPowered = isPowered;
    }
}
