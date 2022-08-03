using System;
using UnityEngine.UI;

namespace Aya.TweenPro
{
    [Tweener("ScrollRect Horizontal", "UGUI Scroll")]
    [Serializable]
    public class TweenScrollRectHorizontal : TweenValueFloat<ScrollRect>
    {
        public override float Value
        {
            get => Target.horizontalNormalizedPosition;
            set => Target.horizontalNormalizedPosition = value;
        }
    }

    #region Extension

    public static partial class UTween
    {
        public static TweenScrollRectHorizontal Horizontal(ScrollRect scrollRect, float to, float duration)
        {
            var tweener = Play<TweenScrollRectHorizontal, ScrollRect, float>(scrollRect, to, duration);
            return tweener;
        }

        public static TweenScrollRectHorizontal Horizontal(ScrollRect scrollRect, float from, float to, float duration)
        {
            var tweener = Play<TweenScrollRectHorizontal, ScrollRect, float>(scrollRect, from, to, duration);
            return tweener;
        }
    }

    public static partial class ScrollRectExtension
    {
        public static TweenScrollRectHorizontal TweenHorizontal(this ScrollRect scrollRect, float to, float duration)
        {
            var tweener = UTween.Horizontal(scrollRect, to, duration);
            return tweener;
        }

        public static TweenScrollRectHorizontal TweenHorizontal(this ScrollRect scrollRect, float from, float to, float duration)
        {
            var tweener = UTween.Horizontal(scrollRect, from, to, duration);
            return tweener;
        }
    }

    #endregion
}