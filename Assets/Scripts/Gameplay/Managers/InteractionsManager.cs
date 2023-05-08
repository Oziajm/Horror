using UnityEngine;
using Gameplay.Utils;

namespace Gameplay.Managers
{
    public class InteractionsManager : Singleton<InteractionsManager>
    {
        private new void Awake()
        {
            base.Awake();

            ChangeCursorInteractability(false);
        }

        public void ChangeCursorInteractability(bool isInteractable)
        {
            Cursor.lockState = isInteractable ? CursorLockMode.None : CursorLockMode.Locked;
            Cursor.visible = isInteractable;
        }
    }
}