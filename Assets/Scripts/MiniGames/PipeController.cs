using UnityEngine;
using UnityEngine.UI;

public class PipeController : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private PipeConnectedChecker[] pipeConnectedCheckers;

    public void ChangePipeColor()
    {
        if(pipeConnectedCheckers[0].isPowered || pipeConnectedCheckers[1].isPowered)
        {
            image.color = Color.cyan;
        }
        else
        {
            image.color = Color.white;
        }
    }
}
