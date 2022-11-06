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
        if(startingOrEnding == StartingOrEnding.Starting)
        {
            pipeConnectedChecker.areStartingConnectors = true;
            pipeConnectedChecker.isPowered = true;
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
