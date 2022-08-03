using System;
using UnityEngine;

namespace Aya.TweenPro
{
    [Tweener("Line Renderer End Color", "Rendering")]
    [Serializable]
    public class TweenLineRendererEndColor : TweenValueColor<LineRenderer>
    {
        public override Color Value
        {
            get => Target.endColor;
            set => Target.endColor = value;
        }
    }

    #region Extension

    public static partial class UTween
    {
        public static TweenLineRendererEndColor EndColor(LineRenderer lineRenderer, Color to, float duration)
        {
            var tweener = Play<TweenLineRendererEndColor, LineRenderer, Color>(lineRenderer, to, duration);
            return tweener;
        }

        public static TweenLineRendererEndColor EndColor(LineRenderer lineRenderer, Color from, Color to, float duration)
        {
            var tweener = Play<TweenLineRendererEndColor, LineRenderer, Color>(lineRenderer, from, to, duration);
            return tweener;
        }

        public static TweenLineRendererEndColor EndColor(LineRenderer lineRenderer, Gradient gradient, float duration)
        {
            var tweener = Play<TweenLineRendererEndColor, LineRenderer>(lineRenderer, duration)
                .SetColorMode(ColorMode.Gradient)
                .SetGradient(gradient) as TweenLineRendererEndColor;
            return tweener;
        }
    }

    public static partial class LineRendererExtension
    {
        public static TweenLineRendererEndColor TweenEndColor(this LineRenderer lineRenderer, Color to, float duration)
        {
            var tweener = UTween.EndColor(lineRenderer, to, duration);
            return tweener;
        }

        public static TweenLineRendererEndColor TweenEndColor(this LineRenderer lineRenderer, Color from, Color to, float duration)
        {
            var tweener = UTween.EndColor(lineRenderer, from, to, duration);
            return tweener;
        }

        public static TweenLineRendererEndColor TweenEndColor(this LineRenderer lineRenderer, Gradient gradient, float duration)
        {
            var tweener = UTween.EndColor(lineRenderer, gradient, duration);
            return tweener;
        }
    }

    #endregion
}