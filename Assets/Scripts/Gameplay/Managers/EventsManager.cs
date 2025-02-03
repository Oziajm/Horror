using System;
using Gameplay.Utils;
using UnityEngine;

namespace Gameplay.Managers
{
    public class EventsManager : Singleton<EventsManager>
    {
        public Action AnimatronicsActivated;

        public Action<int> CameraButtonClicked;

        public Action CamerasExitButtonClicked;

        public Action<HidingSpot> PlayerEnteredHidingSpot;
        public Action PlayerLeftHidingSpot;

        public Action PlayerSpotted;
        public Action PlayerOutOfSight;

        public Action<bool> ToggleFlashlight;

        public Action<bool> ToggleStaminaBarVisibility;
        public Action<float> SetStamina;

        public Action<bool> IsPlayerUsingKeyboard;

        public Action<bool> ToggleInteractableViewVisibity;
        public Action<string> SetInterctableViewText;
    }
}