using System;
using UnityEngine;

namespace Aya.TweenPro
{
    [Tweener("Camera Aspect", "Camera")]
    [Serializable]
    public class TweenCameraAspect : TweenValueFloat<Camera>
    {
        public override float Value
        {
            get => Target.aspect;
            set => Target.aspect = value;
        }
    }

    #region Extension

    public static partial class UTween
    {
        public static TweenCameraAspect Aspect(Camera camera, float to, float duration)
        {
            var tweener = Play<TweenCameraAspect, Camera, float>(camera, to, duration);
            return tweener;
        }

        public static TweenCameraAspect Aspect(Camera camera, float from, float to, float duration)
        {
            var tweener = Play<TweenCameraAspect, Camera, float>(camera, from, to, duration);
            return tweener;
        }
    }

    public static partial class CameraExtension
    {
        public static TweenCameraAspect TweenAspect(this Camera camera, float to, float duration)
        {
            var tweener = UTween.Aspect(camera, to, duration);
            return tweener;
        }

        public static TweenCameraAspect TweenAspect(this Camera camera, float from, float to, float duration)
        {
            var tweener = UTween.Aspect(camera, from, to, duration);
            return tweener;
        }
    }

    #endregion
}