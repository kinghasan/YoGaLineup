using System;
using UnityEngine;

namespace Aya.TweenPro
{
    [Tweener("Line Renderer Start Color", "Rendering")]
    [Serializable]
    public class TweenLineRendererStartColor : TweenValueColor<LineRenderer>
    {
        public override Color Value
        {
            get => Target.startColor;
            set => Target.startColor = value;
        }
    }

    #region Extension

    public static partial class UTween
    {
        public static TweenLineRendererStartColor StartColor(LineRenderer lineRenderer, Color to, float duration)
        {
            var tweener = Play<TweenLineRendererStartColor, LineRenderer, Color>(lineRenderer, to, duration);
            return tweener;
        }

        public static TweenLineRendererStartColor StartColor(LineRenderer lineRenderer, Color from, Color to, float duration)
        {
            var tweener = Play<TweenLineRendererStartColor, LineRenderer, Color>(lineRenderer, from, to, duration);
            return tweener;
        }

        public static TweenLineRendererStartColor StartColor(LineRenderer lineRenderer, Gradient gradient, float duration)
        {
            var tweener = Play<TweenLineRendererStartColor, LineRenderer>(lineRenderer, duration)
                .SetColorMode(ColorMode.Gradient)
                .SetGradient(gradient) as TweenLineRendererStartColor;
            return tweener;
        }
    }

    public static partial class LineRendererExtension
    {
        public static TweenLineRendererStartColor TweenStartColor(this LineRenderer lineRenderer, Color to, float duration)
        {
            var tweener = UTween.StartColor(lineRenderer, to, duration);
            return tweener;
        }

        public static TweenLineRendererStartColor TweenStartColor(this LineRenderer lineRenderer, Color from, Color to, float duration)
        {
            var tweener = UTween.StartColor(lineRenderer, from, to, duration);
            return tweener;
        }

        public static TweenLineRendererStartColor TweenStartColor(this LineRenderer lineRenderer, Gradient gradient, float duration)
        {
            var tweener = UTween.StartColor(lineRenderer, gradient, duration);
            return tweener;
        }
    }

    #endregion
}