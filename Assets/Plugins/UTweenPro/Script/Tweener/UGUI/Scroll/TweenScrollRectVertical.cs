using System;
using UnityEngine.UI;

namespace Aya.TweenPro
{
    [Tweener("ScrollRect Vertical", "UGUI Scroll")]
    [Serializable]
    public class TweenScrollRectVertical : TweenValueFloat<ScrollRect>
    {
        public override float Value
        {
            get => Target.verticalNormalizedPosition;
            set => Target.verticalNormalizedPosition = value;
        }
    }

    #region Extension

    public static partial class UTween
    {
        public static TweenScrollRectVertical Vertical(ScrollRect scrollRect, float to, float duration)
        {
            var tweener = Play<TweenScrollRectVertical, ScrollRect, float>(scrollRect, to, duration);
            return tweener;
        }

        public static TweenScrollRectVertical Vertical(ScrollRect scrollRect, float from, float to, float duration)
        {
            var tweener = Play<TweenScrollRectVertical, ScrollRect, float>(scrollRect, from, to, duration);
            return tweener;
        }
    }

    public static partial class ScrollRectExtension
    {
        public static TweenScrollRectVertical TweenVertical(this ScrollRect scrollRect, float to, float duration)
        {
            var tweener = UTween.Vertical(scrollRect, to, duration);
            return tweener;
        }

        public static TweenScrollRectVertical TweenVertical(this ScrollRect scrollRect, float from, float to, float duration)
        {
            var tweener = UTween.Vertical(scrollRect, from, to, duration);
            return tweener;
        }
    }

    #endregion
}