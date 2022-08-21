using UnityEngine;

public class CamerasScript : MonoBehaviour
{
    [SerializeField] private int camNumber;
    [SerializeField] private CamerasController camerasController;

    private void OnMouseDown()
    {
        camerasController.cameraNumber = camNumber;
        camerasController.ChangeCameraAtScreen();
    }
}
