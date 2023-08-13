using UnityEngine;

namespace Gameplay.SecurityCameras
{
    public class Camera : MonoBehaviour
    {
        [field:SerializeField]
        public string AreaName { get; private set; }

        [field:SerializeField]
        public CameraButton CameraButton { get; private set; }

        public void AssignCameraNumber(int cameraNumber)
        {
            CameraButton.AssignCameraNumber(cameraNumber);
        }

        public void SelectButton()
        {
            CameraButton.SelectButton();
        }
    }
}