using System;
using UnityEngine;
using UnityEngine.UI;

namespace Aya.TweenPro
{
    [Tweener("Outline Color", "UGUI Effect")]
    [Serializable]
    public class TweenOutlineColor : TweenValueColor<Outline>
    {
        public override Color Value
        {
            get => Target.effectColor;
            set => Target.effectColor = value;
        }

        public override void Reset()
        {
            base.Reset();
            From = Color.clear;
            To = new Color(0f, 0f, 0f, 0.5f);
        }
    }

    #region Extension

    public static partial class UTween
    {
        public static TweenOutlineColor Color(Outline outline, Color to, float duration)
        {
            var tweener = Play<TweenOutlineColor, Outline, Color>(outline, to, duration);
            return tweener;
        }

        public static TweenOutlineColor Color(Outline outline, Color from, Color to, float duration)
        {
            var tweener = Play<TweenOutlineColor, Outline, Color>(outline, from, to, duration);
            return tweener;
        }

        public static TweenOutlineColor Color(Outline outline, Gradient gradient, float duration)
        {
            var tweener = Play<TweenOutlineColor, Outline>(outline, duration)
                .SetColorMode(ColorMode.Gradient)
                .SetGradient(gradient) as TweenOutlineColor;
            return tweener;
        }
    }

    public static partial class OutlineExtension
    {
        public static TweenOutlineColor TweenColor(this Outline outline, Color to, float duration)
        {
            var tweener = UTween.Color(outline, to, duration);
            return tweener;
        }

        public static TweenOutlineColor TweenColor(this Outline outline, Color from, Color to, float duration)
        {
            var tweener = UTween.Color(outline, from, to, duration);
            return tweener;
        }

        public static TweenOutlineColor TweenColor(this Outline outline, Gradient gradient, float duration)
        {
            var tweener = UTween.Color(outline, gradient, duration);
            return tweener;
        }
    }

    #endregion
}