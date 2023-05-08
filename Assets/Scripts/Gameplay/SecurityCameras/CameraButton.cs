using UnityEngine;
using UnityEngine.UI;
using Gameplay.Managers;

namespace Gameplay.SecurityCameras
{
    public class CameraButton : MonoBehaviour
    {
        [SerializeField]
        private Button button;

        private int cameraNumber;

        private void OnEnable()
        {
            button.onClick.AddListener(SelectCamera);
        }

        public void AssignCameraNumber(int cameraNumber)
        {
            this.cameraNumber = cameraNumber;
        }

        private void SelectCamera()
        {
            EventsManager.Instance.CameraButtonClicked?.Invoke(cameraNumber);
        }

        public void SelectButton()
        {
            button.Select();
        }

        private void OnDisable()
        {
            button.onClick.RemoveAllListeners();
        }
    }
}