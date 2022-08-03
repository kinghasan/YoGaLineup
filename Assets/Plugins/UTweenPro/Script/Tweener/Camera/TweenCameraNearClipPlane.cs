using System;
using UnityEngine;

namespace Aya.TweenPro
{
    [Tweener("Camera Near Clip Plane", "Camera")]
    [Serializable]
    public class TweenCameraNearClipPlane : TweenValueFloat<Camera>
    {
        public override float Value
        {
            get => Target.nearClipPlane;
            set => Target.nearClipPlane = value;
        }
    }

    #region Extension

    public static partial class UTween
    {
        public static TweenCameraNearClipPlane NearClipPlane(Camera camera, float to, float duration)
        {
            var tweener = Play<TweenCameraNearClipPlane, Camera, float>(camera, to, duration);
            return tweener;
        }

        public static TweenCameraNearClipPlane NearClipPlane(Camera camera, float from, float to, float duration)
        {
            var tweener = Play<TweenCameraNearClipPlane, Camera, float>(camera, from, to, duration);
            return tweener;
        }
    }

    public static partial class CameraExtension
    {
        public static TweenCameraNearClipPlane TweenNearClipPlane(this Camera camera, float to, float duration)
        {
            var tweener = UTween.NearClipPlane(camera, to, duration);
            return tweener;
        }

        public static TweenCameraNearClipPlane TweenNearClipPlane(this Camera camera, float from, float to, float duration)
        {
            var tweener = UTween.NearClipPlane(camera, from, to, duration);
            return tweener;
        }
    }

    #endregion
}