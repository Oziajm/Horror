using UnityEngine;

public class KeyboardController : MonoBehaviour
{
    #region Variables

    [SerializeField] private CamerasController camerasController;

    #endregion

    #region Unity Methods

    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKey(KeyCode.Return))
        {
            camerasController.TurnTVOnOff(true);
        }

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
            camerasController.ChangeCameraAtScreen(0);
        }
        if (Input.GetKey(KeyCode.Alpha1))
        {
            camerasController.ChangeCameraAtScreen(1);
        }
        if (Input.GetKey(KeyCode.Alpha2))
        {
            camerasController.ChangeCameraAtScreen(2);
        }
        if (Input.GetKey(KeyCode.Alpha3))
        {
            camerasController.ChangeCameraAtScreen(3);
        }
        if (Input.GetKey(KeyCode.Alpha4))
        {
            camerasController.ChangeCameraAtScreen(4);
        }
        if (Input.GetKey(KeyCode.Alpha5))
        {
            camerasController.ChangeCameraAtScreen(5);
        }
        if (Input.GetKey(KeyCode.Alpha6))
        {
            camerasController.ChangeCameraAtScreen(6);
        }
        if (Input.GetKey(KeyCode.Alpha7))
        {
            camerasController.ChangeCameraAtScreen(7);
        }
        if (Input.GetKey(KeyCode.Alpha8))
        {
            camerasController.ChangeCameraAtScreen(8);
        }
        if (Input.GetKey(KeyCode.Alpha9))
        {
            camerasController.ChangeCameraAtScreen(9);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        camerasController.TurnTVOnOff(false);
    }

    #endregion
}
