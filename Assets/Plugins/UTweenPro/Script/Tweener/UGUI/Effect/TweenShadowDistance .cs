using System;
using UnityEngine;
using UnityEngine.UI;

namespace Aya.TweenPro
{
    [Tweener("Shadow Distance", "UGUI Effect")]
    [Serializable]
    public class TweenShadowDistance : TweenValueVector2<Shadow>
    {
        public override Vector2 Value
        {
            get => Target.effectDistance;
            set => Target.effectDistance = value;
        }

        public override void Reset()
        {
            base.Reset();
            From = Vector2.zero;
            To = new Vector2(1f, -1f);
        }
    }

    #region Extension

    public static partial class UTween
    {
        public static TweenShadowDistance Distance(Shadow shadow, Vector2 to, float duration)
        {
            var tweener = Play<TweenShadowDistance, Shadow, Vector2>(shadow, to, duration);
            return tweener;
        }

        public static TweenShadowDistance Distance(Shadow shadow, Vector2 from, Vector2 to, float duration)
        {
            var tweener = Play<TweenShadowDistance, Shadow, Vector2>(shadow, from, to, duration);
            return tweener;
        }
    }

    public static partial class ShadowExtension
    {
        public static TweenShadowDistance TweenDistance(this Shadow shadow, Vector2 to, float duration)
        {
            var tweener = UTween.Distance(shadow, to, duration);
            return tweener;
        }

        public static TweenShadowDistance TweenDistance(this Shadow shadow, Vector2 from, Vector2 to, float duration)
        {
            var tweener = UTween.Distance(shadow, from, to, duration);
            return tweener;
        }
    }

    #endregion
}