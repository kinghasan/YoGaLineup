using System;
using UnityEngine;

namespace Aya.TweenPro
{
    [Tweener("Camera Orthographic Size", "Camera")]
    [Serializable]
    public class TweenCameraOrthographicSize : TweenValueFloat<Camera>
    {
        public override float Value
        {
            get => Target.orthographicSize;
            set => Target.orthographicSize = value;
        }
    }

    #region Extension

    public static partial class UTween
    {
        public static TweenCameraOrthographicSize Orthographic(Camera camera, float to, float duration)
        {
            var tweener = Play<TweenCameraOrthographicSize, Camera, float>(camera, to, duration);
            return tweener;
        }

        public static TweenCameraOrthographicSize Orthographic(Camera camera, float from, float to, float duration)
        {
            var tweener = Play<TweenCameraOrthographicSize, Camera, float>(camera, from, to, duration);
            return tweener;
        }
    }

    public static partial class CameraExtension
    {
        public static TweenCameraOrthographicSize TweenOrthographic(this Camera camera, float to, float duration)
        {
            var tweener = UTween.Orthographic(camera, to, duration);
            return tweener;
        }

        public static TweenCameraOrthographicSize TweenOrthographic(this Camera camera, float from, float to, float duration)
        {
            var tweener = UTween.Orthographic(camera, from, to, duration);
            return tweener;
        }
    }

    #endregion
}