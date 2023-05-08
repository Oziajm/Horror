using UnityEngine;
using Gameplay.Managers;
using TMPro;
using UnityEngine.InputSystem;

namespace Gameplay.SecurityCameras
{
    public class CamerasController : Interactable
    {
        private const float DISTANCE_TO_ACTIVATE = 2.5f;

        [SerializeField]
        private Transform player;

        [SerializeField]
        private Transform camerasCam;

        [SerializeField]
        private TMP_Text areaNameText;

        private InputActions inputActions;

        private void OnEnable()
        {
            inputActions = new();
            inputActions.Player.Enable();

            EventsManager.Instance.CameraButtonClicked += ChangeCamera;
        }

        public void ChangeCamera(int cameraNumber)
        {
            Camera camera = CamerasManager.Instance.GetCameraWithIndex(cameraNumber);

            camerasCam.position = camera.transform.position;
            camerasCam.rotation = camera.transform.rotation;

            areaNameText.SetText(CamerasManager.Instance.GetAreaName(camera));
        }

        public override string GetHoverText()
        {
            return "Use";
        }

        public override void Interact()
        {
            float distance = Vector3.Distance(transform.position, player.transform.position);

            if (distance < DISTANCE_TO_ACTIVATE)
            {
                ChangeCameraView(true);
                inputActions.Player.Escape.started += OnEscapeDown;
            }
        }

        private void ChangeCameraView(bool areCamerasOpen)
        {
            ChangeCurrentCamera(areCamerasOpen);

            InteractionsManager.Instance.ChangeCursorInteractability(areCamerasOpen);

            if (areCamerasOpen)
            {
                ChangeCamera(0);
                HUDsManager.Instance.OpenCamerasView();
            }
            else
            {
                HUDsManager.Instance.OpenGameplayView();
            }
        }

        private void OnEscapeDown(InputAction.CallbackContext context)
        {
            ChangeCameraView(false);
            inputActions.Player.Escape.started -= OnEscapeDown;
        }

        private void ChangeCurrentCamera(bool areCamerasOpen)
        {
            camerasCam.gameObject.SetActive(areCamerasOpen);
            player.gameObject.SetActive(!areCamerasOpen);
        }
    }
}