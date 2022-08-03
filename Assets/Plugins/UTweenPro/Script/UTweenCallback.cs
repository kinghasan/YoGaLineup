using System;

namespace Aya.TweenPro
{
    public static class UTweenCallback
    {
        public static Action<Exception> OnException { get; set; } = UnityEngine.Debug.LogError;
    }
}
