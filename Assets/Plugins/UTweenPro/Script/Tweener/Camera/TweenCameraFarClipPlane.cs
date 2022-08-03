using System;
using UnityEngine;

namespace Aya.TweenPro
{
    [Tweener("Camera Far Clip Plane", "Camera")]
    [Serializable]
    public class TweenCameraFarClipPlane : TweenValueFloat<Camera>
    {
        public override float Value
        {
            get => Target.farClipPlane;
            set => Target.farClipPlane = value;
        }
    }

    #region Extension

    public static partial class UTween
    {
        public static TweenCameraFarClipPlane FarClipPlane(Camera camera, float to, float duration)
        {
            var tweener = Play<TweenCameraFarClipPlane, Camera, float>(camera, to, duration);
            return tweener;
        }

        public static TweenCameraFarClipPlane FarClipPlane(Camera camera, float from, float to, float duration)
        {
            var tweener = Play<TweenCameraFarClipPlane, Camera, float>(camera, from, to, duration);
            return tweener;
        }
    }

    public static partial class CameraExtension
    {
        public static TweenCameraFarClipPlane TweenFarClipPlane(this Camera camera, float to, float duration)
        {
            var tweener = UTween.FarClipPlane(camera, to, duration);
            return tweener;
        }

        public static TweenCameraFarClipPlane TweenFarClipPlane(this Camera camera, float from, float to, float duration)
        {
            var tweener = UTween.FarClipPlane(camera, from, to, duration);
            return tweener;
        }
    }

    #endregion
}