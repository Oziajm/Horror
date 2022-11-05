using UnityEngine;
using UnityEngine.UI;

public class PipeController : MonoBehaviour
{
    public bool isPowered;

    [SerializeField] private Image image;

    private PipeController collidersPipeController;

    private void OnTriggerEnter(Collider other)
    {
        collidersPipeController = other.gameObject.GetComponent<PipeController>();
        if (collidersPipeController.isPowered)
        {
            isPowered = true;
            image.color = Color.cyan;
        }
    }
}
