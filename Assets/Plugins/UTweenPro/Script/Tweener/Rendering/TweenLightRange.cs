using System;
using UnityEngine;

namespace Aya.TweenPro
{
    [Tweener("Light Range", "Rendering")]
    [Serializable]
    public class TweenLightRange : TweenValueFloat<Light>
    {
        public override float Value
        {
            get => Target.range;
            set => Target.range = value;
        }
    }

    #region Extension

    public static partial class UTween
    {
        public static TweenLightRange Range(Light light, float to, float duration)
        {
            var tweener = Play<TweenLightRange, Light, float>(light, to, duration);
            return tweener;
        }

        public static TweenLightRange Range(Light light, float from, float to, float duration)
        {
            var tweener = Play<TweenLightRange, Light, float>(light, from, to, duration);
            return tweener;
        }
    }

    public static partial class LightExtension
    {
        public static TweenLightRange TweenRange(this Light light, float to, float duration)
        {
            var tweener = UTween.Range(light, to, duration);
            return tweener;
        }

        public static TweenLightRange TweenRange(this Light light, float from, float to, float duration)
        {
            var tweener = UTween.Range(light, from, to, duration);
            return tweener;
        }
    }

    #endregion
}