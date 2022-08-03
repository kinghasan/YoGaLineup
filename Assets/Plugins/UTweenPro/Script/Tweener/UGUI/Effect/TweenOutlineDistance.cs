using System;
using UnityEngine;
using UnityEngine.UI;

namespace Aya.TweenPro
{
    [Tweener("Outline Distance", "UGUI Effect")]
    [Serializable]
    public class TweenOutlineDistance : TweenValueVector2<Outline>
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
        public static TweenOutlineDistance Distance(Outline outline, Vector2 to, float duration)
        {
            var tweener = Play<TweenOutlineDistance, Outline, Vector2>(outline, to, duration);
            return tweener;
        }

        public static TweenOutlineDistance Distance(Outline outline, Vector2 from, Vector2 to, float duration)
        {
            var tweener = Play<TweenOutlineDistance, Outline, Vector2>(outline, from, to, duration);
            return tweener;
        }
    }

    public static partial class OutlineExtension
    {
        public static TweenOutlineDistance TweenDistance(this Outline outline, Vector2 to, float duration)
        {
            var tweener = UTween.Distance(outline, to, duration);
            return tweener;
        }

        public static TweenOutlineDistance TweenDistance(this Outline outline, Vector2 from, Vector2 to, float duration)
        {
            var tweener = UTween.Distance(outline, from, to, duration);
            return tweener;
        }
    }

    #endregion
}