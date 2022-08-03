using UnityEngine;
using MoreMountains.NiceVibrations;

namespace Fishtail.PlayTheBall.Vibration
{
    public class VibrationController : MonoBehaviour
    {
        public static VibrationController Instance { get; private set; }
        public static float Interval = 0.1f;

        private float _lastTime = -1f;

        private void Awake()
        {
            Instance = this;
            MMVibrationManager.iOSInitializeHaptics();
        }

        private void OnDestroy()
        {
            MMVibrationManager.iOSReleaseHaptics();
        }

        public void Impact()
        {
            Impact(HapticTypes.LightImpact);
        }

        public void Impact(HapticTypes type)
        {
            var currentTime = Time.realtimeSinceStartup;
            if (currentTime - _lastTime < Interval) return;
            _lastTime = currentTime;
            MMVibrationManager.Haptic(type);
        }
    }
}