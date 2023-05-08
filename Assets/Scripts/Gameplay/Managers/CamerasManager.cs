using UnityEngine;
using Gameplay.Utils;
using System.Collections.Generic;

namespace Gameplay.Managers
{
    public class CamerasManager : Singleton<CamerasManager>
    {
        [SerializeField]
        private List<SecurityCameras.Camera> cameras;

        private new void Awake()
        {
            base.Awake();

            for (int i = 0; i < cameras.Count; i++)
            {
                cameras[i].AssignCameraNumber(i);
            }
        }

        public string GetAreaName(SecurityCameras.Camera camera)
        {
            return camera.AreaName;
        }

        public SecurityCameras.Camera GetCameraWithIndex(int index)
        {
            return cameras[index];
        }
    }
}