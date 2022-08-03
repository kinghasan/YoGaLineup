using System;
using UnityEngine;
using UnityEngine.UI;

namespace Aya.TweenPro
{
    [Tweener("Graphic Color", "UGUI", "GraphicRaycaster Icon")]
    [Serializable]
    public class TweenGraphicColor : TweenValueColor<Graphic>
    {
        public override Color Value
        {
            get => Target.color;
            set => Target.color = value;
        }
    }

    #region Extension

    public static partial class UTween
    {
        public static TweenGraphicColor Color(Graphic graphic, Color to, float duration)
        {
            var tweener = Play<TweenGraphicColor, Graphic, Color>(graphic, to, duration);
            return tweener;
        }

        public static TweenGraphicColor Color(Graphic graphic, Color from, Color to, float duration)
        {
            var tweener = Play<TweenGraphicColor, Graphic, Color>(graphic, from, to, duration);
            return tweener;
        }

        public static TweenGraphicColor Color(Graphic graphic, Gradient gradient, float duration)
        {
            var tweener = Play<TweenGraphicColor, Graphic>(graphic, duration)
                .SetColorMode(ColorMode.Gradient)
                .SetGradient(gradient) as TweenGraphicColor;
            return tweener;
        }
    }

    public static partial class GraphicExtension
    {
        public static TweenGraphicColor TweenColor(this Graphic graphic, Color to, float duration)
        {
            var tweener = UTween.Color(graphic, to, duration);
            return tweener;
        }

        public static TweenGraphicColor TweenColor(this Graphic graphic, Color from, Color to, float duration)
        {
            var tweener = UTween.Color(graphic, from, to, duration);
            return tweener;
        }

        public static TweenGraphicColor TweenColor(this Graphic graphic, Gradient gradient, float duration)
        {
            var tweener = UTween.Color(graphic, gradient, duration);
            return tweener;
        }
    }

    #endregion
}