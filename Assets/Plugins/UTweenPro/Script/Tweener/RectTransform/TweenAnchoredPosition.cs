using System;
using UnityEngine;

namespace Aya.TweenPro
{
    [Tweener("Rect Transform Anchored Position", "RectTransform")]
    [Serializable]
    public class TweenAnchoredPosition : TweenValueVector2<RectTransform>
    {
        public override Vector2 Value
        {
            get => Target.anchoredPosition;
            set => Target.anchoredPosition = value;
        }
    }

    #region Extension

    public static partial class UTween
    {
        public static TweenAnchoredPosition AnchoredPosition(RectTransform rectTransform, Vector2 to, float duration)
        {
            var tweener = Play<TweenAnchoredPosition, RectTransform, Vector2>(rectTransform, to, duration);
            return tweener;
        }

        public static TweenAnchoredPosition AnchoredPosition(RectTransform rectTransform, Vector2 from, Vector2 to, float duration)
        {
            var tweener = Play<TweenAnchoredPosition, RectTransform, Vector2>(rectTransform, from, to, duration);
            return tweener;
        }
    }

    public static partial class RectTransformExtension
    {
        public static TweenAnchoredPosition TweenAnchoredPosition(this RectTransform rectTransform, Vector2 to, float duration)
        {
            var tweener = UTween.AnchoredPosition(rectTransform, to, duration);
            return tweener;
        }

        public static TweenAnchoredPosition TweenAnchoredPosition(this RectTransform rectTransform, Vector2 from, Vector2 to, float duration)
        {
            var tweener = UTween.AnchoredPosition(rectTransform, from, to, duration);
            return tweener;
        }
    }

    #endregion
}