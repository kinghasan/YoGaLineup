using System;
using UnityEngine;

namespace Aya.TweenPro
{
    [Tweener("Rect Transform Pivot", "RectTransform")]
    [Serializable]
    public class TweenPivot : TweenValueVector2<RectTransform>
    {
        public override Vector2 Value
        {
            get => Target.pivot;
            set => Target.pivot = value;
        }
    }

    #region Extension

    public static partial class UTween
    {
        public static TweenPivot Pivot(RectTransform rectTransform, Vector2 to, float duration)
        {
            var tweener = Play<TweenPivot, RectTransform, Vector2>(rectTransform, to, duration);
            return tweener;
        }

        public static TweenPivot Pivot(RectTransform rectTransform, Vector2 from, Vector2 to, float duration)
        {
            var tweener = Play<TweenPivot, RectTransform, Vector2>(rectTransform, from, to, duration);
            return tweener;
        }
    }

    public static partial class RectTransformExtension
    {
        public static TweenPivot TweenPivot(this RectTransform rectTransform, Vector2 to, float duration)
        {
            var tweener = UTween.Pivot(rectTransform, to, duration);
            return tweener;
        }

        public static TweenPivot TweenPivot(this RectTransform rectTransform, Vector2 from, Vector2 to, float duration)
        {
            var tweener = UTween.Pivot(rectTransform, from, to, duration);
            return tweener;
        }
    }

    #endregion
}
