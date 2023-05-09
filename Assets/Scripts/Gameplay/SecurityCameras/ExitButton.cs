using UnityEngine;
using UnityEngine.UI;
using Gameplay.Managers;

namespace Gameplay.SecurityCameras
{
    public class ExitButton : MonoBehaviour
    {
        [SerializeField]
        private Button button;

        private void OnEnable()
        {
            button.onClick.AddListener(ExitCameras);
        }

        private void ExitCameras()
        {
            EventsManager.Instance.CamerasExitButtonClicked?.Invoke();
        }

        private void OnDisable()
        {
            button.onClick.RemoveAllListeners();
        }
    }
}