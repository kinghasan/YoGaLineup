using System;
using UnityEngine;
using UnityEngine.UI;

namespace Aya.TweenPro
{
    [Tweener("ScrollRect Position", "UGUI Scroll")]
    [Serializable]
    public class TweenScrollRectPosition : TweenValueVector2<ScrollRect>
    {
        public override Vector2 Value
        {
            get => Target.normalizedPosition;
            set => Target.normalizedPosition = value;
        }
    }

    #region Extension

    public static partial class UTween
    {
        public static TweenScrollRectPosition NormalizedPosition(ScrollRect scrollRect, Vector2 to, float duration)
        {
            var tweener = Play<TweenScrollRectPosition, ScrollRect, Vector2>(scrollRect, to, duration);
            return tweener;
        }

        public static TweenScrollRectPosition NormalizedPosition(ScrollRect scrollRect, Vector2 from, Vector2 to, float duration)
        {
            var tweener = Play<TweenScrollRectPosition, ScrollRect, Vector2>(scrollRect, from, to, duration);
            return tweener;
        }
    }

    public static partial class ScrollRectExtension
    {
        public static TweenScrollRectPosition TweenNormalizedPosition(this ScrollRect scrollRect, Vector2 to, float duration)
        {
            var tweener = UTween.NormalizedPosition(scrollRect, to, duration);
            return tweener;
        }

        public static TweenScrollRectPosition TweenNormalizedPosition(this ScrollRect scrollRect, Vector2 from, Vector2 to, float duration)
        {
            var tweener = UTween.NormalizedPosition(scrollRect, from, to, duration);
            return tweener;
        }
    }

    #endregion
}