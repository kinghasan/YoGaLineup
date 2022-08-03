using System;
using UnityEngine;

namespace Aya.TweenPro
{
    [Tweener("Camera Viewport Rect", "Camera")]
    [Serializable]
    public class TweenCameraViewportRect : TweenValueRect<Camera>
    {
        public override Rect Value
        {
            get => Target.rect;
            set => Target.rect = value;
        }
    }

    #region Extension

    public static partial class UTween
    {
        public static TweenCameraViewportRect Viewport(Camera camera, Rect to, float duration)
        {
            var tweener = Play<TweenCameraViewportRect, Camera, Rect>(camera, to, duration);
            return tweener;
        }

        public static TweenCameraViewportRect Viewport(Camera camera, Rect from, Rect to, float duration)
        {
            var tweener = Play<TweenCameraViewportRect, Camera, Rect>(camera, from, to, duration);
            return tweener;
        }
    }

    public static partial class CameraExtension
    {
        public static TweenCameraViewportRect TweenViewport(this Camera camera, Rect to, float duration)
        {
            var tweener = UTween.Viewport(camera, to, duration);
            return tweener;
        }

        public static TweenCameraViewportRect TweenViewport(this Camera camera, Rect from, Rect to, float duration)
        {
            var tweener = UTween.Viewport(camera, from, to, duration);
            return tweener;
        }
    }

    #endregion
}