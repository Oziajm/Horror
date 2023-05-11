using UnityEngine;

namespace Gameplay.SecurityCameras
{
    public class Camera : MonoBehaviour
    {
        [field:SerializeField]
        public string AreaName { get; private set; }

        [SerializeField]
        private CameraButton cameraButton;

        public void AssignCameraNumber(int cameraNumber)
        {
            cameraButton.AssignCameraNumber(cameraNumber);
        }
    }
}