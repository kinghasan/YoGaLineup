using System;
using UnityEngine;

namespace Aya.TweenPro
{
    [Tweener("Camera Field Of View", "Camera")]
    [Serializable]
    public class TweenCameraFieldOfView : TweenValueFloat<Camera>
    {
        public override float Value
        {
            get => Target.fieldOfView;
            set => Target.fieldOfView = value;
        }
    }

    #region Extension

    public static partial class UTween
    {
        public static TweenCameraFieldOfView FieldOfView(Camera camera, float to, float duration)
        {
            var tweener = Play<TweenCameraFieldOfView, Camera, float>(camera, to, duration);
            return tweener;
        }

        public static TweenCameraFieldOfView FieldOfView(Camera camera, float from, float to, float duration)
        {
            var tweener = Play<TweenCameraFieldOfView, Camera, float>(camera, from, to, duration);
            return tweener;
        }
    }

    public static partial class CameraExtension
    {
        public static TweenCameraFieldOfView TweenFieldOfView(this Camera camera, float to, float duration)
        {
            var tweener = UTween.FieldOfView(camera, to, duration);
            return tweener;
        }

        public static TweenCameraFieldOfView TweenFieldOfView(this Camera camera, float from, float to, float duration)
        {
            var tweener = UTween.FieldOfView(camera, from, to, duration);
            return tweener;
        }
    }

    #endregion
}