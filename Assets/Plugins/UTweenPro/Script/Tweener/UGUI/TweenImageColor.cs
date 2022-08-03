using System;
using UnityEngine;
using UnityEngine.UI;

namespace Aya.TweenPro
{
    [Tweener("Image Color", "UGUI")]
    [Serializable]
    public class TweenImageColor : TweenValueColor<Image>
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
        public static TweenImageColor Color(Image image, Color to, float duration)
        {
            var tweener = Play<TweenImageColor, Image, Color>(image, to, duration);
            return tweener;
        }

        public static TweenImageColor Color(Image image, Color from, Color to, float duration)
        {
            var tweener = Play<TweenImageColor, Image, Color>(image, from, to, duration);
            return tweener;
        }

        public static TweenImageColor Color(Image image, Gradient gradient, float duration)
        {
            var tweener = Play<TweenImageColor, Image>(image, duration)
                .SetColorMode(ColorMode.Gradient)
                .SetGradient(gradient) as TweenImageColor;
            return tweener;
        }
    }

    public static partial class ImageExtension
    {
        public static TweenImageColor TweenColor(this Image image, Color to, float duration)
        {
            var tweener = UTween.Color(image, to, duration);
            return tweener;
        }

        public static TweenImageColor TweenColor(this Image image, Color from, Color to, float duration)
        {
            var tweener = UTween.Color(image, from, to, duration);
            return tweener;
        }

        public static TweenImageColor TweenColor(this Image image, Gradient gradient, float duration)
        {
            var tweener = UTween.Color(image, gradient, duration);
            return tweener;
        }
    }

    #endregion
}