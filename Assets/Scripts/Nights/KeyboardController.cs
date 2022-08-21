using UnityEngine;

public class KeyboardController : MonoBehaviour
{
    [SerializeField] private CamerasController camerasController;

    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKey(KeyCode.E))
        {
            if (camerasController.rotation < 20)
            {
                camerasController.rotation++;
                camerasController.SetNewCameraRotation();
            }
        }
        if (Input.GetKey(KeyCode.Q))
        {
            if (camerasController.rotation > -20)
            {
                camerasController.rotation--;
                camerasController.SetNewCameraRotation();
            }
        }

        if (Input.GetKey(KeyCode.Alpha0))
        {
            camerasController.cameraNumber = 0;
            camerasController.ChangeCameraAtScreen();
        }
        if (Input.GetKey(KeyCode.Alpha1))
        {
            camerasController.cameraNumber = 1;
            camerasController.ChangeCameraAtScreen();
        }
        if (Input.GetKey(KeyCode.Alpha2))
        {
            camerasController.cameraNumber = 2;
            camerasController.ChangeCameraAtScreen();
        }
        if (Input.GetKey(KeyCode.Alpha3))
        {
            camerasController.cameraNumber = 3;
            camerasController.ChangeCameraAtScreen();
        }
        if (Input.GetKey(KeyCode.Alpha4))
        {
            camerasController.cameraNumber = 4;
            camerasController.ChangeCameraAtScreen();
        }
        if (Input.GetKey(KeyCode.Alpha5))
        {
            camerasController.cameraNumber = 5;
            camerasController.ChangeCameraAtScreen();
        }
        if (Input.GetKey(KeyCode.Alpha6))
        {
            camerasController.cameraNumber = 6;
            camerasController.ChangeCameraAtScreen();
        }
        if (Input.GetKey(KeyCode.Alpha7))
        {
            camerasController.cameraNumber = 7;
            camerasController.ChangeCameraAtScreen();
        }
        if (Input.GetKey(KeyCode.Alpha8))
        {
            camerasController.cameraNumber = 8;
            camerasController.ChangeCameraAtScreen();
        }
        if (Input.GetKey(KeyCode.Alpha9))
        {
            camerasController.cameraNumber = 9;
            camerasController.ChangeCameraAtScreen();
        }
    }
}
