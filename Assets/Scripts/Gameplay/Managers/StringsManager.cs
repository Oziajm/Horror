using System;
using Gameplay.Utils;
using UnityEngine;

namespace Gameplay.Managers
{
    public class StringsManager : Singleton<StringsManager>
    {
        //Animation names
        public readonly string RUN_ANIMATION_NAME = "RUN_ANIMATION";
        public readonly string IDLE_ANIMATION_NAME = "IDLE_ANIMATION";
        public readonly string WALK_ANIMATION_NAME = "WALK_ANIMATION";
        public readonly string FLASHED_ANIMATION = "FLASHED_ANIMATION";
        public readonly string STUN_ANIMATION = "STUN_ANIMATION_1";
        public readonly string EXITING_STUN_ANIMATION = "STUN_ANIMATION_2";

        //Animator Parameters names
        public readonly string PLAYER_REACHED_DESTINATION_ANIMATOR_VARIABLE = "reachedDestination";
        public readonly string ANIMATRONIC_ACTIVATED_VARIABLE = "animatronicActivated";

        //Tags
        public readonly string PLAYER_TAG = "Player";
    }
}