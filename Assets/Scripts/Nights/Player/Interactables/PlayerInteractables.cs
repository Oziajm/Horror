using UnityEngine;
using TMPro;
public class PlayerInteractables : MonoBehaviour
{
    #region Variables

    [SerializeField] private Transform cameraTransform;
    [SerializeField] private float maxDistance = 1;
    [SerializeField] private LayerMask layers;
    [SerializeField] private TextMeshProUGUI useText;
    [SerializeField] private GameObject textWindow;

    #endregion

    #region Unity Methods

    private void Update()
    {
        if(Physics.Raycast(cameraTransform.position, cameraTransform.forward, out RaycastHit hitInfo, maxDistance, layers))
        {
            if(hitInfo.collider.TryGetComponent<Interactable>(out Interactable interactable) && interactable.active)
            {
                textWindow.SetActive(true);
                useText.text = interactable.GetHoverText();
                if(Input.GetKeyDown(KeyCode.E))
                {
                    interactable.Interact();
                }
                return;
            }
        }
        textWindow.SetActive(false);
    }

    #endregion
}
