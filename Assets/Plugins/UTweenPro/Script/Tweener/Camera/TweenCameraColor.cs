using System;
using UnityEngine;

namespace Aya.TweenPro
{
    [Tweener("Camera Color", "Camera")]
    [Serializable]
    public class TweenCameraColor : TweenValueColor<Camera>
    {
        public override Color Value
        {
            get => Target.backgroundColor;
            set => Target.backgroundColor = value;
        }
    }

    #region Extension

    public static partial class UTween
    {
        public static TweenCameraColor Color(Camera camera, Color to, float duration)
        {
            var tweener = Play<TweenCameraColor, Camera, Color>(camera, to, duration);
            return tweener;
        }

        public static TweenCameraColor Color(Camera camera, Color from, Color to, float duration)
        {
            var tweener = Play<TweenCameraColor, Camera, Color>(camera, from, to, duration);
            return tweener;
        }

        public static TweenCameraColor Color(Camera camera, Gradient gradient, float duration)
        {
            var tweener = Play<TweenCameraColor, Camera>(camera, duration)
                .SetColorMode(ColorMode.Gradient)
                .SetGradient(gradient) as TweenCameraColor;
            return tweener;
        }
    }

    public static partial class CameraExtension
    {
        public static TweenCameraColor TweenColor(this Camera camera, Color to, float duration)
        {
            var tweener = UTween.Color(camera, to, duration);
            return tweener;
        }

        public static TweenCameraColor TweenColor(this Camera camera, Color from, Color to, float duration)
        {
            var tweener = UTween.Color(camera, from, to, duration);
            return tweener;
        }

        public static TweenCameraColor TweenColor(this Camera camera, Gradient gradient, float duration)
        {
            var tweener = UTween.Color(camera, gradient, duration);
            return tweener;
        }
    }

    #endregion
}