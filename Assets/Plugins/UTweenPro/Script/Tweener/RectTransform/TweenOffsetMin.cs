using System;
using UnityEngine;

namespace Aya.TweenPro
{
    [Tweener("Rect Transform Offset Min", "RectTransform")]
    [Serializable]
    public class TweenOffsetMin : TweenValueVector2<RectTransform>
    {
        public override Vector2 Value
        {
            get => Target.offsetMin;
            set => Target.offsetMin = value;
        }
    }

    #region Extension

    public static partial class UTween
    {
        public static TweenOffsetMin OffsetMin(RectTransform rectTransform, Vector2 to, float duration)
        {
            var tweener = Play<TweenOffsetMin, RectTransform, Vector2>(rectTransform, to, duration);
            return tweener;
        }

        public static TweenOffsetMin OffsetMin(RectTransform rectTransform, Vector2 from, Vector2 to, float duration)
        {
            var tweener = Play<TweenOffsetMin, RectTransform, Vector2>(rectTransform, from, to, duration);
            return tweener;
        }
    }

    public static partial class RectTransformExtension
    {
        public static TweenOffsetMin TweenOffsetMin(this RectTransform rectTransform, Vector2 to, float duration)
        {
            var tweener = UTween.OffsetMin(rectTransform, to, duration);
            return tweener;
        }

        public static TweenOffsetMin TweenOffsetMin(this RectTransform rectTransform, Vector2 from, Vector2 to, float duration)
        {
            var tweener = UTween.OffsetMin(rectTransform, from, to, duration);
            return tweener;
        }
    }

    #endregion
}
