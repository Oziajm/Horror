using UnityEngine;
using UnityEngine.UI;

public class PlayerUsables : MonoBehaviour
{
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private float maxDistance = 2;
    [SerializeField] private LayerMask layers;
    [SerializeField] private Text useText;

    private void Update()
    {
        if(Physics.Raycast(cameraTransform.position, cameraTransform.forward, out RaycastHit hitInfo, maxDistance, layers))
        {
            useText.gameObject.SetActive(true);
            if (hitInfo.collider.TryGetComponent<Door>(out Door door))
            {
                if (door.isOpen) useText.text = "Press E to close";
                else useText.text = "Press E to open";

                if(Input.GetKeyDown(KeyCode.E))
                {
                    if (door.isOpen) door.Close();
                    else door.Open();
                }
            }
            else if(hitInfo.collider.TryGetComponent<Button>(out Button button))
            {
                useText.text = "Press E to Use";
                if (Input.GetKeyDown(KeyCode.E))
                {
                    button.OnClick();
                }
            }
            return;
        }
        useText.gameObject.SetActive(false);
    }
}
