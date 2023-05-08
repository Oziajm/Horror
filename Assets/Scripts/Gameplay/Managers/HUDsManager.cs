using UnityEngine;
using Gameplay.Utils;

namespace Gameplay.Managers
{
    public class HUDsManager : Singleton<HUDsManager>
    {
        [SerializeField]
        private GameObject gameplayView;

        [SerializeField]
        private GameObject camerasView;

        private new void Awake()
        {
            base.Awake();

            OpenGameplayView();
        }

        public void OpenGameplayView()
        {
            CloseAllViews();

            gameplayView.SetActive(true);
        }

        public void OpenCamerasView()
        {
            CloseAllViews();

            camerasView.SetActive(true);
        }

        private void CloseAllViews()
        {
            camerasView.SetActive(false);
            gameplayView.SetActive(false);
        }
    }
}