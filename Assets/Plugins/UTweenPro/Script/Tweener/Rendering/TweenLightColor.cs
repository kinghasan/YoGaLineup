using System;
using UnityEngine;

namespace Aya.TweenPro
{
    [Tweener("Light Color", "Rendering")]
    [Serializable]
    public class TweenLightColor : TweenValueColor<Light>
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
        public static TweenLightColor Color(Light light, Color to, float duration)
        {
            var tweener = Play<TweenLightColor, Light, Color>(light, to, duration);
            return tweener;
        }

        public static TweenLightColor Color(Light light, Color from, Color to, float duration)
        {
            var tweener = Play<TweenLightColor, Light, Color>(light, from, to, duration);
            return tweener;
        }

        public static TweenLightColor Color(Light light, Gradient gradient, float duration)
        {
            var tweener = Play<TweenLightColor, Light>(light, duration)
                .SetColorMode(ColorMode.Gradient)
                .SetGradient(gradient) as TweenLightColor;
            return tweener;
        }
    }

    public static partial class LightExtension
    {
        public static TweenLightColor TweenColor(this Light light, Color to, float duration)
        {
            var tweener = UTween.Color(light, to, duration);
            return tweener;
        }

        public static TweenLightColor TweenColor(this Light light, Color from, Color to, float duration)
        {
            var tweener = UTween.Color(light, from, to, duration);
            return tweener;
        }

        public static TweenLightColor TweenColor(this Light light, Gradient gradient, float duration)
        {
            var tweener = UTween.Color(light, gradient, duration);
            return tweener;
        }
    }

    #endregion
}