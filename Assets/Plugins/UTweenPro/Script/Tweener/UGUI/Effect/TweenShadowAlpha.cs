using System;
using UnityEngine.UI;

namespace Aya.TweenPro
{
    [Tweener("Shadow Alpha", "UGUI Effect")]
    [Serializable]
    public class TweenShadowAlpha : TweenValueFloat<Shadow>
    {
        public override float Value
        {
            get => Target.effectColor.a;
            set
            {
                var color = Target.effectColor;
                color.a = value;
                Target.effectColor = color;
            }
        }
    }

    #region Extension

    public static partial class UTween
    {
        public static TweenShadowAlpha Alpha(Shadow shadow, float to, float duration)
        {
            var tweener = Play<TweenShadowAlpha, Shadow, float>(shadow, to, duration);
            return tweener;
        }

        public static TweenShadowAlpha Alpha(Shadow shadow, float from, float to, float duration)
        {
            var tweener = Play<TweenShadowAlpha, Shadow, float>(shadow, from, to, duration);
            return tweener;
        }
    }

    public static partial class ShadowExtension
    {
        public static TweenShadowAlpha TweenAlpha(this Shadow shadow, float to, float duration)
        {
            var tweener = UTween.Alpha(shadow, to, duration);
            return tweener;
        }

        public static TweenShadowAlpha TweenAlpha(this Shadow shadow, float from, float to, float duration)
        {
            var tweener = UTween.Alpha(shadow, from, to, duration);
            return tweener;
        }
    }

    #endregion
}