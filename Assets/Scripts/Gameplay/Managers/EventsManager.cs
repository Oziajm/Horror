using System;
using Gameplay.Utils;

namespace Gameplay.Managers
{
    public class EventsManager : Singleton<EventsManager>
    {
        public Action AnimatronicsActivated;

        public Action<int> CameraButtonClicked;

        public Action CamerasExitButtonClicked;

        public Action<BaseItem> ItemPickedUp;
    }
}