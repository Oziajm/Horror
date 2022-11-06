using UnityEngine;
using UnityEngine.UI;

public class StartingEndingPipeController : MonoBehaviour
{
    public bool isPowered;

    [SerializeField] private Image image;

    [SerializeField] private StartingOrEnding startingOrEnding;
    [SerializeField] private PipeConnectedChecker pipeConnectedChecker;

    private void Start()
    {
        if((int)startingOrEnding == 0)
        {
            pipeConnectedChecker.areStartingConnectors = true;
            pipeConnectedChecker.isPowered = true;
        }
        else
        {
            ChangePipeColor();
        }
    }

    private void Update()
    {
        if((int)startingOrEnding == 1)
        {
            ChangePipeColor();
        }
    }

    public void ChangePipeColor()
    {
        if (pipeConnectedChecker.isPowered)
        {
            image.color = Color.cyan;
        }
        else
        {
            image.color = Color.white;
        }
    }
}



enum StartingOrEnding
{
    Starting,
    Ending
}
