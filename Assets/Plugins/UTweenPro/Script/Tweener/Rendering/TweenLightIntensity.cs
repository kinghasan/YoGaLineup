using System;
using UnityEngine;

namespace Aya.TweenPro
{
    [Tweener("Light Intensity", "Rendering")]
    [Serializable]
    public class TweenLightIntensity : TweenValueFloat<Light>
    {
        public override float Value
        {
            get => Target.intensity;
            set => Target.intensity = value;
        }
    }

    #region Extension

    public static partial class UTween
    {
        public static TweenLightIntensity Intensity(Light light, float to, float duration)
        {
            var tweener = Play<TweenLightIntensity, Light, float>(light, to, duration);
            return tweener;
        }

        public static TweenLightIntensity Intensity(Light light, float from, float to, float duration)
        {
            var tweener = Play<TweenLightIntensity, Light, float>(light, from, to, duration);
            return tweener;
        }
    }

    public static partial class LightExtension
    {
        public static TweenLightIntensity TweenIntensity(this Light light, float to, float duration)
        {
            var tweener = UTween.Intensity(light, to, duration);
            return tweener;
        }

        public static TweenLightIntensity TweenIntensity(this Light light, float from, float to, float duration)
        {
            var tweener = UTween.Intensity(light, from, to, duration);
            return tweener;
        }
    }

    #endregion
}