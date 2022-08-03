using System;
using UnityEngine;
using UnityEngine.UI;

namespace Aya.TweenPro
{
    [Tweener("Text Color", "UGUI Text")]
    [Serializable]
    public class TweenTextColor : TweenValueColor<Text>
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
        public static TweenTextColor Color(Text text, Color to, float duration)
        {
            var tweener = Play<TweenTextColor, Text, Color>(text, to, duration);
            return tweener;
        }

        public static TweenTextColor Color(Text text, Color from, Color to, float duration)
        {
            var tweener = Play<TweenTextColor, Text, Color>(text, from, to, duration);
            return tweener;
        }

        public static TweenTextColor Color(Text text, Gradient gradient, float duration)
        {
            var tweener = Play<TweenTextColor, Text>(text, duration)
                .SetColorMode(ColorMode.Gradient)
                .SetGradient(gradient) as TweenTextColor;
            return tweener;
        }
    }

    public static partial class TextExtension
    {
        public static TweenTextColor TweenColor(this Text text, Color to, float duration)
        {
            var tweener = UTween.Color(text, to, duration);
            return tweener;
        }

        public static TweenTextColor TweenColor(this Text text, Color from, Color to, float duration)
        {
            var tweener = UTween.Color(text, from, to, duration);
            return tweener;
        }

        public static TweenTextColor TweenColor(this Text text, Gradient gradient, float duration)
        {
            var tweener = UTween.Color(text, gradient, duration);
            return tweener;
        }
    }

    #endregion
}