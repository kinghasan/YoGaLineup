using System;
using UnityEngine;
using UnityEngine.UI;

namespace Aya.TweenPro
{
    [Tweener("Shadow Color", "UGUI Effect")]
    [Serializable]
    public class TweenShadowColor : TweenValueColor<Shadow>
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
        public static TweenShadowColor Color(Shadow shadow, Color to, float duration)
        {
            var tweener = Play<TweenShadowColor, Shadow, Color>(shadow, to, duration);
            return tweener;
        }

        public static TweenShadowColor Color(Shadow shadow, Color from, Color to, float duration)
        {
            var tweener = Play<TweenShadowColor, Shadow, Color>(shadow, from, to, duration);
            return tweener;
        }

        public static TweenShadowColor Color(Shadow shadow, Gradient gradient, float duration)
        {
            var tweener = Play<TweenShadowColor, Shadow>(shadow, duration)
                .SetColorMode(ColorMode.Gradient)
                .SetGradient(gradient) as TweenShadowColor;
            return tweener;
        }
    }

    public static partial class ShadowExtension
    {
        public static TweenShadowColor TweenColor(this Shadow shadow, Color to, float duration)
        {
            var tweener = UTween.Color(shadow, to, duration);
            return tweener;
        }

        public static TweenShadowColor TweenColor(this Shadow shadow, Color from, Color to, float duration)
        {
            var tweener = UTween.Color(shadow, from, to, duration);
            return tweener;
        }

        public static TweenShadowColor TweenColor(this Shadow shadow, Gradient gradient, float duration)
        {
            var tweener = UTween.Color(shadow, gradient, duration);
            return tweener;
        }
    }

    #endregion
}