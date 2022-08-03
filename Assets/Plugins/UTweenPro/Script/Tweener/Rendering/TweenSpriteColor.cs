using System;
using UnityEngine;

namespace Aya.TweenPro
{
    [Tweener("Sprite Color", "Rendering")]
    [Serializable]
    public class TweenSpriteColor : TweenValueColor<SpriteRenderer>
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
        public static TweenSpriteColor Color(SpriteRenderer spriteRenderer, Color to, float duration)
        {
            var tweener = Play<TweenSpriteColor, SpriteRenderer, Color>(spriteRenderer, to, duration);
            return tweener;
        }

        public static TweenSpriteColor Color(SpriteRenderer spriteRenderer, Color from, Color to, float duration)
        {
            var tweener = Play<TweenSpriteColor, SpriteRenderer, Color>(spriteRenderer, from, to, duration);
            return tweener;
        }

        public static TweenSpriteColor Color(SpriteRenderer spriteRenderer, Gradient gradient, float duration)
        {
            var tweener = Play<TweenSpriteColor, SpriteRenderer>(spriteRenderer, duration)
                .SetColorMode(ColorMode.Gradient)
                .SetGradient(gradient) as TweenSpriteColor;
            return tweener;
        }
    }

    public static partial class SpriteRendererExtension
    {
        public static TweenSpriteColor TweenColor(this SpriteRenderer spriteRenderer, Color to, float duration)
        {
            var tweener = UTween.Color(spriteRenderer, to, duration);
            return tweener;
        }

        public static TweenSpriteColor TweenColor(this SpriteRenderer spriteRenderer, Color from, Color to, float duration)
        {
            var tweener = UTween.Color(spriteRenderer, from, to, duration);
            return tweener;
        }

        public static TweenSpriteColor TweenColor(this SpriteRenderer spriteRenderer, Gradient gradient, float duration)
        {
            var tweener = UTween.Color(spriteRenderer, gradient, duration);
            return tweener;
        }
    }

    #endregion
}