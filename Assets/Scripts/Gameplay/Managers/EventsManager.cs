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
    }
}